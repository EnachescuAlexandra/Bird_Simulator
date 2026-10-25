# Planned flight validation guide

Not executable functionality yet. Do not run installation/build commands merely because
this guide exists. Reuse [foundation setup](../../docs/manual-setup.md) and installed versions.
All scenarios below are pending implementation; only document review has occurred.

## Core Editor demonstration

After T001–T009: open Assets/Scenes/FlightPrototype.unity with no headset. Confirm clearing,
eight trees, two landmarks, two solid obstacles, visible bounds and mock body/beak/tail/wings.
Play: Space takes off once; A/D steer, W/S adjust speed, distinct Space presses flap.
No new flap for two seconds must visibly glide/descend. R resets to the clearing.
Perform the route past both landmarks twice during a 60-second observation. Approach
each side/corner/ceiling and a tree/obstacle: no escape, penetration or velocity buildup.
Ground contact may stop flight; R recovers. Holding Space two seconds must not repeat flap.
Record actual tuning/speed, count and bounds tolerances from the spec, not assumed results.

Run the focused EditMode motion tests after implementation: cap, no-input descent, five
separated accepted presses, held/rejected edge behavior and reset. Then reopen FoundationSmoke
and reproduce its basic toggle/mode isolation; no new action should double-toggle the target.

## Next: Quest input and actual device

Only after core gate: choose Quest mode in the flight scene, use the recorded left-hand
movement action and right trigger for activation; no PC duplicate commands. Check one camera/
listener, tracked children and Editor-only simulator. Follow the contract's narrow guard/profile
scene changes before any build; FlightPrototype entry must pass XR/profile validation.

When a device test/build is separately authorized, create the distinct Quest flight artifact,
record its hash and install that artifact on Quest 2 (not the old smoke APK). Record firmware,
refresh setting, input/tracking, measured timings if available and duration. Begin with a short
low-speed trial; stop on discomfort. Record stationary-observer fallback as partial, not a
passed immersive-flight test. Do not inherit earlier stationary smoke comfort acceptance.

## Stretch and handoff

Motion wingbeats: explicitly select button OR downward-motion source; verify one stroke/one
beat, rearm, jitter/upward-return rejection and tracking-loss handling. Touch: steer/beat with
mouse in Editor first; actual Android touch remains pending until separately run.

After each authorized stage update docs/foundation-validation.md, docs/ai-usage.md and
docs/manual-setup.md with completed checks, elapsed work, failures and deferred tasks.
No builds/devices/tests have passed for feature 002 at planning time.
