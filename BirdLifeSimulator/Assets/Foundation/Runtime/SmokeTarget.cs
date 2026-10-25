using UnityEngine;

namespace BirdLife.Foundation
{
    public sealed class SmokeTarget : MonoBehaviour
    {
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private Color initialColor = Color.cyan;
        [SerializeField] private Color alternateColor = Color.yellow;
        private MaterialPropertyBlock properties;
        public int ActivationCount { get; private set; }
        public bool IsAlternate { get; private set; }
        public Color CurrentColor => IsAlternate ? alternateColor : initialColor;

        private void Awake()
        {
            if (!targetRenderer) targetRenderer = GetComponent<Renderer>();
            properties = new MaterialPropertyBlock();
            ActivationCount = 0;
            IsAlternate = false;
            ApplyColor();
        }

        public void Activate()
        {
            IsAlternate = !IsAlternate;
            ActivationCount++;
            ApplyColor();
        }

        private void ApplyColor()
        {
            if (!targetRenderer) return;
            targetRenderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", CurrentColor);
            properties.SetColor("_Color", CurrentColor);
            targetRenderer.SetPropertyBlock(properties);
        }
    }
}
