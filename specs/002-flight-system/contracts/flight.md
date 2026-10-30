# Flight and mode contract

## Shared behavior

BirdMotor exposes command submission/reset and read-only flight state. FlightDynamics owns
small deterministic velocity/heading integration for focused tests. Neither depends on
InputAction, XR tracking, UI, keyboard keys or scene/root-name lookup. BirdMotor is the sole
writer of FlightRoot movement via its CharacterController; visuals never move that root.

Commands have steer/throttle in [-1,1], activation press-edge and reset press-edge. The active
adapter samples once per frame. A press is consumed once, never once per physics substep;
cooldown rejects excess presses without buffering them. Reset takes precedence over a flap
in the same frame. Mode switch/focus loss discards pending edges. Flap does not require a
bird animator or gesture detector.

Sample before consumption with explicit ordering, once per render frame; do not rely on
unspecified Update order. Reset, source disable, focus loss and mode changes clear edges
and held intent; require release before rearming held activation/reset controls. Disabling
an InputAction alone does not clear a press already observed in the current frame.

CharacterController.Move takes displacement (velocity * deltaTime), supplies no gravity,
and runs once per movement frame. Supply gravity explicitly. Interpret post-Move flags:
Below while descending grounds/stops; Above cancels upward velocity; Sides alone does not
ground. Cancel blocked velocity without accumulation. Previous isGrounded must not cancel
takeoff. Reset clears contact bookkeeping and restores a valid grounded refuge launch pose.
Use stepOffset=0 for this prototype, record skinWidth/minMoveDistance and verify contacts.
Measure actual displacement separately from requested velocity; exclude explicit reset
teleports from speed measurement, but still validate their launch pose against bounds.

Optional Editor debugging contract: Space=takeoff if Grounded, otherwise wingbeat; A/D=yaw;
W/S=forward-speed target; R=reset. Neutral command applies downward acceleration; existing
upward momentum may continue until its apex, after which the bird descends.
Use the same motor instance when changing source. Caps and collider/world boundaries are
invariants; tests read the configured values, not an assumed achieved speed.

## Mode/camera contract

FlightPrototype gets explicit scene-local references, not FoundationSmoke's implicit bootstrap.
Use AndroidTouch/QuestStandalone product identity and BIRDLIFE_TOUCH/BIRDLIFE_QUEST defines;
Quest is primary; Editor may use the existing XR simulator or inexpensive keyboard debugging.
Existing smoke scene behavior/profile policies must remain available.
Disable old input and rig before enabling the new set; repeated selection is idempotent.
Exactly one active Camera and AudioListener; one command source when flight is implemented,
none during static observation. Android touch is secondary/later; Windows player is optional.
Reuse VRRig, disabling VRSmokeTest only on the flight instance. Its simulator activation
must remain Editor-only without relying on the disabled smoke adapter.

Static VR evaluation uses first-person near Head and third-person behind the visible bird,
with the same tracked headset camera. Configure offsets on an untracked parent/origin;
never overwrite tracked local poses, force pitch/roll, scale the rig or inherit wing animation.
Use a serialized default and one dedicated controller-button press to switch offsets during
static observation in the same standalone APK; record the binding in T007. This button
must not submit a wingbeat or modify tracked local poses. No extra rig or camera manager. Static scale
acceptance is separate from later immersive flight acceptance.

Give gameplay maps one activation owner. The rig InputActionManager must not enable inactive
PC/flight maps; retain tracking actions. Prepare serialized flight bindings before OpenXR
startup; mode selection enables/disables maps rather than creating late runtime bindings.

Quest flight: right trigger press=activation; left-hand XRI movement Vector2 supplies
steering/throttle (record exact resolved binding when implemented). The smoke-test adapter is
disabled only on the reused flight rig instance. Existing XR Origin children remain tracked,
scale remains one, simulator stays EditorOnly, and no extra locomotion provider writes motion.
Initial XR follow translates an independent origin by actual post-collision bird displacement,
without inheriting bird rotation or forcing pitch/roll/yaw; preserve tracked offsets.
Bird capsule bounds do not constrain physical head movement or establish room-scale safety.
Reset returns to the refuge launch, not the clearing. It is explicit; do not recenter/
overwrite the tracked HMD pose as a hidden side effect.

For required later motion wingbeats, detect both hands moving down deliberately and require
both hands' upward recovery before rearming. One beat requests bounded upward and forward
velocity under the shared motor caps. Thresholds are Inspector-configurable. The detector
supplies an edge to the existing Quest adapter, not a
second command producer. Button OR motion selects only activation; retain Quest steering.
Tracking loss/regain, recenter, first sample and mode/focus changes clear motion history.
Require a fresh valid baseline and rearm before accepting a stroke; pose discontinuities
and origin locomotion must not count as hand motion. Retain threshold/hysteresis/cooldown.
Reuse the installed XR Device/Interaction Simulator for simulated hand poses where supported;
exercise the actual detector, not a parallel mock detector. Editor-only Space uses the same
motor action. Optional concise detected/rearmed diagnostics suffice; no new framework,
debug UI, Meta XR Simulator or XR package changes. Validate real gestures/comfort on Quest 2.

## Guarded build selection (before static Quest trial, not during Step 1)

Native profiles keep distinct IDs and XR policies. Their sole enabled entry scene must be
exactly FoundationSmoke or FlightPrototype, never an arbitrary bypass or an accidental list
containing both. The existing guard retains scene/define/loader/startup/feature validation.
Flight output is Builds/QuestStandalone/FlightPrototype.apk (and, only if later built,
Builds/AndroidTouch/FlightPrototype.apk); foundation output paths stay unchanged.
Select the entry scene explicitly through Editor profile settings; restore/select the smoke
entry and validate relevant selection checks as a regression; do not repeat foundation builds.

## Evidence contract

For each implemented task record date, actual tuning/mode, Editor result, automated result,
build result, actual device/OS and manual observations separately. A flight build needs its
own artifact identity; earlier foundation APKs do not contain flight. No comfort/performance
pass without actual observations/measurements. Update existing validation and AI/asset docs;
all outstanding foundation acceptance remains outstanding until its own criteria pass.
