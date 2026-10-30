# Planned flight validation guide

Step 1 implements only the static scene; bird, XR, viewpoints and gameplay remain pending.
Reuse [foundation setup](../../docs/manual-setup.md) and installed versions. Do not execute
later steps merely because this guide describes them.

## Static graybox review (Step 1)

Open Assets/Scenes/FlightPrototype.unity in Scene view (no runtime camera yet). Ground is
60 x 60 m at y=0; clearing 20 x 20 m. Inspect trees/shrubs, two obstacles, large-tree and
standing-stone landmarks, refuge/nest, resting surfaces and three near-ground food markers.
Perimeter strips and four 12 m corner markers indicate proposed flight limits; no roof or
motor boundary behavior. Check open corridors and landing approach space. Edit saved objects
directly: the one-time builder refuses existing output and never regenerates manual edits.
The refuge's FutureBirdStart is the planned start/reset location; the clearing is the open
exploration area. Before later collision/flight tests, resolve the known shrub/boulder/stump
collider mismatch recorded in docs/foundation-validation.md; visual review alone does not pass it.

## Static Quest scale gate (Steps 2–4, pending)

Add primitive bird and one reused VRRig. Disable its smoke adapter only on this instance,
preserving Editor-only activation of the existing simulator. Configure first/third-person
offsets for the same tracked camera: no pitch/roll/shake, tracked-pose writes or extra rig.
Use one dedicated controller button to switch viewpoints in the same standalone APK;
record its binding and observations for each view. The button does not issue a flight action.
Check simulator tracking, one camera/listener and disabled old locomotion. Narrow the build
guard to one smoke OR flight entry, preserving XR policies and old artifact paths. Build a
distinct static APK and record physical Quest 2 scale, visibility, orientation, tracking,
duration and perceived comfort for both viewpoints. No keyboard or flight prerequisite.
Static coursework acceptance is separate from moving-flight comfort/performance acceptance.

## Optional Editor keyboard debugging (later flight)

Only if T006 keyboard debugging is implemented, after the static gate and motor/input: open
FlightPrototype. This check is not a prerequisite for Quest input or its device trial. Confirm clearing,
eight trees, two landmarks, two solid obstacles, visible bounds and mock body/beak/tail/wings.
Play: Space takes off once; A/D steer, W/S adjust speed, distinct Space presses flap.
After upward momentum reaches its apex, observe two seconds without impulses with forward
glide/descent. Do not require immediate descent during ascent. R restores a valid grounded
refuge launch pose with motion/contact bookkeeping cleared. Take off from the refuge
and use the clearing as the open exploration area.
Perform the route past both landmarks twice during a 60-second observation. Approach
each side/corner/ceiling and a tree/obstacle: no escape, penetration or velocity buildup.
Ground contact may stop flight; R recovers. Holding Space two seconds must not repeat flap.
Verify side contact alone does not ground, ceiling contact cancels upward velocity, and
takeoff is not cancelled by stale ground state. Reset/focus/mode changes while activation
is held must not replay input until release. Measure actual speed, excluding reset teleports.
Record actual tuning/speed, count and bounds tolerances from the spec, not assumed results.

Run the focused EditMode motion tests after implementation: cap, no-input descent, five
separated accepted presses, held/rejected edge behavior and reset. Recheck smoke isolation
only where actual shared changes justify it, without rebuilding the completed foundation.

## Later Quest flight input and real device validation (T017)

After shared motor implementation, without waiting for keyboard debugging: use the recorded left-hand
movement action and right trigger for activation; no PC duplicate commands. Check one camera/
listener, tracked children and Editor-only simulator. Follow the contract's narrow guard/profile
scene changes before any build; FlightPrototype entry must pass XR/profile validation.
Turning the bird must not rotate XR Origin; blocked movement must not continue moving it.
Check reset/mode entry and map ownership. Bird bounds do not constrain physical head movement.

When the flight stage is requested, execute T017 after T009/T010 and before T013 gesture tuning.
Create the distinct Quest flight artifact,
record its hash and install that artifact on Quest 2 (not the old smoke APK). Record firmware,
refresh setting, input/tracking, measured timings if available and duration. Begin with a short
low-speed trial; stop on discomfort. Record stationary-observer fallback as partial, not a
passed immersive-flight test. Do not inherit earlier stationary smoke comfort acceptance.
Repeat the US1 route with button/joystick controls from the refuge, including glide and reset;
record input/tracking and moving-flight comfort separately from the static T012 trial.

## Required later gestures, secondary touch and handoff

Motion wingbeats: deliberate downward movement of both controllers gives one bounded upward/
forward beat; raising both re-arms. Verify jitter/upward-return rejection and tracking loss.
Use Inspector thresholds and installed XR Device/Interaction Simulator poses to exercise the
actual detector where supported; Space uses the same motor action. Optional detected/rearmed
messages only; no custom framework/debug UI or Meta XR Simulator. Real Quest 2 gestures and
comfort require separate hardware testing. Select activation within
the existing Quest adapter, retaining steering. Tracking regain, recenter and first sample
must clear history and require a fresh baseline/rearm; origin movement must not fake a stroke.
Touch: steer/beat with
mouse in Editor first; actual Android touch remains pending until separately run.

Record completed checks and pending manual work in docs/foundation-validation.md; update
docs/ai-usage.md for material assistance and docs/manual-setup.md only when setup changes.
Static Editor evidence is separate; no Quest build/device or flight pass is implied.
