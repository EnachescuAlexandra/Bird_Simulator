# 003 — One food portion

## Scope and requirements

- Reuse FoodLocation_01/FoodPlaceholder in FlightPrototype. Only this portion is collectible; locations 02/03 remain placeholders.
- Collect automatically within a configurable 0.3 m 3D radius of the location, measured from the bird root. Require an enabled shared BirdMotor, enabled CharacterController, ground contact and no active flight.
- Each FoodCollectible collects once per scene session, hides only its referenced visual, and shows world-space uGUI text: `Food collected! (1/1)`.
- Each instance owns its own state and references; another portion can reuse the component. The 1/1 feedback describes this prototype, not a future shared mission counter.
- Component location: `BirdLifeSimulator/Assets/Gameplay/Runtime/FoodCollectible.cs`, namespace `BirdLife.Gameplay`. It reads the existing `BirdLife.Flight.BirdMotor`; gameplay remains outside the flight core.
- No new input, movement rules, managers, packages, animations or mission framework. Preserve T019/T020 and existing collisions. Return to Nest does not respawn food; scene reload starts a fresh session.

## Acceptance criteria

1. Walking into the radius hides the one food visual and displays the message.
2. Outside the radius, or airborne/falling without ground contact, food remains available.
3. Remaining nearby, leaving/re-entering, recentering, changing perspective or returning to the nest cannot collect it again.
4. Existing keyboard, Quest and touch adapters require no new binding. Takeoff, flight, landing and walking remain functional.
5. Message is readable in non-XR and XR views. Physical Quest readability/comfort and Android interaction remain pending until device tests.
