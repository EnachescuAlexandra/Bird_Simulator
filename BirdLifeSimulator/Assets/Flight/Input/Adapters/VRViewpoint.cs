using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

namespace BirdLife.Flight
{
    // View offsets only; FoundationMode retains ownership of platform activation.
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
        private Perspective appliedPerspective;
        private bool armed;

        private void Awake()
        {
            if (!origin || !bird || !observationPoint)
            {
                Debug.LogError("VRViewpoint requires the existing XR Origin, bird root and observation point.", this);
                enabled = false;
                return;
            }

            ApplyPerspective(false);
        }

        private void Update()
        {
            if (toggleView == null) return;
            if (perspective != appliedPerspective) ApplyPerspective(origin.gameObject.activeInHierarchy);

            bool useVR = origin.gameObject.activeInHierarchy;
            if (useVR != toggleView.enabled)
            {
                if (useVR) toggleView.Enable();
                else toggleView.Disable();
                armed = false;
            }
            if (!useVR) return;

            // A held button on mode entry cannot switch perspective until released.
            if (!toggleView.IsPressed()) armed = true;
            if (armed && toggleView.WasPressedThisFrame())
            {
                armed = false;
                perspective = perspective == Perspective.FirstPerson
                    ? Perspective.ThirdPerson : Perspective.FirstPerson;
                ApplyPerspective(true);
            }
        }

        private void ApplyPerspective(bool moveOrigin)
        {
            Vector3 target = bird.TransformPoint(perspective == Perspective.FirstPerson
                ? firstPersonOffset : thirdPersonOffset);
            Vector3 displacement = target - observationPoint.position;
            observationPoint.position = target;
            // Translate by the offset difference, preserving all tracked local head/controller poses.
            if (moveOrigin) origin.transform.position += displacement;
            appliedPerspective = perspective;
        }

        private void OnDisable()
        {
            toggleView?.Disable();
            armed = false;
        }

        private void OnDestroy() => toggleView?.Dispose();
    }
}
