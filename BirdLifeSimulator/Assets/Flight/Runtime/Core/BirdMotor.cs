using System.Collections.Generic;
using UnityEngine;

namespace BirdLife.Flight
{
    [RequireComponent(typeof(CharacterController))]
    [DefaultExecutionOrder(100)] // Input adapters sample in Update before this motor.
    public sealed class BirdMotor : MonoBehaviour
    {
        [SerializeField] private Transform refuge;
        [SerializeField] private Bounds flightBounds = new Bounds(new Vector3(0f, 6f, 0f), new Vector3(60f, 12f, 60f));
        [SerializeField] private FlightTuning tuning = new FlightTuning();
        [SerializeField] private float resetClearance = 0.005f;

        private CharacterController controller;
        private FlightDynamics dynamics;
        private FlightCommand pending;
        private int pendingFrame = -1;
        private readonly List<Vector3> contactNormals = new List<Vector3>(4);
        private readonly Collider[] overlaps = new Collider[8];

        public Vector3 Velocity => dynamics?.Velocity ?? Vector3.zero;
        public bool IsFlying => dynamics != null && dynamics.IsFlying;
        public int AcceptedWingbeats => dynamics?.AcceptedWingbeats ?? 0;
        public Vector3 ActualDisplacement { get; private set; }
        public float ActualSpeed { get; private set; }

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (!ValidSetup()) { Fail(); return; }
            dynamics = new FlightDynamics(tuning);
            ResetToRefuge();
        }

        // Edges may be merged within one frame, but are never replayed in a later frame.
        public void Submit(FlightCommand command)
        {
            pending = pendingFrame == Time.frameCount
                ? new FlightCommand(command.Steer, command.Throttle,
                    pending.Activate || command.Activate, pending.Reset || command.Reset)
                : command;
            pendingFrame = Time.frameCount;
        }

        public void ClearInput() { pending = default; pendingFrame = -1; }

        private void Update()
        {
            FlightCommand command = pendingFrame == Time.frameCount ? pending : default;
            ClearInput();
            Tick(command, Time.deltaTime);
        }

        private void Tick(FlightCommand command, float deltaTime)
        {
            ActualDisplacement = Vector3.zero;
            ActualSpeed = 0f;
            if (!ValidSetup()) { Fail(); return; }
            if (command.Reset) { ResetToRefuge(); return; }
            if (deltaTime <= 0f || !FlightTuning.Finite(deltaTime)) return;
            dynamics.Step(command, deltaTime);
            transform.rotation = Quaternion.Euler(0f, dynamics.Heading, 0f);

            Vector3 desired = dynamics.Velocity * deltaTime;
            bool wasDescending = dynamics.Velocity.y <= 0f;
            Bounds capsule = controller.bounds;
            Vector3 minimum = flightBounds.min + capsule.extents;
            Vector3 maximum = flightBounds.max - capsule.extents;
            Vector3 target = capsule.center + desired;
            Vector3 clamped = new Vector3(Mathf.Clamp(target.x, minimum.x, maximum.x),
                Mathf.Clamp(target.y, minimum.y, maximum.y), Mathf.Clamp(target.z, minimum.z, maximum.z));
            contactNormals.Clear();
            Vector3 before = transform.position;
            CollisionFlags flags = controller.Move(clamped - capsule.center); // One Move per motion frame.
            ActualDisplacement = transform.position - before;
            ActualSpeed = ActualDisplacement.magnitude / deltaTime;

            foreach (Vector3 normal in contactNormals) dynamics.ClipVelocity(normal);
            if (target.x != clamped.x) dynamics.ClipVelocity(target.x > clamped.x ? Vector3.left : Vector3.right);
            if (target.z != clamped.z) dynamics.ClipVelocity(target.z > clamped.z ? Vector3.back : Vector3.forward);
            if (target.y != clamped.y)
            {
                bool below = target.y < clamped.y;
                dynamics.ClipVelocity(below ? Vector3.up : Vector3.down);
                flags |= below ? CollisionFlags.Below : CollisionFlags.Above;
            }
            dynamics.ResolveContacts(flags, wasDescending); // Fresh upward takeoff ignores Below.
        }

        public void ResetToRefuge()
        {
            ClearInput();
            if (dynamics == null || !ValidSetup()) { Fail(); return; }
            Quaternion rotation = Quaternion.Euler(0f, refuge.eulerAngles.y, 0f);
            Vector3 position = refuge.position + Vector3.up * resetClearance;
            Vector3 center = position + rotation * controller.center;
            Vector3 extents = new Vector3(controller.radius, controller.height * 0.5f, controller.radius);
            if (!flightBounds.Contains(center - extents) || !flightBounds.Contains(center + extents))
            { Fail("Refuge capsule is outside flight bounds."); return; }

            Physics.SyncTransforms();
            Vector3 halfSegment = Vector3.up * (controller.height * 0.5f - controller.radius);
            int count = Physics.OverlapCapsuleNonAlloc(center - halfSegment, center + halfSegment,
                controller.radius, overlaps, ~0, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < count; i++)
                if (overlaps[i] != controller)
                { Fail("Refuge capsule overlaps a solid collider: " + overlaps[i].name); return; }

            controller.enabled = false; // Clear stale CharacterController contact bookkeeping.
            transform.SetPositionAndRotation(position, rotation);
            controller.enabled = true;
            dynamics.Reset(rotation.eulerAngles.y);
            contactNormals.Clear();
            ActualDisplacement = Vector3.zero;
            ActualSpeed = 0f; // Reset teleport is excluded from movement-speed measurements.
        }

        private bool ValidSetup() => refuge && controller && controller.enabled
            && tuning != null && tuning.IsValid() && !GetComponent<Rigidbody>()
            && transform.lossyScale == Vector3.one && resetClearance >= controller.skinWidth
            && controller.stepOffset == 0f && controller.minMoveDistance == 0f
            && Finite(flightBounds.center) && Finite(flightBounds.size) && Finite(refuge.position)
            && Finite(transform.position) && FlightTuning.Finite(resetClearance)
            && flightBounds.size.x > controller.radius * 2f
            && flightBounds.size.y > controller.height && flightBounds.size.z > controller.radius * 2f;

        private static bool Finite(Vector3 value) => FlightTuning.Finite(value.x)
            && FlightTuning.Finite(value.y) && FlightTuning.Finite(value.z);

        private void Fail(string reason = "Check refuge, finite tuning, unit scale, controller settings and flight bounds.")
        {
            Debug.LogError("BirdMotor: " + reason, this);
            enabled = false;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) => contactNormals.Add(hit.normal);
        private void OnDisable() => ClearInput();
        private void OnApplicationFocus(bool focused) { if (!focused) ClearInput(); }
        private void OnApplicationPause(bool paused) { if (paused) ClearInput(); }
    }
}
