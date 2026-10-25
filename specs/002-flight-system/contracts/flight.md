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

Default PC contract: Space=takeoff if Grounded, otherwise wingbeat; A/D=yaw;
W/S=forward-speed target; R=reset. Neutral command glides/descends during flight.
Use the same motor instance when changing source. Caps and collider/world boundaries are
invariants; tests read the configured values, not an assumed achieved speed.

## Mode/camera contract

FlightPrototype gets explicit scene-local references, not FoundationSmoke's implicit bootstrap.
Use AndroidTouch/QuestStandalone product identity and BIRDLIFE_TOUCH/BIRDLIFE_QUEST defines;
Editor defaults PC/non-VR. Existing smoke scene behavior/profile policies must remain available.
Disable old input and rig before enabling the new set; repeated selection is idempotent.
Exactly one active Camera, AudioListener and flight command source. Android touch adapter
does not exist until stretch: PC keyboard remains an Editor aid, not a claim of Android flight.

Quest next-priority: right trigger press=activation; left-hand XRI movement Vector2 supplies
steering/throttle (record exact resolved binding when implemented). The smoke-test adapter is
disabled only on the reused flight rig instance. Existing XR Origin children remain tracked,
scale remains one, simulator stays EditorOnly, and no extra locomotion provider writes motion.
Initial XR follow translates origin with bird displacement without forced pitch/roll/yaw.
Reset is explicit; do not recenter/overwrite the tracked HMD pose as a hidden side effect.

## Guarded build selection (next-priority work only)

Native profiles keep distinct IDs and XR policies. Their sole enabled entry scene must be
exactly FoundationSmoke or FlightPrototype, never an arbitrary bypass or an accidental list
containing both. The existing guard retains scene/define/loader/startup/feature validation.
Flight output is Builds/QuestStandalone/FlightPrototype.apk (and, only if later built,
Builds/AndroidTouch/FlightPrototype.apk); foundation output paths stay unchanged.
Select the entry scene explicitly through Editor profile settings; restore/select the smoke
entry and validate it as a regression. No scene or profile changes occur during this planning.

## Evidence contract

For each implemented task record date, actual tuning/mode, Editor result, automated result,
build result, actual device/OS and manual observations separately. A flight build needs its
own artifact identity; earlier foundation APKs do not contain flight. No comfort/performance
pass without actual observations/measurements. Update existing validation and AI/asset docs;
all outstanding foundation acceptance remains outstanding until its own criteria pass.
