# Implementation Plan: First Flight Prototype

**Feature**: `002-flight-system` | **Date**: 2026-10-25 | **Spec**: [spec.md](spec.md)  
**Branch**: existing `main`, unchanged. Planning only; all implementation tasks are pending.

## Summary

Deliver PC flight first in a new `FlightPrototype` woodland greybox. Use one upright
CharacterController and an arcade BirdMotor with explicit velocity/gravity and discrete
wingbeats; device adapters submit the same commands. Retain existing foundation assets.
Next add Quest button input and a limited hardware trial; defer both stretch experiments first.
Deferring motion-driven wingbeats postpones a required project mechanic, not its acceptance:
carry it into a later milestone specification if T013 is not completed this week.

## Technical Context

| Item | Observed baseline / decision |
| --- | --- |
| Language / project | C#, existing Unity 6000.3.25f1 project in BirdLifeSimulator/ |
| Dependencies | Input System 1.20.0, URP 17.3.0, OpenXR 1.16.1, XRI 3.3.2, XR Core Utilities 2.6.0; no new packages planned |
| Tooling | Pipeline 0.9.1-exp.1 / CLI 1.0.0-beta.14; Auto Start=false; MCP for scene/prefab edits when reachable |
| Storage | Serialized scene/prefabs/input asset and tuning fields; no saves/database/service |
| Testing | Existing Unity Test Framework 1.6.0: focused EditMode motion tests; Editor and physical-device observations separately |
| Platforms | Windows Editor core; Quest standalone next; AndroidTouch smoke retained, flight touch stretch; Windows player not built |
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

Planned source paths (not created by this planning request):

```text
BirdLifeSimulator/Assets/
  Scenes/FlightPrototype.unity
  Flight/
    Runtime/{BirdMotor,FlightDynamics,FlightCommand,FlightModeCoordinator,FlightCameraFollow}.cs
    Prefabs/MockBlackbird.prefab
    Input/Flight.inputactions
    Input/Adapters/{KeyboardFlightInput,QuestFlightInput}.cs
    Input/Adapters/BirdLife.Flight.Input.asmdef
    Tests/EditMode/{FlightDynamicsTests.cs,BirdLife.Flight.EditModeTests.asmdef}
    Runtime/BirdLife.Flight.asmdef
```

The small Flight runtime assembly keeps deterministic motion checks testable without input,
XR or scene dependencies. Optional motion/touch adapters stay in this folder only if reached.
Keep the same AndroidTouch/QuestStandalone identities and compile defines in explicit
flight-scene mode configuration. Do not reference Assembly-CSharp's FoundationMode from
the new runtime asmdef or create a dependency cycle. Adapters reference Flight runtime and
Input System; coordinator references adapters as serialized Behaviour/command-source contracts,
not their concrete types. No foundation scripts need to move between assemblies.

## Design and Integration

- **Environment**: approximately 60 x 60 m, ground at y=0, ceiling y=12; spawn in an open
  clearing, eight primitive trees, two block obstacles and two contrasting landmarks. Bounds
  constrain the entire capsule. Primitive construction through Editor/MCP, with .meta intact.
- **Mock**: stable FlightRoot/capsule; visual child contains Body, YellowBeak, Tail,
  LeftWing, RightWing, with independent wing transforms and usable local rotation pivots.
  This visual hierarchy must support later flap, glide and idle/perching animations without
  changing the motor/collision root. No animator, clips or perching behavior required this week.
  Swapping the visual child does not move control code.
- **Motor**: sample one command per frame; integrate explicit velocity with delta time,
  apply CharacterController.Move once; resolve collisions and cancel blocked velocity.
  No Rigidbody on the same actor. Proposed default tuning: forward 2–6 m/s, overall cap
  8 m/s, reduced downward acceleration 2 m/s², wingbeat +2 m/s vertical, vertical cap
  -3..4 m/s, takeoff 4 m/s, cooldown 0.15 s, yaw 45 degrees/s. These are prototype starting
  values, not validated bird physics or comfort settings. Record actual tuning after trials.
- **Loop**: Space from grounded requests takeoff, not takeoff plus an extra flap. Later
  press edges flap; no press means glide/descend. A/D yaw; W/S change forward target within
  limits. R clears motion/queued input and returns to launch. Ground impact stops; no landing
  system. Read [contract](contracts/flight.md) for focus, switch and impulse rules.
- **PC view**: simple third-person follow for seeing the mock/obstacles; no Cinemachine
  dependency. This debugging view does not finalize the eventual blackbird-perspective design.
- **Modes**: FoundationMode's bootstrap explicitly targets FoundationSmoke and requires
  smoke-specific root names. Do not attach it blindly to the new scene. A scene-local
  FlightModeCoordinator owns PC/Quest rig+input activation using the same explicit product
  identities and compile defines. Editor defaults non-VR; Quest player VR. Reference/reuse
  existing VRRig with VRSmokeTest disabled only on the flight-scene instance, not its source.
  Keep simulator Editor-only/VR-only and old locomotion providers disabled.
- **Quest next**: trigger press for takeoff/flap; reuse the existing XRI left-hand movement
  Vector2 binding for steering/throttle, resolving its exact binding during wiring. Translate
  XR Origin with the motor, preserving tracked child transforms and unit scale; first trial
  does not force camera pitch, roll or yaw. Head look remains independent of motor heading.
  Start with a lower speed cap (3 m/s); no implicit comfort pass. Stationary observer fallback
  may diagnose input but is not completion of immersive flight testing.
- **Build blocker**: FoundationBuildGuard currently permits only FoundationSmoke as the
  sole enabled scene. Only before the Quest flight trial, extend its explicit scene allowlist
  to FoundationSmoke OR FlightPrototype and select that one scene in native profiles through
  the Editor. Retain all XR/profile checks. Make Output choose FlightPrototype.apk for the
  flight entry scene, keeping existing foundation APK/report paths intact. Validate smoke
  selection remains possible. Retain stable-Edit-only, event-driven synchronization with no
  Play/transition mutations or update retry loop. No guard bypass, new build framework or duplicated profiles.

## Budget, Dependencies and Fallback

| Scope | Minutes | Exit gate |
| --- | --- | --- |
| Core T001–T009 plus T016 | 330 | Complete repeatable PC flight demo and evidence |
| Next T010–T012 | 90 | Controller mapping, guarded build selection and Quest trial result |
| Contingency | 60 | Debugging, imports/build delay; not promised stretch capacity |
| Stretch T013–T015 | 120 extra, uncommitted | Only after core/next, with explicit remaining/additional budget |

At hour 4, if flight is not controllable, freeze art at minimum primitives and defer all Quest/
stretch work. At hour 6, prioritize core fixes/evidence; start the next-priority block only
with a realistic 90-minute window. Stop at eight hours and record deferrals. Never remove
speed/bounds/reset or input isolation to meet time. Unexpected full builds can consume the
reserve; device testing becomes pending rather than extending work silently.

## Complexity Tracking

No constitutional violation. Scene-local mode coordination avoids a general framework;
one narrow guard extension is justified by an observed existing scene restriction.
Research decisions/sources are in [research.md](research.md); validation is in [quickstart.md](quickstart.md).
