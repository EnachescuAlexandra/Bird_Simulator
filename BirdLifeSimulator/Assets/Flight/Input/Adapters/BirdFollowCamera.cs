using UnityEngine;

namespace BirdLife.Flight
{
    // Attached only to the existing non-XR camera; XR tracking has its own untracked origin.
    public sealed class BirdFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform bird;
        [SerializeField] private Vector3 offset = new Vector3(0f, 0.35f, -0.8f);
        [SerializeField] private Vector3 lookOffset = new Vector3(0f, 0.08f, 0f);

        private void Awake()
        {
            if (!bird)
            {
                Debug.LogError("BirdFollowCamera requires the existing bird root.", this);
                enabled = false;
            }
        }

        private void LateUpdate()
        {
            transform.position = bird.TransformPoint(offset);
            transform.rotation = Quaternion.LookRotation(bird.TransformPoint(lookOffset) - transform.position, Vector3.up);
        }
    }
}
