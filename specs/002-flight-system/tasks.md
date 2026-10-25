# Tasks: First Flight Prototype

**Input**: [spec](spec.md), [plan](plan.md), [research](research.md),
[state model](data-model.md), [contract](contracts/flight.md), [validation](quickstart.md).
**Status**: Planning only; no feature work/tests/builds executed. IDs are local to feature 002.
**Paths**: Assets paths below are under BirdLifeSimulator/. Preserve .meta/GUIDs; scene/prefab
edits through Editor/MCP. No package changes planned. Request/execute a stage before proceeding.

## Phase 1 — Setup

- [ ] T001 Inspect committed baseline, live project/dirty state and existing versions; record the feature-002 baseline in `docs/foundation-validation.md`. **15 min. Depends:** foundation checkpoint. **Outcome/check:** correct project, unchanged smoke/XR references, pending foundation checks retained; stop before saving unknown edits. **Manual:** only Editor/server start or dirty-state clarification if required.

## Phase 2 — Foundations

- [ ] T002 Establish `Assets/Flight/Runtime/FlightCommand.cs`, `BirdLife.Flight.asmdef`, folder metadata and scene references per `contracts/flight.md`; keep the runtime assembly free of Input System/XR dependencies (adapters use a separate assembly if required). **20 min. Depends:** T001. **Outcome/check:** command axes "[-1,1]", edges and reset priority explicit, no dependency cycle; compilation succeeds. **Manual:** no, except unavailable Editor access.

## Phase 3 — US1: PC flight, minimum delivery

**Independent demonstration:** the complete core route in quickstart, without headset, after T003–T009.

- [ ] T003 [US1] Create `Assets/Scenes/FlightPrototype.unity` via Editor: ground/clearing, eight trees, two landmarks, two solid obstacles and visible 60 x 60 x 12 m bounds; preserve FoundationSmoke. **40 min. Depends:** T002. **Outcome/check:** open flight route, valid launch and capsule-containing volume, no new missing-reference/Console errors. **Manual:** visual layout confirmation.
- [ ] T004 [US1] Create `Assets/Flight/Prefabs/MockBlackbird.prefab` with stable FlightRoot and replaceable visual Body/YellowBeak/Tail/LeftWing/RightWing subtree. **25 min. Depends:** T003. **Outcome/check:** all parts distinguishable, each wing rotates independently around its local pivot for future flap/glide/idle-perching animation without motor changes, capsule independent of visuals; swap a temporary primitive visual and restore without breaking root references. **Manual:** visual confirmation; no Blender/animation work.
- [ ] T005 [US1] Implement `Assets/Flight/Runtime/BirdMotor.cs` and `FlightDynamics.cs` using CharacterController movement and explicit bounded velocity, takeoff, gravity/glide, yaw, one-shot flap/cooldown and reset. **75 min. Depends:** T004. **Outcome/check:** Grounded/Flying transitions; finite velocity, cap tolerance 0.1 m/s, no device references or Rigidbody conflict. Reset clears motion/cooldown/edges. Invalid tuning stops with a clear error. **Manual:** initial flight-feel observation only; automated checks in T009.
- [ ] T006 [US1] Add `Assets/Flight/Input/Flight.inputactions` and `KeyboardFlightInput.cs` under `Assets/Flight/Input/Adapters/` with its own Input System-aware asmdef referencing Flight runtime: Space activation, A/D yaw, W/S speed, R reset. **30 min. Depends:** T005. **Outcome/check:** press edge consumed once, held Space not repeating, focus loss/disable discards queued input; no motor rule duplicated. **Manual:** physical keyboard verification.
- [ ] T007 [US1] Add `Assets/Flight/Runtime/FlightCameraFollow.cs` and wire a simple PC chase camera in `Assets/Scenes/FlightPrototype.unity`. **20 min. Depends:** T006. **Outcome/check:** mock/route visible during the basic loop, no new package, one active camera/listener. **Manual:** Game view observation.
- [ ] T008 [US1] Wire `Assets/Flight/Runtime/FlightModeCoordinator.cs` in the flight scene with explicit identity/rig/source references; reuse VRRig as an instance, disable its smoke adapter via scene override, retain Editor-only simulator and smoke regression setup. **30 min. Depends:** T007. **Outcome/check:** PC default in Editor, Quest default in player define, idempotent selection, inactive sources silent. Until T010, Quest flight input is pending, not falsely enabled. **Manual:** mode/view observation; do not force foundation's scene-specific bootstrap into flight.
- [ ] T009 [US1] Add focused `Assets/Flight/Tests/EditMode/FlightDynamicsTests.cs` and test asmdef; execute cap/glide/edge/reset checks and the core quickstart Editor route; record in `docs/foundation-validation.md`. **45 min. Depends:** T008. **Outcome/check:** five accepted presses at intervals above cooldown, held input <=one, two-second glide, collision/boundary tolerance 0.05 m, two route cycles/60 s and two resets, no new errors; FoundationSmoke still toggles with isolated modes. **Manual:** rendering/keyboard/route; label test-run output separately. Fix core defects before P2.

## Phase 4 — US2: Quest button and Quest 2 trial (next priority)

**Independent test:** controller commands first in Editor, then the newly identified Quest flight APK; not the old smoke artifact.

- [ ] T010 [US2] Add `Assets/Flight/Input/Adapters/QuestFlightInput.cs`, extend Flight.inputactions with existing XRI left-hand movement binding and right trigger press, and connect existing XR Origin in FlightPrototype. **25 min. Depends:** T009. **Outcome/check:** same motor, one active source/camera/listener, one press/beat, no forced camera pitch/roll/yaw or tracked-child writes, scale=1, other locomotion writers disabled; record actual bindings. Start at <=3 m/s. **Manual:** simulator input/tracking check; no comfort claim.
- [ ] T011 [US2] Narrowly extend `Assets/Foundation/Editor/FoundationBuildGuard.cs` and native `Assets/Settings/Build Profiles/*.asset` scene selection through Editor for exactly one allowed smoke OR flight entry; keep all existing XR checks and choose `Builds/QuestStandalone/FlightPrototype.apk` for flight. **20 min. Depends:** T010. **Outcome/check:** flight preflight succeeds, mismatched/unknown scenes still reject, touch has no XR and Quest intended OpenXR, old smoke scene/output still selectable. No actual build until T012. **Manual:** profile UI review if needed.
- [ ] T012 [US2] Build the distinct Quest flight APK and test on Meta Quest 2; record artifact hash/device/OS, duration, tracking, input and initial comfort in `docs/foundation-validation.md`. **45 min plus reserve if needed. Depends:** T011 and actual device/trust access. **Outcome/check:** flight scene launches, trigger/steering work with no PC duplicates; separate build/device/performance/comfort outcomes. Stop on discomfort; stationary fallback or no measured performance is partial/pending, not a full pass. **Manual:** install/trust/headset trial and observations. Defer if the eight-hour limit is reached; never repeat foundation builds as flight evidence.

## Phase 5 — US3/US4: Optional experiments, outside committed budget

- [ ] T013 [US3] Prototype `Assets/Flight/Input/Adapters/MotionWingbeatInput.cs` using tracking-origin-local downward motion, tunable threshold and rearm/cooldown; select button OR motion source in FlightModeCoordinator. **45 extra min. Depends:** T012 completed safely and extra time authorized. **Outcome/check:** one downward stroke/one beat, upward return/jitter/invalid tracking do not fire; locomotion-origin movement is not a fake stroke, button fallback works. **Manual:** real controller stroke test; no gesture package. This is a required overall-project mechanic: if deferred in Week 2, record it as mandatory in a later milestone specification; button fallback does not close that requirement.
- [ ] T014 [US4] Add `Assets/Flight/Input/Adapters/TouchFlightInput.cs` and minimal flight-scene UI for steer/wingbeat with the existing Input System. **45 extra min. Depends:** T009 core and time authorized after next-priority disposition. **Outcome/check:** same motor, one tap/one command, pointer release/disable resets input, Quest source inactive. **Manual:** Editor mouse check; Android hardware result remains pending until actually tested.
- [ ] T015 [US4] Validate the optional touch adapter and inactive-source isolation; record touch results in `docs/foundation-validation.md` (including a separate Android device result only if actually run with an identified flight artifact). **30 extra min. Depends:** T014. **Outcome/check:** no duplicate input, hold/tap/release semantics correct; Editor mouse evidence does not pass Android touch. Unimplemented/unrun experiment stays unchecked. **Manual:** applicable device interactions; no automatic Android build requirement. Motion validation belongs to T013.

## Final phase — Core handoff and cross-cutting evidence

- [ ] T016 Update `docs/foundation-validation.md`, `docs/manual-setup.md`, `docs/ai-usage.md`, `docs/third-party-assets.md` if new sources exist, and README progress with actual outcomes/tuning/time spent/deferred scope; inspect authored Assets/.meta pairs. **30 min included in core. Depends:** T009 and disposition of any started P2/P3 work; does not require optional tasks to pass. **Outcome/check:** minimum demo reproducible, no old smoke result reused as flight result, Editor/build/device separation, references preserved, no temporary assets/build outputs eligible for Git. **Manual:** student reviews attribution, demonstration and pending limits. No automatic commit/push.

## Dependencies, estimates and execution strategy

```text
T001 -> T002 -> T003 -> T004 -> T005 -> T006 -> T007 -> T008 -> T009
                                                                |-> T016 (core close)
                                                                `-> T010 -> T011 -> T012
                                                                                      `-> T013
T009 + next-priority disposition -> T014 -> T015 -> T016 evidence update
```

Core T001–T009 (300 min) + T016 (30) = **330 min**. P2 T010–T012 = **90 min**.
Reserve = **60 min**; total planned ceiling **480 min / 8 hours**. Stretch T013–T015 adds
120 min and is not promised inside eight hours. Do not count post-build waiting as free work.
At hour 4 freeze art/defer Quest if core not controllable; at hour 6 protect PC fixes/handoff.
At hour 8 stop and report unchecked tasks; do not drop core safety/verification criteria. T016 must explicitly carry any unfinished T013 forward as a required later-milestone feature, not mark it optional or complete.

Story counts: US1 seven tasks, US2 three, US3 one, US4 two; setup/foundation two, handoff one.
No implementation task is marked complete at planning time.

Parallel example: after T009, evidence drafting can overlap waiting for the Quest trial if it
does not mutate Editor state or claim unfinished results. Scene/mock mutations are serialized
by their task dependencies in this solo workflow. No [P] markers promise unsafe scene/physics
parallel work or concurrent Editor writers.
