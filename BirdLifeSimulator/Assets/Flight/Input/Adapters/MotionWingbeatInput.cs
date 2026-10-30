using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

namespace BirdLife.Flight
{
    // Detects an edge only. QuestFlightInput owns command submission; BirdMotor owns the impulse.
    public sealed class MotionWingbeatInput : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float strokeDistance = 0.10f;
        [SerializeField, Min(0.01f)] private float recoveryDistance = 0.10f;
        [SerializeField, Min(0.01f)] private float minimumDownwardSpeed = 0.15f;
        [SerializeField, Min(0.01f)] private float cooldown = 0.30f;
        [SerializeField, Min(0.01f)] private float maximumSampleGap = 0.15f;
        [SerializeField, Min(0.01f)] private float maximumSampleDistance = 0.25f;
        [SerializeField, Min(0.01f)] private float maximumTrackingSpeed = 6f;
        [SerializeField] private bool logGestures;
        [Tooltip("Optional existing simulator Reset action; observed only, never enabled here.")]
        [SerializeField] private InputActionReference simulatorResetAction;

        private readonly List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        private XRController leftDevice, rightDevice;
        private Vector3 previousLeft, previousRight;
        private float lowLeft, lowRight, peakLeft, peakRight, previousTime, nextBeatTime;
        private bool hasSample, armed;

        private void OnEnable()
        {
            ResetHistory();
            SubsystemManager.GetSubsystems(subsystems);
            foreach (var subsystem in subsystems) subsystem.trackingOriginUpdated += OnTrackingOriginUpdated;
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDisable()
        {
            foreach (var subsystem in subsystems) subsystem.trackingOriginUpdated -= OnTrackingOriginUpdated;
            subsystems.Clear();
            InputSystem.onDeviceChange -= OnDeviceChange;
            ResetHistory();
        }

        public void ResetHistory()
        {
            hasSample = armed = false;
            nextBeatTime = Time.unscaledTime + cooldown;
        }

        public bool SampleWingbeat()
        {
            if (!isActiveAndEnabled) { ResetHistory(); return false; }
            if (simulatorResetAction && simulatorResetAction.action.IsPressed())
            {
                ResetHistory();
                return false;
            }
            var left = XRController.leftHand;
            var right = XRController.rightHand;
            if (!TryPosition(left, out var l) || !TryPosition(right, out var r))
            {
                ResetHistory();
                return false;
            }

            float now = Time.unscaledTime;
            if (!hasSample || left != leftDevice || right != rightDevice)
            {
                leftDevice = left; rightDevice = right;
                Baseline(l, r, now);
                return false;
            }
            float dt = now - previousTime;
            if (dt <= 0f) return false;
            Vector3 dl = l - previousLeft, dr = r - previousRight;
            if (dt > maximumSampleGap || dl.magnitude > maximumSampleDistance
                || dr.magnitude > maximumSampleDistance || dl.magnitude / dt > maximumTrackingSpeed
                || dr.magnitude / dt > maximumTrackingSpeed)
            {
                // Pose jumps/stalls are not strokes. Require a new upward recovery from this baseline.
                Baseline(l, r, now);
                return false;
            }
            previousLeft = l; previousRight = r; previousTime = now;

            if (!armed)
            {
                lowLeft = Mathf.Min(lowLeft, l.y); lowRight = Mathf.Min(lowRight, r.y);
                if (now >= nextBeatTime && l.y - lowLeft >= recoveryDistance && r.y - lowRight >= recoveryDistance)
                {
                    armed = true;
                    peakLeft = l.y; peakRight = r.y;
                    if (logGestures) Debug.Log("Wingbeat gesture re-armed.", this);
                }
                return false;
            }

            peakLeft = Mathf.Max(peakLeft, l.y); peakRight = Mathf.Max(peakRight, r.y);
            if (peakLeft - l.y < strokeDistance || peakRight - r.y < strokeDistance
                || -dl.y / dt < minimumDownwardSpeed || -dr.y / dt < minimumDownwardSpeed)
                return false;

            Baseline(l, r, now);
            if (logGestures) Debug.Log("Wingbeat gesture detected.", this);
            return true;
        }

        private void Baseline(Vector3 left, Vector3 right, float now)
        {
            previousLeft = left; previousRight = right; previousTime = now;
            lowLeft = left.y; lowRight = right.y;
            hasSample = true; armed = false;
            nextBeatTime = now + cooldown;
        }

        private static bool TryPosition(XRController device, out Vector3 position)
        {
            position = default;
            if (device == null || !device.added || !device.isTracked.isPressed
                || (device.trackingState.ReadValue() & (int)InputTrackingState.Position) == 0) return false;
            position = device.devicePosition.ReadValue();
            return Finite(position.x) && Finite(position.y) && Finite(position.z);
        }

        private static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);
        private void OnTrackingOriginUpdated(XRInputSubsystem _) => ResetHistory();
        private void OnDeviceChange(UnityEngine.InputSystem.InputDevice device, InputDeviceChange change)
        {
            if (device == leftDevice || device == rightDevice) ResetHistory();
        }

        private void OnApplicationFocus(bool focused) { if (!focused) ResetHistory(); }
        private void OnApplicationPause(bool paused) { if (paused) ResetHistory(); }
    }
}
