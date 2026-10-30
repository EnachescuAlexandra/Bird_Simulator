# Flight prototype research

Date: 2026-10-25. Reused foundation findings and inspected current scripts/manifest/lock;
researched only new movement/input/XR design questions. No installations or runtime checks.

2026-10-30 correction: Main Modules final PDF and the user's revised priority place static
Quest graybox/viewpoint evaluation before flight. Preserve the research decisions below;
keyboard debugging is not a Quest prerequisite. Reuse ProBuilder 6.1.4 and the installed
XR simulator; two-controller gestures remain required later. No new external assets now.
Checkpoint clarification: refuge start/reset and open exploration clearing; one controller
button selects the two static viewpoints in the same APK. T017 records real button/joystick
flight validation before T013 gesture tuning. These are planned behaviors, not new test results.

Execution-order amendment (2026-10-30): the current order and user-owned end gates in tasks.md supersede the earlier static-before-implementation and gestures-before-touch sequencing below. Retain the shared motor, separate adapters and tracked-camera decisions; no new research or device inventory is required.

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
| Static Quest scale/viewpoints first; shared flight/button next; required motion then secondary touch | Coursework allows static observation; keyboard is only an Editor aid, never a Quest gate | No gesture framework, Meta XR Simulator, new SDK or Windows player work |

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
