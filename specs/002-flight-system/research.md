# Flight prototype research

Date: 2026-10-25. Reused foundation findings and inspected current scripts/manifest/lock;
researched only new movement/input/XR design questions. No installations or runtime checks.

Workflow note: the installed specify/plan/tasks skills and template structures were read.
PowerShell execution policy blocked resolve-template/setup-plan (and therefore setup-tasks
was not attempted). No policy was changed or bypassed. No preset/override/extension directories
or extension hooks were present; the repository's core templates were used manually to produce
the equivalent documents. Feature selection metadata points to this feature; branch main is unchanged.

| Decision | Rationale | Alternative / consequence |
| --- | --- | --- |
| Upright CharacterController; explicit arcade velocity/gravity | Simple collision-constrained motion and predictable control in an eight-hour prototype | Dynamic Rigidbody/aerodynamic lift needs more tuning; kinematic transform-only movement misses obstacle constraints |
| One motor, press-edge command adapters | Existing foundation already separates device input from shared behavior; repeated held flaps must not multiply | Separate PC/Quest flight rules rejected |
| Separate flight scene and scene-local mode wiring | FoundationMode bootstraps only FoundationSmoke and has smoke-specific references | Renaming/replacing the regression scene or forcing its smoke adapter into flight rejected |
| Preserve existing rigs; translate XR Origin, natural tracked rotation | No replacement XR stack, no tracking-transform writes or competing locomotion writers; fixed horizon for initial trial | Forced bank/pitch/yaw and physical-wing input deferred pending comfort experiment |
| Extend exact scene/output policy only before device build | Current guard otherwise rejects FlightPrototype; preserve foundation artifact evidence and XR isolation | Disabling guard or overwriting the foundation APK without identity separation rejected |
| Button first; motion/touch stretch | Baseline should demonstrate flight before gesture tuning and cross-device UI | No gesture framework or new Meta SDK |

Official documentation consulted (no package upgrades implied):

- [CharacterController.Move, Unity 6.3](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/CharacterController.Move.html):
  movement is constrained by collisions; gravity must be supplied by the caller.
- [Character Controller component](https://docs.unity3d.com/6000.3/Documentation/Manual/class-CharacterController.html):
  use a stable capsule and tune skin/step settings; validate corners and ground in the actual scene.
- [XR Origin, Core Utilities 2.6](https://docs.unity3d.com/Packages/com.unity.xr.core-utils@2.6/manual/xr-origin.html):
  move the origin while camera/controller children remain tracked relative to it.
- [XRI 3.3 locomotion](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.3/manual/locomotion.html):
  locomotion writers must be coordinated. Keep existing providers disabled; a general mediator
  integration is unnecessary for the first single-owner movement experiment.
- [Input System polling](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/manual/polling-actions.html):
  distinguish press edges from held values. Online 1.20 docs currently identify 1.20.1;
  installed 1.20.0 is retained and already supports the project's existing press-edge pattern.

Unresolved by documentation, deliberately empirical: flight feel, final tuning and Quest comfort.
Initial choices are reversible and tested before adoption; none is recorded as an observed pass.
