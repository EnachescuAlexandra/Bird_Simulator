# Implementation Plan: First Flight Prototype

**Feature**: `002-flight-system` | **Date**: 2026-10-25 | **Spec**: [spec.md](spec.md)  
**Branch**: existing `main`, unchanged. Step 1 static graybox implemented; manual review and later work pending.

## Summary

Deliver a static `FlightPrototype` woodland graybox first, then the primitive bird/reused
XR rig, two viewpoints and an identified static Quest APK for real scale evaluation.
Meta Quest 2 standalone is primary, Android touch secondary, Windows standalone optional
later. Keyboard is an Editor debugging aid and never a prerequisite for Quest functionality.
Main Modules (final PDF supplied 2026-10-30, pp. 9–13) guides layout and replaceable assets;
its ideal menus, missions, energy, scenarios and final art remain outside this stage.

After static scale testing, use one upright
CharacterController and an arcade BirdMotor with explicit velocity/gravity and discrete
wingbeats; device adapters submit the same commands. Retain existing foundation assets.
Add Quest joystick/button input and a flight trial; two-controller gestures are required later,
followed by secondary touch controls. Reuse the installed XR simulator for Editor testing.
Deferring motion-driven wingbeats postpones a required project mechanic, not its acceptance:
carry it into a later milestone specification if T013 is not completed this week.

## Technical Context

| Item | Observed baseline / decision |
| --- | --- |
| Language / project | C#, existing Unity 6000.3.25f1 project in BirdLifeSimulator/ |
| Dependencies | Input System 1.20.0, URP 17.3.0, OpenXR 1.16.1, XRI 3.3.2, XR Core Utilities 2.6.0; ProBuilder 6.1.4 already installed locally; no new packages planned |
| Tooling | Pipeline 0.9.1-exp.1 / CLI 1.0.0-beta.15; MCP for scene/prefab edits when reachable |
| Storage | Serialized scene/prefabs/input asset and tuning fields; no saves/database/service |
| Testing | Existing Unity Test Framework 1.6.0: focused EditMode motion tests; Editor and physical-device observations separately |
| Platforms | Quest 2 standalone primary; Android touch secondary; existing XR simulator/keyboard for Editor debugging; no Windows player milestone |
| Performance | Small primitive scene and no new expensive effects; no sustained device fps/comfort result claimed. Foundation performance targets remain provisional |
| Scope | One bird, one small scene, about eight hours; no aerodynamic solver, locomotion framework or final art |

## Constitution Check

Pre-design and post-design: **pass**. I/VII: bounded solo scope, time gates and early flight/
comfort experiments. II/III: shared motor, separate inputs/camera and actual Quest trial.
IV: task-level criteria and separate automated/manual evidence. V: reuse installed versions,
preserve GUID/.meta and prefer Editor/MCP. VI: update implemented-behavior/AI/asset records
only when that work happens. No exception/amendment is required; unfinished foundation
performance/comfort checks are explicitly carried forward, not declared passed.

## Project Structure

Documentation: spec.md, plan.md, research.md, data-model.md, contracts/flight.md,
quickstart.md, tasks.md and checklists/requirements.md in this feature directory.

Source paths (only the Editor builder and static scene belong to Step 1):

```text
BirdLifeSimulator/Assets/
  Scenes/FlightPrototype.unity
  Flight/
    Editor/FlightGrayboxBuilder.cs
    Materials/Graybox*.mat
    Runtime/{BirdMotor,FlightDynamics,FlightCommand}.cs
    Prefabs/MockBlackbird.prefab
    Input/Flight.inputactions
    Input/Adapters/{KeyboardFlightInput,QuestFlightInput}.cs
    Input/Adapters/BirdLife.Flight.Input.asmdef
    Tests/EditMode/{FlightDynamicsTests.cs,BirdLife.Flight.EditModeTests.asmdef}
    Runtime/BirdLife.Flight.asmdef
```

The small Flight runtime assembly keeps deterministic motion checks testable without input,
XR or scene dependencies. Required later motion and secondary touch adapters stay in this folder.
Keep the same AndroidTouch/QuestStandalone identities and compile defines in explicit
flight-scene mode configuration. Do not reference Assembly-CSharp's FoundationMode from
the new runtime asmdef or create a dependency cycle. Adapters reference Flight runtime and
Input System. Add scene-local mode wiring only when needed; do not pre-create interfaces,
coordinators or a PC chase camera for static observation. No foundation scripts need to move.

Data-model entities are responsibilities/serialized data, not mandatory new managers or
services. Named flight components are custom project types. Integration details and edge
semantics in the flight contract govern T005/T006/T008/T010/T013 without additional systems.

## Design and Integration

- **Environment**: approximately 60 x 60 m, ground at y=0, ceiling y=12; open exploration
  clearing, eight primitive trees, shrubs, two solid obstacles and two contrasting landmarks,
  a refuge/nest placeholder, resting surfaces and three near-ground food markers. Keep open
  corridors. Step 1 uses visible perimeter and ceiling-height corner markers, not a roof;
  later motor bounds constrain the capsule. Editor script creates editable objects once,
  refuses an existing scene/assets and never auto-regenerates or replaces manual edits.
  Use ordinary primitives; ProBuilder only where custom geometry helps. No Terrain/art imports.
  Start/reset at the refuge's FutureBirdStart marker (currently world (0,0.4,-17)); choose
  the future capsule offset so the launch pose is valid on its surface. Known primitive
  collider mismatch and required pre-flight checks are recorded in docs/foundation-validation.md.
- **Mock**: stable FlightRoot; visual child contains Body, Head, YellowBeak, Tail,
  LeftWing, RightWing, with independent wing transforms and usable local rotation pivots.
  This visual hierarchy must support later flap, glide and idle/perching animations without
  changing the motor/collision root. No animator, clips or perching behavior required this week.
  Swapping the visual child does not move control code.
- **Motor**: sample one command per frame; integrate explicit velocity with delta time,
  apply CharacterController.Move once with velocity * deltaTime; it supplies no gravity.
  Handle post-Move Below/Above/Sides per contract, never cancel takeoff using stale isGrounded.
  Reset clears contact bookkeeping at a valid launch. Use stepOffset=0 and record skin/minimum
  move settings. Verify actual displacement speed separately from the requested cap, excluding
  explicit reset teleports from speed measurement.
  No Rigidbody on the same actor. Proposed debugging tuning: forward 2–6 m/s, overall cap
  8 m/s, reduced downward acceleration 2 m/s², wingbeat +2 m/s vertical plus a bounded
  forward impulse (initial +0.5 m/s, subject to tuning and total cap), vertical cap
  -3..4 m/s, takeoff 4 m/s, cooldown 0.15 s, yaw 45 degrees/s. These are prototype starting
  values, not validated bird physics or comfort settings. Record actual tuning after trials.
- **Optional Editor keyboard loop**: Space from grounded requests takeoff, not takeoff plus an extra flap. Later
  press edges flap; no press applies gravity, with descent after the apex. A/D yaw; W/S change forward target within
  limits. R clears motion/queued input and returns to the refuge launch. Ground impact stops; no landing
  system. Read [contract](contracts/flight.md) for focus, switch and impulse rules.
- **Static viewpoints (Step 3)**: first-person near Head and third-person behind the visible
  mock, configured as two offsets for one tracked HMD camera on the reused rig. Preserve unit
  scale and tracking; do not write tracked child poses or force pitch/roll/animation shake.
  Keep a serialized default plus one dedicated controller-button press to switch offsets
  during static observation in the same APK; record the binding in T007. No Cinemachine,
  duplicate rigs or complex camera manager. The viewpoint button does not issue flight commands.
  A separate PC follow camera is unnecessary for this coursework stage.
- **Modes**: FoundationMode's bootstrap explicitly targets FoundationSmoke and requires
  smoke-specific root names. Do not attach it blindly to the new scene. A scene-local
  setup owns only necessary rig/input activation using the same explicit product
  identities and compile defines. Editor reuses the existing simulator; Quest player VR. Reference/reuse
  existing VRRig with VRSmokeTest disabled only on the flight-scene instance, not its source.
  Keep simulator Editor-only/VR-only and old locomotion providers disabled. VRSmokeTest currently
  activates the simulator: preserve that Editor-only function when disabling the smoke adapter.
  Sample then consume once per render frame in explicit order; clear edges/held intent on
  reset, disable, focus/mode change and require release-to-rearm. Gameplay maps have one owner;
  rig InputActionManager must not activate inactive maps. Serialize bindings before XR startup.
- **Quest flight after static gate**: trigger press for takeoff/flap; reuse the existing XRI left-hand movement
  Vector2 binding for steering/throttle, resolving its exact binding during wiring. Translate
  an independent XR Origin by actual post-collision displacement, preserving tracked offsets,
  child transforms and unit scale without inheriting bird rotation; first trial
  does not force camera pitch, roll or yaw. Head look remains independent of motor heading.
  Start with a lower speed cap (3 m/s); no implicit comfort pass. Stationary observer fallback
  may diagnose input but is not completion of immersive flight testing.
  Bird bounds do not constrain physical head movement. Required later motion detection supplies
  an edge to the existing Quest adapter while retaining steering; tracking changes, recenter,
  first sample and mode/focus changes require a fresh valid baseline and rearm.
- **Required gesture/testing**: both controllers move deliberately downward for one bounded
  upward/forward impulse; both rise to re-arm. Inspector thresholds and tracking-local history
  stay outside BirdMotor. Reuse the installed simulator to move both virtual hands through
  the actual detector where supported; Space is an Editor-only route to the same action.
  Reject jitter, tracking loss/regain and recenter discontinuities. Optional detected/rearmed
  diagnostics only; no test framework, custom debug UI, Meta simulator or package changes.
  Editor checks do not pass real Quest gestures or comfort.
- **Build blocker**: FoundationBuildGuard currently permits only FoundationSmoke as the
  sole enabled scene. In Step 3 before the static Quest trial, extend its explicit scene allowlist
  to FoundationSmoke OR FlightPrototype and select that one scene in native profiles through
  the Editor. Retain all XR/profile checks. Make Output choose FlightPrototype.apk for the
  flight entry scene, keeping existing foundation APK/report paths intact. Validate smoke
  selection remains possible. Retain stable-Edit-only, event-driven synchronization with no
  Play/transition mutations or update retry loop. No guard bypass, new build framework or duplicated profiles.

## Budget, Dependencies and Fallback

| Scope | Minutes | Exit gate |
| --- | --- | --- |
| Step 1: T001, T003 | 60–90 | Editable static scene; no gameplay/XR build changes |
| Step 2: T004, T008 | 60–90 | Replaceable mock and one reused rig |
| Step 3: T007, T011 | 45–75 | Two static tracked viewpoints and narrow guard adjustment |
| Step 4: T012 | 60–120+ | Distinct APK; real scale trial recorded separately |
| Later: T002, T005, T010, T006, T009, T017, T013 | Re-estimate after scale trial | Shared flight, Quest input, optional Editor debugging, physical flight trial, then required gestures |

Protect static Quest scale testing before spending time on gameplay. The original eight-hour
window is a planning ceiling, not a guarantee that builds/device access fit. Stop after each
requested step and record pending work; flight and touch do not block coursework graybox review.
T016 records each increment rather than waiting for the flight loop. No HTML dashboard now.

## Complexity Tracking

No constitutional violation. Scene-local mode coordination avoids a general framework;
one narrow guard extension is justified by an observed existing scene restriction.
Research decisions/sources are in [research.md](research.md); validation is in [quickstart.md](quickstart.md).
