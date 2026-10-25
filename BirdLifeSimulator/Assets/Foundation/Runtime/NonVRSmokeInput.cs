using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace BirdLife.Foundation
{
    public sealed class NonVRSmokeInput : MonoBehaviour
    {
        [SerializeField] private SmokeTarget target;
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private Button button;
        private InputAction activate;

        private void OnEnable()
        {
            activate = actions.FindAction("NonVR/ActivateSmokeTarget", true);
            activate.performed += OnActivate;
            activate.Enable();
            // Only pointer clicks drive the button; keyboard Space uses the action above.
            button.navigation = new Navigation { mode = Navigation.Mode.None };
            button.onClick.AddListener(Activate);
        }

        private void OnDisable()
        {
            if (activate != null)
            {
                activate.performed -= OnActivate;
                activate.Disable();
            }
            if (button) button.onClick.RemoveListener(Activate);
        }

        private void OnActivate(InputAction.CallbackContext context) => Activate();
        private void Activate() { if (target) target.Activate(); }
    }
}
