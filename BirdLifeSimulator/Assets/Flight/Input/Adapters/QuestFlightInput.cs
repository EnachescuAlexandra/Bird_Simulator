using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

namespace BirdLife.Flight
{
    [DefaultExecutionOrder(-100)] // Sample before BirdMotor.Update (+100).
    public sealed class QuestFlightInput : MonoBehaviour
    {
        public enum WingbeatSource { TriggerOnly, MotionWithTriggerFallback }

        [SerializeField] private BirdMotor motor;
        [SerializeField] private XROrigin origin;
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private MotionWingbeatInput motionWingbeat;
        [SerializeField] private WingbeatSource wingbeatSource = WingbeatSource.TriggerOnly;

        private InputActionMap quest;
        private InputAction move, wingbeat, reset;
        private bool wingbeatArmed, resetArmed;
        private WingbeatSource appliedWingbeatSource;

        private void Awake()
        {
            if (!motor || !origin || !actions)
            {
                Debug.LogError("QuestFlightInput requires the shared motor, existing XR Origin and Flight actions.", this);
                enabled = false;
                return;
            }
            quest = actions.FindActionMap("Quest", true);
            move = quest.FindAction("Move", true);
            wingbeat = quest.FindAction("Wingbeat", true);
            reset = quest.FindAction("Reset", true);
            wingbeat.wantsInitialStateCheck = reset.wantsInitialStateCheck = true;
            appliedWingbeatSource = wingbeatSource;
        }

        private void Update()
        {
            if (quest == null) return;
            if (wingbeatSource != appliedWingbeatSource)
            {
                motionWingbeat?.ResetHistory();
                wingbeatArmed = resetArmed = false;
                appliedWingbeatSource = wingbeatSource;
                return;
            }
            if (!origin.gameObject.activeInHierarchy || !Application.isFocused || !motor.isActiveAndEnabled)
            {
                StopInput();
                return;
            }
            if (!quest.enabled)
            {
                quest.Enable();
                motionWingbeat?.ResetHistory();
                wingbeatArmed = resetArmed = false;
                return; // Initial held input must release before requesting a new action.
            }

            if (!wingbeat.IsPressed()) wingbeatArmed = true;
            if (!reset.IsPressed()) resetArmed = true;
            bool doReset = resetArmed && reset.WasPressedThisFrame();
            bool doWingbeat = wingbeatArmed && wingbeat.WasPressedThisFrame();
            if (doReset)
            {
                wingbeatArmed = resetArmed = false;
                motor.ClearInput();
                motionWingbeat?.ResetHistory();
                motor.Submit(new FlightCommand(0f, 0f, reset: true));
                return;
            }
            if (doWingbeat)
            {
                wingbeatArmed = false;
                motionWingbeat?.ResetHistory();
            }
            else if (wingbeatSource == WingbeatSource.MotionWithTriggerFallback && motionWingbeat)
                doWingbeat = motionWingbeat.SampleWingbeat();
            Vector2 axes = move.ReadValue<Vector2>();
            motor.Submit(new FlightCommand(axes.x, axes.y, doWingbeat));
        }

        private void StopInput()
        {
            if (quest != null && quest.enabled)
            {
                quest.Disable();
                if (motor) motor.ClearInput();
            }
            wingbeatArmed = resetArmed = false;
            motionWingbeat?.ResetHistory();
        }

        private void OnDisable()
        {
            StopInput();
        }

        private void OnApplicationFocus(bool focused) { if (!focused) StopInput(); }
        private void OnApplicationPause(bool paused) { if (paused) StopInput(); }
    }
}
