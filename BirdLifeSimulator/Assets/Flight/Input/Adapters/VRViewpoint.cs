using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR;
using CommonUsages = UnityEngine.XR.CommonUsages;

namespace BirdLife.Flight
{
    // Owns flight-scene origin placement; FoundationMode retains platform activation.
    [DefaultExecutionOrder(-1100)]
    public sealed class VRViewpoint : MonoBehaviour
    {
        public enum Perspective { FirstPerson, ThirdPerson }

        [SerializeField] private XROrigin origin;
        [SerializeField] private Transform bird;
        [SerializeField] private Transform observationPoint;
        [SerializeField] private Perspective perspective = Perspective.FirstPerson;
        [SerializeField] private Vector3 firstPersonOffset = new Vector3(0f, 0.10f, 0.13f);
        [SerializeField] private Vector3 thirdPersonOffset = new Vector3(0f, 0.18f, -0.55f);

        [SerializeField] private InputAction toggleView = new InputAction("ToggleBirdView",
            InputActionType.Button, "<XRController>{RightHand}/secondaryButton"); // Quest B.
        [SerializeField] private InputAction recenter = new InputAction("RecenterBirdView",
            InputActionType.Button, "<XRController>{LeftHand}/primaryButton"); // Quest X.
        [SerializeField, Min(0.25f)] private float recenterHoldSeconds = 1f;
        [Header("Optional seated turn-follow (Quest comfort testing required)")]
        [SerializeField] private bool turnFollowEnabled = false;
        [SerializeField, Min(0f), Tooltip("Maximum automatic yaw rotation in degrees per second.")]
        private float turnFollowSpeed = 25f;
        [SerializeField, Range(0f, 20f), Tooltip("Ignore horizontal yaw errors within this many degrees.")]
        private float turnFollowDeadZone = 3f;
        private bool armed, recenterArmed, following, paused, recenterRequested;
        private bool turnFollowReady;
        private BirdMotor birdMotor;
        private Vector3 previousBirdPosition;
        private float previousBirdYaw, turnFollowYawOffset;
        private int trackedFrames;
        private float recenterHeld;
        private Vector3 previousAnchorPosition;
        private Quaternion anchorYaw = Quaternion.identity;

        // FoundationMode uses this to leave flight placement to this component.
        public XROrigin Origin => origin;

        private void Awake()
        {
            if (!origin || !origin.Camera || !bird || !observationPoint)
            {
                Debug.LogError("VRViewpoint requires the existing XR Origin, bird root and observation point.", this);
                enabled = false;
                return;
            }

            toggleView.wantsInitialStateCheck = recenter.wantsInitialStateCheck = true;
            birdMotor = bird.GetComponent<BirdMotor>(); // Read-only teleport detection; no gameplay ownership.
            UpdateAnchor();
        }

        private void Update()
        {
            bool useVR = origin.gameObject.activeInHierarchy && Application.isFocused && !paused;
            if (!useVR)
            {
                SuspendView();
                return;
            }
            if (!toggleView.enabled || !recenter.enabled)
            {
                toggleView.Enable();
                recenter.Enable();
                return; // Let initial held-button state arrive before release-to-arm checks.
            }
            if (!following) return; // Tracking must settle before accepting view controls.

            // A held button on mode entry cannot switch perspective until released.
            if (!toggleView.IsPressed()) armed = true;
            if (armed && toggleView.WasPressedThisFrame())
            {
                armed = false;
                perspective = perspective == Perspective.FirstPerson
                    ? Perspective.ThirdPerson : Perspective.FirstPerson;
            }

            if (!recenter.IsPressed())
            {
                recenterArmed = true;
                recenterHeld = 0f;
            }
            else if (recenterArmed)
            {
                recenterHeld += Time.unscaledDeltaTime;
                if (recenterHeld >= recenterHoldSeconds)
                {
                    recenterRequested = true;
                    recenterArmed = false; // Exactly once per hold, including after resume.
                    recenterHeld = 0f;
                }
            }
        }

        private void UpdateAnchor()
        {
            Vector3 forward = Vector3.ProjectOnPlane(bird.forward, Vector3.up);
            if (forward.sqrMagnitude > 0.0001f)
                anchorYaw = Quaternion.LookRotation(forward, Vector3.up);
            observationPoint.SetPositionAndRotation(bird.position + anchorYaw *
                (perspective == Perspective.FirstPerson ? firstPersonOffset : thirdPersonOffset), anchorYaw);
        }

        private bool HasTracking()
        {
            // Use the same Input System HMD source as TrackedPoseDriver (also works with XR Device Simulator).
            var head = InputSystem.GetDevice<XRHMD>();
            const InputTrackingState required = InputTrackingState.Position | InputTrackingState.Rotation;
            if (head == null || !head.added || !head.enabled || !head.isTracked.isPressed
                || ((InputTrackingState)head.trackingState.ReadValue() & required) != required)
                return false;
            var xrHead = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.Head);
            return !xrHead.TryGetFeatureValue(CommonUsages.userPresence, out bool present) || present;
        }

        private void LateUpdate()
        {
            UpdateAnchor(); // Absolute bird-relative anchor, including Return to Nest teleports.
            // The motor excludes Return to Nest teleports from ActualDisplacement.
            bool discontinuity = (birdMotor && birdMotor.isActiveAndEnabled
                && (bird.position - previousBirdPosition - birdMotor.ActualDisplacement).sqrMagnitude > 0.0001f)
                || Mathf.Abs(Mathf.DeltaAngle(previousBirdYaw, anchorYaw.eulerAngles.y)) > 45f;
            previousBirdPosition = bird.position;
            previousBirdYaw = anchorYaw.eulerAngles.y;
            if (!origin.gameObject.activeInHierarchy || !Application.isFocused || paused || !HasTracking())
            {
                SuspendView();
                return;
            }

            // Allow tracked camera updates to arrive before placing it. Never rotate on recovery.
            if (!following)
            {
                if (++trackedFrames < 3) return;
                if (!origin.MoveCameraToWorldLocation(observationPoint.position)) return;
                following = true;
                turnFollowReady = false;
            }
            else
            {
                // Preserve natural local head movement; never pin the camera to the anchor each frame.
                origin.transform.position += observationPoint.position - previousAnchorPosition;
            }
            previousAnchorPosition = observationPoint.position;

            if (recenterRequested)
            {
                recenterRequested = false;
                turnFollowReady = false; // Preserve this explicit calibration; do not undo it next frame.
                // Looking vertically has no reliable horizontal facing: wait for another explicit hold.
                if (Vector3.ProjectOnPlane(origin.Camera.transform.forward, Vector3.up).sqrMagnitude < 0.0001f)
                    return;
                if (origin.MatchOriginUpCameraForward(Vector3.up, observationPoint.forward))
                    origin.MoveCameraToWorldLocation(observationPoint.position);
            }
            else
                FollowBirdTurn(discontinuity);
        }

        private void FollowBirdTurn(bool discontinuity)
        {
            if (!turnFollowEnabled)
            {
                turnFollowReady = false;
                return;
            }

            float birdYaw = anchorYaw.eulerAngles.y;
            float originYaw = origin.transform.eulerAngles.y;
            if (!turnFollowReady || discontinuity)
            {
                // Follow subsequent bird turns from the current calibration, never replay missed turns.
                turnFollowYawOffset = Mathf.DeltaAngle(birdYaw, originYaw);
                turnFollowReady = true;
                return;
            }

            // Absolute yaw error avoids integrated drift. HMD facing is deliberately not part of the target.
            float error = Mathf.DeltaAngle(originYaw, birdYaw + turnFollowYawOffset);
            float remaining = Mathf.Max(0f, Mathf.Abs(error) - turnFollowDeadZone);
            // Cap the time step so a frame stall cannot cause a sudden catch-up rotation.
            float step = Mathf.Min(remaining, turnFollowSpeed * Mathf.Min(Time.deltaTime, 0.05f));
            if (step > 0f)
                origin.RotateAroundCameraUsingOriginUp(Mathf.Sign(error) * step);
        }

        private void SuspendView()
        {
            toggleView?.Disable();
            recenter?.Disable();
            armed = recenterArmed = following = recenterRequested = false;
            trackedFrames = 0;
            recenterHeld = 0f;
            turnFollowReady = false;
        }

        private void OnDisable() => SuspendView();
        private void OnApplicationFocus(bool focused) { if (!focused) SuspendView(); }
        private void OnApplicationPause(bool value) { paused = value; if (value) SuspendView(); }

        private void OnDestroy()
        {
            toggleView?.Dispose();
            recenter?.Dispose();
        }
    }
}
