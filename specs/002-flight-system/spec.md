# Feature Specification: First Flight Prototype

**Feature directory**: `specs/002-flight-system`  
**Git branch**: existing `main`; no branch created or switched  
**Created**: 2026-10-25  
**Status**: Planned, not implemented  
**Window**: Week 2, 26 October–1 November 2026; approximately eight working hours.

**Input**: A controllable placeholder blackbird flying through a small woodland-edge test
environment in the Editor. PC flight is the minimum demonstration; Quest button input and
a Quest 2 flight test are next priority; movement-driven wingbeats and touch flight are stretch.

Movement-driven controller wing flapping is a **required core mechanic of the overall
BirdLife Simulator project**, not an optional thesis feature. Its stretch priority applies
only to Week 2 scheduling. If deferred, carry it as an explicit required deliverable into
a later milestone specification; button input does not replace that requirement.

## User Scenarios & Testing

### User Story 1 — Fly a mock blackbird with a keyboard (Priority: P1)

As the student, I can demonstrate takeoff, steering, wingbeats, gliding and reset in a
readable woodland greybox without a headset or finished artwork.

**Independent test**: start the flight scene, take off from the clearing, fly past two
landmarks, steer around an obstacle, glide, approach a boundary and reset. Repeat twice.

**Acceptance scenarios**:

1. Given a grounded bird, one takeoff activation starts flight; holding the same control
   does not repeatedly apply wingbeats. Subsequent distinct presses produce distinct impulses.
2. Given flight without new wingbeats, the bird continues forward and gradually descends;
   steering changes direction and speed stays within the recorded tuning limits.
3. Given trees/obstacles or a boundary, the bird cannot pass through solid geometry or
   leave the playable volume; explicit reset returns it to the clearing with motion cleared.
4. Given a replacement visual model, swapping only the visual subtree preserves control
   behavior, collision shape and camera references.

### User Story 2 — Try the same flight using Quest controllers (Priority: P2)

As the student, I can use a controller button for wingbeats and test the shared flight on
Quest 2 without changing the PC behavior or existing platform separation.

**Independent test**: after US1, first exercise the controller adapter in Editor simulation,
then run a separately identified flight APK on Quest 2. Record device input, tracking,
rendering and the limited comfort observation separately from build success.

**Acceptance scenarios**:

1. Given Quest mode, a controller press requests one takeoff/wingbeat using the same behavior
   as PC; inactive keyboard/touch adapters do not also produce a command.
2. Given head/controller movement, tracking remains functional and the flight configuration
   has exactly one active camera and AudioListener; no simulator is activated in a player.
3. Given actual Quest 2 execution, record what was observed, duration and any discomfort.
   If unavailable or unsuccessful within budget, mark this story pending/failed, not passed
   from the earlier stationary smoke test or an Editor simulator result.

### User Story 3 — Experiment with movement-driven wingbeats (Priority: P3, stretch)

**Independent test**: in a separately selectable experimental input mode, one deliberate
downward controller stroke generates one wingbeat; jitter, upward return, missing tracking
and a held trigger do not create extra movement-driven impulses. Button fallback remains.

### User Story 4 — Experiment with touch flight (Priority: P3, stretch)

**Independent test**: touch steering and one on-screen wingbeat control command the shared
flight behavior; Quest input remains inactive. Editor mouse checks and an Android device
run are distinct results; existing AndroidTouch smoke functionality must remain available.

### Edge Cases

- Repeated/held activation, multiple physics/render steps, mode switch or focus loss must
  not replay a buffered wingbeat; reset clears input and motion.
- Ground contact may stop flight; controlled landing/perching is not required. Reset must
  recover from ground/obstacle contact without reloading the project.
- Obstacle contact, ceiling and corner boundaries must not generate accumulating speed,
  penetration or an endless reset loop.
- Replacing artwork must not remove the movement/collision root. Missing optional XR
  tracking must not affect the PC minimum demonstration.
- Artificial camera movement can be uncomfortable. Stop a Quest trial on discomfort;
  a stationary-observer fallback is not accepted as completed immersive flight validation.

## Requirements

### Functional Requirements

- **FR-001 (core)**: Provide one bounded woodland-edge greybox: ground, clearing, at least
  eight simple trees, two distinguishable landmarks and two solid obstacles with an open route.
- **FR-002 (core)**: Provide a replaceable blackbird mock with dark body, yellow beak,
  tail and independently transformable left/right wings. Preserve visual pivots/hierarchy
  for future flap, glide and idle/perching animations without rebuilding movement/collision
  logic; detailed modelling, animation clips and perching behavior are excluded from Week 2.
- **FR-003 (core)**: Share movement behavior across inputs: takeoff, configured speed limit,
  steering, discrete wingbeat lift, forward gliding/descent and explicit reset.
- **FR-004 (core)**: Provide keyboard control of the complete loop and a readable non-VR view.
- **FR-005 (core)**: Keep the bird within visible flight boundaries and prevent passage
  through solid test obstacles; make recovery repeatable.
- **FR-006 (core)**: Preserve the existing smoke scene and AndroidTouch/QuestStandalone
  identity, exclusive input/camera ownership, XR configuration and asset references.
- **FR-007 (next)**: Add button-driven Quest commands to the same behavior; retain natural
  tracked pose and avoid forced camera pitch/roll for the first experiment.
- **FR-008 (next)**: Attempt a real Quest 2 flight test only after the PC gate; record
  build, device function and comfort/performance evidence as separate outcomes.
- **FR-009 (stretch)**: Downward-motion detection is opt-in, with threshold, rearming,
  tracking-loss handling and button fallback; no simultaneous duplicate command sources.
- **FR-010 (stretch)**: Add touch commands without a second movement implementation.
- **FR-011 (all)**: Record outcomes, actual tuning and deferred work; update material AI
  assistance and asset provenance when implementation occurs. Use focused automated checks
  where they meaningfully verify speed limits, impulse consumption and reset semantics.

### Key Entities

- **Bird**: movement/collision root plus replaceable visual model.
- **Flight state/command**: grounded/flying state, velocity, heading and active input intent.
- **Test environment**: spawn clearing, visible bounds, landmarks and solid obstacles.
- **Trial evidence**: mode, artifact/device identity, duration, actual result and pending checks.

## Success Criteria

### Measurable Outcomes

- **SC-001 (core)**: Complete the US1 route twice in a 60-second Editor flight observation,
  including takeoff, at least three wingbeats, a two-second glide and reset, with no new errors.
- **SC-002 (core)**: At recorded default tuning, five separated presses give five accepted
  impulses; holding for two seconds produces at most one. Reset restores zero motion and
  grounded state. Obstacles/bounds remain respected in repeated approach tests.
- **SC-003 (core)**: Actual speed never exceeds configured maximum plus 0.1 m/s tolerance;
  collider bounds remain inside the play volume within 0.05 m collision tolerance.
- **SC-004 (core)**: Foundation smoke behavior remains reproducible and inactive mode inputs
  do not drive flight; a visual-only prefab swap requires no movement-rule change.
- **SC-005 (next)**: One identified Quest flight build and one actual Quest 2 trial have
  explicit outcomes; no stationary-smoke evidence is relabelled as a flight/comfort pass.
- **SC-006 (delivery)**: Core, next and stretch outcomes are reported separately against
  the eight-hour timebox. A PC-only core demonstration is a valid minimum delivery.

## Assumptions and Boundaries

Foundation checkpoint `7ddb8ed` is committed locally, not proof of a GitHub push. Its pending
performance/comfort/reproduction checks remain pending; they do not prevent planning or a
PC prototype. Existing Quest 2 access is reported; actual availability/firmware for the new
trial must be reconfirmed. Samsung S24 Ultra touch flight is optional this week.

Use a new flight scene; do not turn the smoke scene into gameplay. Camera/flight feel is
provisional and must be prototyped before adoption. No food collection, objectives, energy,
complex landing, final forest/bird assets, Blender detail work or detailed wing animation.
No kiosk/inter-app/panel integration; deployment assumptions remain open. Later weeks are
roadmap intentions, not acceptance claims or authorization to implement them now.
