using UnityEngine;
using UnityEngine.UI;
using BirdLife.Flight;

namespace BirdLife.Gameplay
{
    // Per-portion proximity collection. No input, mission state or movement ownership.
    public sealed class FoodCollectible : MonoBehaviour
    {
        [SerializeField] private BirdMotor bird;
        [SerializeField, Min(0.01f)] private float interactionRadius = 0.3f;
        [SerializeField] private GameObject foodVisual;
        [SerializeField] private Text feedback;
        private CharacterController controller;
        public bool IsCollected { get; private set; }

        private void Awake()
        {
            controller = bird ? bird.GetComponent<CharacterController>() : null;
            if (!bird || !controller || !foodVisual || !feedback
                || foodVisual == gameObject || transform.IsChildOf(foodVisual.transform)
                || feedback.transform.IsChildOf(foodVisual.transform)
                || float.IsNaN(interactionRadius) || float.IsInfinity(interactionRadius)
                || interactionRadius <= 0f)
            {
                Debug.LogError("FoodCollectible requires a bird/controller, positive radius, separate food visual and feedback text.", this);
                enabled = false;
                return;
            }
            feedback.text = IsCollected ? "Food collected! (1/1)" : string.Empty;
        }

        private void LateUpdate()
        {
            // BirdMotor has already resolved this frame's movement and contacts in Update.
            if (!IsCollected && bird.isActiveAndEnabled && controller.enabled
                && controller.isGrounded && !bird.IsFlying
                && (bird.transform.position - transform.position).sqrMagnitude <= interactionRadius * interactionRadius)
            {
                IsCollected = true; // Set before side effects: re-entry cannot collect this portion again.
                foodVisual.SetActive(false);
                feedback.text = "Food collected! (1/1)";
            }

            // World-space uGUI uses the active tagged camera in both XR and non-XR modes.
            if (IsCollected && Camera.main)
                feedback.transform.rotation = Camera.main.transform.rotation;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}
