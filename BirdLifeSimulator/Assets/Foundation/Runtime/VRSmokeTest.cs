using UnityEngine;
using UnityEngine.InputSystem;

namespace BirdLife.Foundation
{
    // VR input adapter; FoundationMode owns camera/input exclusivity.
    public sealed class VRSmokeTest : MonoBehaviour
    {
        [SerializeField] private SmokeTarget target;
        [SerializeField] private GameObject nonVRCamera;
        [SerializeField] private GameObject nonVRCanvas;
        [SerializeField] private GameObject simulator;
        private InputAction select;
        private void OnEnable()
        {
            if (simulator) simulator.SetActive(Application.isEditor);
            select = new InputAction("SmokeSelect", InputActionType.Button,
                "<XRController>{RightHand}/triggerPressed");
            select.Enable();
        }
        private void Update()
        {
            if (select != null && select.WasPressedThisFrame() && target) target.Activate();
        }
        private void OnDisable()
        {
            select?.Dispose();
            select = null;
            if (simulator) simulator.SetActive(false);
        }
    }
}


