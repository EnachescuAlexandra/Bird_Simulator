using UnityEngine;
using UnityEngine.InputSystem;

namespace BirdLife.Flight
{
    [DefaultExecutionOrder(-200)] // Clear the outgoing touch source before keyboard/Quest sample input.
    public sealed class TouchFlightInput : MonoBehaviour
    {
        [SerializeField] private BirdMotor motor;
        [SerializeField] private GameObject nonVRCamera;
        [SerializeField] private GameObject controlsCanvas;
        [SerializeField] private KeyboardFlightInput keyboardInput;
        [SerializeField] private InputActionAsset actions;

        private InputActionMap touch;
        private InputAction move, wingbeat, reset;
        private bool wingbeatArmed, resetArmed;

        private void Awake()
        {
            if (!motor || !nonVRCamera || !controlsCanvas || !keyboardInput || !actions)
            {
                Debug.LogError("TouchFlightInput requires the shared motor, non-XR camera, touch canvas, keyboard adapter and Flight actions.", this);
                enabled = false;
                return;
            }
            touch = actions.FindActionMap("Touch", true);
            move = touch.FindAction("Move", true);
            wingbeat = touch.FindAction("Wingbeat", true);
            reset = touch.FindAction("Reset", true);
            wingbeat.wantsInitialStateCheck = reset.wantsInitialStateCheck = true;
        }

        private void Update()
        {
            if (touch == null) return;
            bool useTouch = nonVRCamera.activeInHierarchy
                && !(Application.isEditor && keyboardInput.isActiveAndEnabled);
            if (controlsCanvas.activeSelf != useTouch) controlsCanvas.SetActive(useTouch);
            if (!useTouch || !Application.isFocused || !motor.isActiveAndEnabled)
            {
                StopInput();
                return;
            }
            if (!touch.enabled)
            {
                touch.Enable();
                wingbeatArmed = resetArmed = false;
                return; // Resolve held state before accepting a fresh press after mode/focus changes.
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
            Vector2 axes = move.ReadValue<Vector2>();
            motor.Submit(new FlightCommand(axes.x, axes.y, doWingbeat));
        }

        private void StopInput()
        {
            if (touch != null && touch.enabled)
            {
                touch.Disable();
                if (motor) motor.ClearInput();
            }
            wingbeatArmed = resetArmed = false;
        }

        private void OnDisable()
        {
            StopInput();
            if (controlsCanvas) controlsCanvas.SetActive(false);
        }
        private void OnApplicationFocus(bool focused) { if (!focused) StopInput(); }
        private void OnApplicationPause(bool paused) { if (paused) StopInput(); }
    }
}
