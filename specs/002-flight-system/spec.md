# Feature Specification: First Flight Prototype

**Feature directory**: `specs/002-flight-system`  
**Git branch**: existing `main`; no branch created or switched  
**Created**: 2026-10-25  
**Status**: Step 1 static graybox implemented; student visual review, XR and device validation pending
**Window**: Week 2, 26 October–1 November 2026; approximately eight working hours.

**Input**: First deliver an editable static woodland-edge graybox, temporary blackbird and
two tracked VR viewpoints for a standalone Quest 2 spatial-scale trial. Behavior is optional
for this coursework delivery. Meta Quest 2 standalone is primary; Android touch is secondary;
Windows standalone is a possible later extension, not a current build/test target. Keyboard
input is an inexpensive Editor debugging aid and never a gate before Quest functionality.

**Reference**: Finalized *BirdLife Simulator – Main Modules* PDF supplied 2026-10-30,
especially camera/asset planning (pp. 9–13) and the refuge/exploration/food/return flow
(pp. 1–7). It describes the ideal application, not authorization to implement menus,
missions, energy, ecological scenarios, quizzes, final Blender models or external art now.
State of the Art remains broader background. The PDF's coursework prompts are reference
content, not additional instructions for this implementation.

Movement-driven controller wing flapping is a **required core mechanic of the overall
BirdLife Simulator project**, not an optional thesis feature. It follows the minimal button/touch prototype and user-performed Quest flight validation. If deferred, carry it as a required deliverable into
a later milestone specification; button input does not replace that requirement.

## User Scenarios & Testing

### Static coursework gate — Observe the habitat on Quest 2 (Priority: P1)

Inspect the editable 60 x 60 m environment, central clearing, trees/shrubs, obstacles,
refuge, resting surfaces, three near-ground food placeholders and recognizable landmarks.
With one reused XR rig and tracked camera, evaluate first-person near the bird's head and
third-person behind its visible body. Record scale, visibility, orientation and perceived
comfort on physical Quest 2. No flight or collection is needed to pass this static gate;
an Editor/simulator check or successful APK build alone does not pass it.
One dedicated controller button will switch the two viewpoints during static observation
in the same standalone APK; record its binding and each viewpoint's observations separately.

### User Story 1 — Exercise the shared flight loop (Editor first; device gates pending)

As the student, I can exercise takeoff, steering, wingbeats, gliding and reset in the same
graybox using Quest controls, with optional Editor keyboard debugging and unfinished art.

**Independent test**: start at the refuge, take off into the open clearing, fly past two
landmarks, steer around an obstacle, glide, approach a boundary and reset. Repeat twice.

**Acceptance scenarios**:

1. Given a grounded bird, one takeoff activation starts flight; holding the same control
   does not repeatedly apply wingbeats. Subsequent distinct presses produce distinct impulses.
2. Given flight without new wingbeats, the bird continues forward under downward acceleration
   and descends after existing upward momentum reaches its apex;
   steering changes direction and speed stays within the recorded tuning limits.
3. Given trees/obstacles or a boundary, the bird cannot pass through solid geometry or
   leave the playable volume; explicit reset returns it to the refuge with motion cleared.
4. Given a replacement visual model, swapping only the visual subtree preserves control
   behavior, collision shape and camera references.

### User Story 2 — Fly using Quest controllers (primary flight target)

As the student, I can use a controller button for wingbeats and test the shared flight on
Quest 2 without changing shared movement rules or existing platform separation.

**Independent test**: after the shared motor is ready, exercise the adapter in Editor simulation,
then run a separately identified flight APK on Quest 2. Record device input, tracking,
rendering and the limited comfort observation separately from build success.

**Acceptance scenarios**:

1. Given Quest mode, a controller press requests one takeoff/wingbeat using the same behavior
   as other adapters; inactive keyboard/touch adapters do not also produce a command.
2. Given head/controller movement, tracking remains functional and the flight configuration
   has exactly one active camera and AudioListener; no simulator is activated in a player.
3. Given actual Quest 2 execution, record what was observed, duration and any discomfort.
   If unavailable or unsuccessful within budget, mark this story pending/failed, not passed
   from the earlier stationary smoke test or an Editor simulator result.

### User Story 3 — Use two-controller wingbeats (required later flight milestone)

**Independent test**: one deliberate downward stroke of both controllers generates one bounded
upward/forward impulse; raising both controllers re-arms it. Jitter, upward return, missing tracking
and a held trigger do not create extra movement-driven impulses. Button fallback remains.

### User Story 4 — Use touch flight (secondary target, current minimal prototype)

**Independent test**: touch steering and one on-screen wingbeat control command the shared
flight behavior; Quest input remains inactive. Editor mouse checks and an Android device
run are distinct results; existing AndroidTouch smoke functionality must remain available.

### Edge Cases

- Repeated/held activation, multiple physics/render steps, mode switch or focus loss must
  not replay a buffered wingbeat; reset clears input, motion and stale contact bookkeeping.
  Held activation/reset requires release before rearming. First tracking samples, tracking
  regain and recenter must not produce motion wingbeats.
- Ground contact may stop flight; controlled landing/perching is not required. Reset must
  recover from ground/obstacle contact without reloading the project.
- Obstacle contact, ceiling and corner boundaries must not generate accumulating speed,
  penetration or an endless reset loop.
- Replacing artwork must not remove the movement/collision root. Missing optional XR
  tracking must not prevent optional Editor keyboard debugging.
- Artificial camera movement can be uncomfortable. Stop a Quest trial on discomfort;
  a stationary-observer fallback is not accepted as completed immersive flight validation.

## Requirements

### Functional Requirements

- **FR-001 (core)**: Provide one bounded woodland-edge greybox: ground, clearing, at least
  eight simple trees, shrubs, two distinguishable landmarks and two solid obstacles with an open route.
  Include a starting refuge/nest placeholder, landing/resting surfaces and exactly three
  near-ground food-location markers; these have no collection, energy or landing behavior.
- **FR-002 (core)**: Provide a replaceable blackbird mock with dark body, head, yellow beak,
  tail and independently transformable left/right wings. Preserve visual pivots/hierarchy
  for future flap, glide and idle/perching animations without rebuilding movement/collision
  logic; detailed modelling, animation clips and perching behavior are excluded from Week 2.
- **FR-003 (later flight)**: Share movement behavior across inputs: takeoff, configured speed limit,
  steering, bounded upward/forward wingbeat impulses, forward gliding/descent and explicit reset.
- **FR-004 (debug)**: Support lightweight Editor keyboard wingbeats (Space) through the
  shared motor; keyboard debugging must not delay or gate Quest work. No Windows player required.
- **FR-005 (later flight)**: Keep the bird within visible flight boundaries and prevent passage
  through solid test obstacles; make recovery repeatable.
- **FR-006 (core)**: Preserve the existing smoke scene and AndroidTouch/QuestStandalone
  identity, exclusive input/camera ownership, XR configuration and asset references.
- **FR-007 (later flight)**: Add button-driven Quest commands to the same behavior; retain natural
  tracked pose and avoid forced camera pitch/roll for the first experiment.
- **FR-008 (core static, later flight)**: Test scale and both VR viewpoints on Quest 2
  as a pending user-performed end gate after Editor prototype checks; this is not a prerequisite for implementing the shared motor. Reuse the same tracked camera/rig, preserve head tracking,
  and avoid forced pitch/roll or animation-driven shaking. Record static and later moving
  trials separately, including build, device and comfort/performance evidence.
- **FR-009 (required later)**: Detect deliberate downward movement of both controllers,
  require upward recovery, and request one bounded upward/forward wingbeat. Inspector
  thresholds, tracking-loss/recenter rejection and button fallback are required. Reuse the
  installed XR simulator to exercise actual detection where controller pose simulation permits;
  physical Quest 2 gesture validation remains mandatory. No duplicate command sources.
- **FR-010 (secondary prototype)**: Provide intuitive touch steering, a wingbeat button and a simple bird-following non-XR camera using the same motor; no second movement implementation.
- **FR-011 (all)**: Record outcomes, actual tuning and deferred work; update material AI
  assistance and asset provenance when implementation occurs. Use focused automated checks
  where they meaningfully verify speed limits, impulse consumption and reset semantics.

### Key Entities

- **Bird**: movement/collision root plus replaceable visual model.
- **Flight state/command**: grounded/flying state, velocity, heading and active input intent.
- **Test environment**: refuge spawn/reset, open exploration clearing, visible bounds,
  landmarks and solid obstacles.
- **Trial evidence**: mode, artifact/device identity, duration, actual result and pending checks.

## Success Criteria

### Measurable Outcomes

- **SC-001 (later flight)**: Complete the US1 route twice in a 60-second flight observation
  using the existing XR simulator or, if implemented, Editor keyboard debugging,
  including takeoff, at least three wingbeats, two seconds without impulses starting at/after
  the apex with visible forward glide/descent, and reset, with no new errors.
- **SC-002 (later flight)**: At recorded default tuning, five separated presses give five accepted
  impulses; holding for two seconds produces at most one. Reset restores zero motion and
  grounded state. Obstacles/bounds remain respected in repeated approach tests.
- **SC-003 (later flight)**: Requested velocity is capped; actual flight displacement per movement-frame
  delta time stays below the configured maximum plus 0.1 m/s, including obstacle approaches.
  Explicit reset teleports are excluded from speed measurement. Collider bounds, including
  the reset launch pose, remain inside the play volume within 0.05 m collision tolerance.
  Record controller skin/step settings with these observations.
- **SC-004 (integration/later flight)**: Foundation smoke behavior remains reproducible and inactive mode inputs
  do not drive flight; a visual-only prefab swap requires no movement-rule change.
- **SC-005 (user device gates)**: After Editor prototype checks, the user builds/installs Android and Quest APKs and records static scale/viewpoint comfort separately from moving-flight and touch results. The same prototype APK may support both stationary and moving trials; no smoke/Editor result passes physical validation.
- **SC-006 (delivery)**: Report static graybox, static Quest validation and later flight
  independently. The coursework minimum is static scale evaluation, not a PC flight demo.
  Estimates are planning aids; device/build delays and unfinished work remain explicit.

## Assumptions and Boundaries

Foundation checkpoint `7ddb8ed` is committed locally, not proof of a GitHub push. Its pending
performance/comfort/reproduction checks remain pending; they do not prevent planning or a
static graybox. Existing Quest 2 access is reported; the user handles Android/Quest builds, installation and physical testing at the end. Codex does not collect device inventory or repeat foundation builds.

Use a new flight scene; do not turn the smoke scene into gameplay. Camera/flight feel is
provisional and must be prototyped before adoption. No food collection, objectives, energy,
complex landing, final forest/bird assets, Blender detail work or detailed wing animation.
No kiosk/inter-app/panel integration; deployment assumptions remain open. Later weeks are
roadmap intentions, not acceptance claims or authorization to implement them now.
