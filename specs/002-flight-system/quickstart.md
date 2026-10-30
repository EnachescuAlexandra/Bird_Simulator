# Planned flight validation guide

Static graybox and mock bird are implemented; scene-local Editor mode selection is available. Two VR viewpoints are Editor-verified; the shared motor and Editor keyboard are checked; Quest button/joystick flight is Editor-verified; touch input and device validation remain pending.
Reuse [foundation setup](../../docs/manual-setup.md) and installed versions. Do not execute
later steps merely because this guide describes them.

## Static graybox review (Step 1)

Open Assets/Scenes/FlightPrototype.unity in Scene view. Ground is
60 x 60 m at y=0; clearing 20 x 20 m. Inspect trees/shrubs, two obstacles, large-tree and
standing-stone landmarks, refuge/nest, resting surfaces and three near-ground food markers.
Perimeter strips and four 12 m corner markers indicate proposed flight limits; no roof or
motor boundary behavior. Check open corridors and landing approach space. Edit saved objects
directly: the one-time builder refuses existing output and never regenerates manual edits.
The refuge's FutureBirdStart is the planned start/reset location; the clearing is the open
exploration area. T018 corrected the simple colliders; actual movement/contact validation remains a later flight check.

## Editor static preview (T004/T008)

Select StaticObservation and use Foundation Mode > Selected Mode before or during Play.
Android Touch activates the non-XR static camera: woodland and bird should be visible.
Quest Standalone activates the single reused VRRig and its existing Editor simulator.
During the same Play session, switch Android Touch -> Quest Standalone -> Android Touch.
The Inspector is the existing workflow; no gameplay switching menu is implemented; flight input is mode-specific.

Focus Game View. These bindings come from the rig's installed XR Device Simulator Controls:

- U selects body/FPS; mouse looks around, W/A/S/D translates, Q/E changes height.
- V resets selected device poses. For a downward-facing view on a fresh Play session,
  press Escape, hold right mouse and press V with no axis-constraint keys held. This resets
  head rotation in the default non-position target mode. Avoid U+V for rotation-only reset:
  FPS reset also changes position, which can place the bird-height observer below ground.
  Restart Play if you previously translated/reset the observer position.
- For head-only rotation, stop other selection with Escape, hold right mouse and Left Ctrl,
  then move the mouse (Ctrl forces rotation; the installed simulator starts in rotation mode).
- For independent controller translation, Escape, then hold Left Shift for left or Space
  for right; press 3 once to select device-position control, then use W/A/S/D and Q/E.
  Release the selection key before testing the other hand. The initial 2D target is joystick input.
  R toggles mouse translation/rotation; do not toggle it before the Ctrl instruction above.

In Quest mode, select the right controller with Y and press N to switch first/third-person
(the installed simulator secondary-button binding). On Quest hardware this is right B,
`<XRController>{RightHand}/secondaryButton`; physical validation remains pending. The serialized
VR Viewpoint > Perspective setting defaults to First Person. Only the untracked origin moves;
head rotation stays tracked. Android preview is unaffected; the view action is inactive there.
Check one active camera/AudioListener in either mode and no active VRSmokeTest on the flight
instance. The bird starts at FutureBirdStart; inspect its Visual subtree and separate wing
pivots without modifying the stable prefab root. Physical-device validation remains T012.

## User-performed static Quest scale gate (T012, pending at the end)

The mock bird and one reused VRRig are present. Its smoke adapter is disabled only on this instance,
preserving Editor-only activation of the existing simulator. Configure first/third-person
offsets for the same tracked camera: no pitch/roll/shake, tracked-pose writes or extra rig.
Use one dedicated controller button to switch viewpoints in the same standalone APK;
record its binding and observations for each view. The button does not issue a flight action.
After T007 -> T002 -> T005 -> T006/T010 -> T014, run focused T009/T015 in Editor, then prepare the existing guard/profiles in T011. The user builds and installs Android/Quest APKs at the end. On Quest, check stationary scale/viewpoints, visibility, tracking and comfort before moving flight, recording the outcomes separately even when using the same prototype APK. Editor checks do not pass these physical gates. No Codex builds, device inventory or repeated foundation checks.

T006 keyboard flight is active only in Android/non-XR Editor mode with Game View focused. Quest input and XR movement-follow are Editor-verified under T010. Touch input and non-XR camera-follow remain pending under T014; the Android preview camera stays static.

## Editor keyboard flight (T006)

After T005/T006 motor/input implementation, without waiting for device testing: open
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

## Editor Quest flight (T010)

Select Quest Standalone, focus Game View, then press Y to select the right controller.
Click the left mouse button for each takeoff/wingbeat; holding it does not repeat.
Hold Left Shift while using A/D to steer and W/S to adjust throttle through the left joystick.
Press N for first/third-person and B to reset at the refuge. On hardware these are right B
(viewpoint) and right A (reset); right trigger activates flight. Space is not flight input in
Quest mode. Use the head-rotation reset above if looking downward. XR Origin follows actual
bird displacement without rotating with the bird; headset/controller poses remain tracked.
Physical comfort and hardware behavior remain unverified.

## Later Quest flight input and real device validation (T017)

After shared motor implementation, without waiting for keyboard debugging: use the recorded left-hand
movement action and right trigger for activation; no PC duplicate commands. Check one camera/
listener, tracked children and Editor-only simulator. Follow the contract's narrow guard/profile
scene changes before any build; FlightPrototype entry must pass XR/profile validation.
Turning the bird must not rotate XR Origin; blocked movement must not continue moving it.
Check reset/mode entry and map ownership. Bird bounds do not constrain physical head movement.

After T011, the user builds/installs the prototype and performs T012 stationary Quest checks, then T017 joystick/button flight checks and T015 Android touch checks. Start Quest flight at low speed; stop on discomfort. Record actual observations/issues and pending results, without mandatory device inventory, hashes or OS collection. These checks precede later T013 gesture tuning.

## Current touch prototype, required later gestures and handoff

Motion wingbeats: deliberate downward movement of both controllers gives one bounded upward/
forward beat; raising both re-arms. Verify jitter/upward-return rejection and tracking loss.
Use Inspector thresholds and installed XR Device/Interaction Simulator poses to exercise the
actual detector where supported; Space uses the same motor action. Optional detected/rearmed
messages only; no custom framework/debug UI or Meta XR Simulator. Real Quest 2 gestures and
comfort require separate hardware testing. Select activation within
the existing Quest adapter, retaining steering. Tracking regain, recenter and first sample
must clear history and require a fresh baseline/rearm; origin movement must not fake a stroke.
Touch (T014 before focused T009/T015): intuitive steering/wingbeat UI and the existing non-XR camera following the bird, all using the shared motor. Check mouse interaction and runtime mode/source isolation in Editor; actual Android touch is user-validated at the end.

Record completed checks and pending manual work in docs/foundation-validation.md; update
docs/ai-usage.md for material assistance and docs/manual-setup.md only when setup changes.
Static Editor evidence is separate; no Quest build/device or flight pass is implied.

## User build handoff (T011 prepared; no APK built by Codex)

Stop Play and wait for compilation/import. Both existing native profiles have Override Global Scene List enabled with only Assets/Scenes/FlightPrototype.unity checked.

1. For Android, choose **BirdLife > Select AndroidTouch**. Wait for synchronization, then **BirdLife > Validate Active Profile**. A successful message gives the expected output. In **File > Build Profiles**, confirm AndroidTouch is active and only FlightPrototype is enabled; choose Build and save to `Builds/AndroidTouch/FlightPrototype.apk` relative to BirdLifeSimulator/.
2. For Quest, choose **BirdLife > Select QuestStandalone**. Wait for compilation and the existing XR synchronization, then **BirdLife > Validate Active Profile**. Confirm QuestStandalone and its single FlightPrototype entry in Build Profiles; choose Build to `Builds/QuestStandalone/FlightPrototype.apk`.

Inspector Selected Mode controls Editor preview; the player uses the existing BIRDLIFE_TOUCH/BIRDLIFE_QUEST profile define. Do not manually reconfigure XR: the existing profile-selection workflow applies each policy. Only AndroidTouch preflight was executed in this preparation; Quest preflight must succeed after selection before its user-run build. Stop on a validation error.

FoundationSmoke remains available: in the selected profile's scene list, replace the flight entry with the sole enabled `Assets/Scenes/FoundationSmoke.unity`, then reselect the profile and validate. Original smoke filenames remain AndroidTouch.apk / QuestStandalone.apk in their corresponding folders. Never enable both entry scenes together.

Before accepting flight, complete the remaining manual T009/T013 checks in `docs/foundation-validation.md`. Physical Android multi-touch, Quest static scale/viewpoint comfort (T012), moving flight (T017) and real two-controller gestures remain pending user-performed checks. Build success alone does not pass these gates.
## Milestone closure — 2026-10-30

The student has built/launched FlightPrototype on Quest 2 and Android and confirms Quest joystick/trigger/real wingbeat flight plus basic Android touch flight. Weekly grayboxing is complete. Existing build handoff steps remain available; earlier pending notes describe that stage, not the current basic functional outcome. T001–T018 are closed for weekly delivery; unperformed detailed validation remains T021. Known restart/re-entry viewpoint offset, third-person viewing direction and lag are T020, with no camera changes now. See the [dashboard](../../docs/dashboard.html) and authoritative tasks for current status.