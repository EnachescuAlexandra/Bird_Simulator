using System;
using UnityEngine;

namespace BirdLife.Flight
{
    [Serializable]
    public sealed class FlightTuning
    {
        public float minimumForwardSpeed = 1f;
        public float maximumForwardSpeed = 2.5f;
        public float maximumSpeed = 3f;
        public float gravity = 2f;
        public float upwardImpulse = 2f;
        public float forwardImpulse = 0.5f;
        public float takeoffSpeed = 2.5f;
        public float minimumVerticalSpeed = -3f;
        public float maximumVerticalSpeed = 3f;
        public float wingbeatCooldown = 0.15f;
        public float yawSpeed = 45f;
        public float throttleAcceleration = 1f;
        [Min(0f), Tooltip("Forward walking speed at full directional input, in metres per second.")]
        public float groundSpeed = 0.6f;

        public bool IsValid()
        {
            return Finite(minimumForwardSpeed) && Finite(maximumForwardSpeed) && Finite(maximumSpeed)
                && Finite(gravity) && Finite(upwardImpulse) && Finite(forwardImpulse) && Finite(takeoffSpeed)
                && Finite(minimumVerticalSpeed) && Finite(maximumVerticalSpeed) && Finite(wingbeatCooldown)
                && Finite(yawSpeed) && Finite(throttleAcceleration) && Finite(groundSpeed)
                && minimumForwardSpeed >= 0f && maximumForwardSpeed >= minimumForwardSpeed
                && maximumSpeed > 0f && maximumForwardSpeed <= maximumSpeed && gravity > 0f
                && upwardImpulse > 0f && forwardImpulse > 0f && takeoffSpeed > 0f
                && minimumVerticalSpeed < 0f && maximumVerticalSpeed > 0f
                && takeoffSpeed <= maximumVerticalSpeed && wingbeatCooldown > 0f
                && yawSpeed > 0f && throttleAcceleration > 0f && groundSpeed >= 0f;
        }

        internal static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);
    }

    // Deterministic motion state; no scene, device, input-map or camera references.
    public sealed class FlightDynamics
    {
        private readonly FlightTuning tuning;
        private float forwardSpeed;
        public Vector3 Velocity { get; private set; }
        public float Heading { get; private set; }
        public bool IsFlying { get; private set; }
        public float Cooldown { get; private set; }
        public int AcceptedWingbeats { get; private set; }

        public FlightDynamics(FlightTuning tuning)
        {
            if (tuning == null || !tuning.IsValid()) throw new ArgumentException("Invalid flight tuning.");
            this.tuning = tuning;
        }

        public void Reset(float heading)
        {
            Heading = Mathf.Repeat(heading, 360f);
            Velocity = Vector3.zero;
            forwardSpeed = Cooldown = 0f;
            AcceptedWingbeats = 0;
            IsFlying = false;
        }

        public void Step(FlightCommand command, float deltaTime)
        {
            if (command.Reset) { Reset(Heading); return; }
            if (deltaTime <= 0f || float.IsNaN(deltaTime) || float.IsInfinity(deltaTime)) return;
            Cooldown = Mathf.Max(0f, Cooldown - deltaTime);
            float vertical = Velocity.y;

            if (command.Activate && Cooldown <= 0f)
            {
                if (!IsFlying)
                {
                    IsFlying = true;
                    forwardSpeed = tuning.minimumForwardSpeed;
                    vertical = tuning.takeoffSpeed;
                }
                else
                {
                    vertical += tuning.upwardImpulse;
                    forwardSpeed += tuning.forwardImpulse;
                    AcceptedWingbeats++;
                }
                Cooldown = tuning.wingbeatCooldown;
            }
            Heading = Mathf.Repeat(Heading + command.Steer * tuning.yawSpeed * deltaTime, 360f);
            if (!IsFlying)
            {
                // Reuse throttle as forward walking input; releasing it stops horizontal motion.
                // Gravity still passes through BirdMotor's single CharacterController.Move, including off ledges.
                vertical = Mathf.Clamp(vertical - tuning.gravity * deltaTime,
                    tuning.minimumVerticalSpeed, 0f);
                Velocity = Quaternion.Euler(0f, Heading, 0f) * Vector3.forward
                    * (Mathf.Clamp01(command.Throttle) * tuning.groundSpeed) + Vector3.up * vertical;
                return;
            }

            // Zero speed after an obstacle stays zero until throttle or another impulse requests motion.
            forwardSpeed = Mathf.Clamp(forwardSpeed + command.Throttle * tuning.throttleAcceleration * deltaTime,
                forwardSpeed > 0f ? tuning.minimumForwardSpeed : 0f, tuning.maximumForwardSpeed);
            vertical = Mathf.Clamp(vertical - tuning.gravity * deltaTime,
                tuning.minimumVerticalSpeed, tuning.maximumVerticalSpeed);
            Velocity = Vector3.ClampMagnitude(Quaternion.Euler(0f, Heading, 0f) * Vector3.forward * forwardSpeed
                + Vector3.up * vertical, tuning.maximumSpeed);
            forwardSpeed = new Vector2(Velocity.x, Velocity.z).magnitude;
        }

        public void ClipVelocity(Vector3 normal)
        {
            float inward = Vector3.Dot(Velocity, normal);
            if (inward < 0f) Velocity -= normal * inward;
            forwardSpeed = new Vector2(Velocity.x, Velocity.z).magnitude;
        }

        public void ResolveContacts(CollisionFlags flags, bool wasDescending)
        {
            if ((flags & CollisionFlags.Above) != 0) ClipVelocity(Vector3.down);
            if ((flags & CollisionFlags.Below) != 0 && wasDescending)
            {
                IsFlying = false;
                Velocity = Vector3.zero;
                forwardSpeed = Cooldown = 0f;
            }
        }
    }
}
