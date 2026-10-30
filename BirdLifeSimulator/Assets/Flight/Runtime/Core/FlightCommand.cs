using UnityEngine;

namespace BirdLife.Flight
{
    // Activate and Reset are press edges, supplied by exactly one active input adapter.
    public readonly struct FlightCommand
    {
        public float Steer { get; }
        public float Throttle { get; }
        public bool Activate { get; }
        public bool Reset { get; }

        public FlightCommand(float steer, float throttle, bool activate = false, bool reset = false)
        {
            Steer = Normalize(steer);
            Throttle = Normalize(throttle);
            Activate = activate;
            Reset = reset;
        }

        private static float Normalize(float value) =>
            float.IsNaN(value) || float.IsInfinity(value) ? 0f : Mathf.Clamp(value, -1f, 1f);
    }
}
