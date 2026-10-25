# Flight state and configuration

No persistence/network data. All proposed types live under Assets/Flight/Runtime/.

| Entity | Fields / constraints | Relationships |
| --- | --- | --- |
| FlightCommand | steer and throttle normalized to [-1,1]; takeoff/wingbeat edge; reset edge | One active adapter submits; motor consumes |
| FlightState | Grounded or Flying; finite velocity; finite yaw; remaining flap cooldown >=0; accepted wingbeat count >=0, excluding takeoff and zeroed by reset | Owned by BirdMotor; no device references |
| FlightTuning | forward min >=0, max >=min; total speed cap >0; vertical min <=0 and max >0; gravity >=0; impulse >0; cooldown >0 | Serialized fields, not a new tuning service or asset system |
| FlightWorld | launch pose, collider-containing axis-aligned bounds with positive extents, solid obstacle layers | Scene configuration supplied to motor |
| MockVisual | Body, YellowBeak, Tail, independently transformable LeftWing and RightWing with rotation pivots under one replaceable child | Supports future flap/glide/idle-perching visuals without motor changes; no input/collision/gameplay authority |
| ModeBinding | AndroidTouch or QuestStandalone identity; exclusive rig/input references | Scene-local coordination, existing build defines |

Grounded + activation -> Flying with one takeoff impulse. Flying + accepted press -> one
wingbeat; held input or cooldown rejection does not queue a later extra beat. Flying + no
press -> gravity/glide. Ground contact -> Grounded and stopped. Reset from any state clears
velocity/cooldown/queued input and restores launch pose. Mode disable/focus loss clears input
edges so they are not replayed on reactivation; mode switching does not create another motor.

The actual capsule displacement can be zero at obstacles even if nominal minimum forward
speed is positive; collision blocking takes precedence. Clamp total requested speed after
steering/impulses, then constrain movement to bounds/collisions. Invalid/non-finite tuning
must produce a clear validation error and no uncontrolled movement, not silently escape caps.
