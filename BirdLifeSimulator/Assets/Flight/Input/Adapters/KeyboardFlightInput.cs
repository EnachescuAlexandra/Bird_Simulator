using UnityEngine;
using UnityEngine.InputSystem;

namespace BirdLife.Flight
{
    [DefaultExecutionOrder(-100)] // Submit before BirdMotor.Update (+100).
    public sealed class KeyboardFlightInput : MonoBehaviour
    {
        [SerializeField] private BirdMotor motor;
        [SerializeField] private GameObject nonVRCamera;
        [SerializeField] private InputActionAsset actions;
        [Tooltip("Disable when testing the later touch adapter with the mouse.")]
        [SerializeField] private bool enableKeyboardInEditor = true;

        private InputActionMap keyboard;
        private InputAction steer, throttle, wingbeat, reset;
        private bool wingbeatArmed, resetArmed;

        private void Awake()
        {
            if (!motor || !nonVRCamera || !actions)
            {
                Debug.LogError("KeyboardFlightInput requires the existing motor, non-VR camera and Flight actions.", this);
                enabled = false;
                return;
            }
            keyboard = actions.FindActionMap("Keyboard", true);
            steer = keyboard.FindAction("Steer", true);
            throttle = keyboard.FindAction("Throttle", true);
            wingbeat = keyboard.FindAction("Wingbeat", true);
            reset = keyboard.FindAction("Reset", true);
            wingbeat.wantsInitialStateCheck = reset.wantsInitialStateCheck = true;
        }

        private void Update()
        {
            if (keyboard == null) return;
            if (!Application.isEditor || !enableKeyboardInEditor || !nonVRCamera.activeInHierarchy
                || !Application.isFocused || !motor.isActiveAndEnabled)
            {
                StopInput();
                return;
            }
            if (!keyboard.enabled)
            {
                keyboard.Enable();
                wingbeatArmed = resetArmed = false;
                return; // Wait for the initial held-button state before allowing a fresh press.
            }

            if (!wingbeat.IsPressed()) wingbeatArmed = true;
            if (!reset.IsPressed()) resetArmed = true;
            bool doReset = resetArmed && reset.WasPressedThisFrame();
            bool doWingbeat = wingbeatArmed && wingbeat.WasPressedThisFrame();
            if (doReset)
            {
                wingbeatArmed = resetArmed = false;
                motor.ClearInput();
                motor.Submit(new FlightCommand(0f, 0f, reset: true));
                return;
            }
            if (doWingbeat) wingbeatArmed = false;
            motor.Submit(new FlightCommand(steer.ReadValue<float>(), throttle.ReadValue<float>(), doWingbeat));
        }

        private void StopInput()
        {
            if (keyboard != null && keyboard.enabled)
            {
                keyboard.Disable();
                if (motor) motor.ClearInput();
            }
            wingbeatArmed = resetArmed = false;
        }

        private void OnDisable() => StopInput();
        private void OnApplicationFocus(bool focused) { if (!focused) StopInput(); }
        private void OnApplicationPause(bool paused) { if (paused) StopInput(); }
    }
}
