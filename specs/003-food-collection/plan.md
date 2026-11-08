# Implementation plan

Use `BirdLife.Gameplay.FoodCollectible` in `Assets/Gameplay/Runtime/FoodCollectible.cs`, outside the flight core assembly. Retain the existing Assembly-CSharp assembly; its auto-reference to BirdLife.Flight allows `using BirdLife.Flight` without a new asmdef. Preserve the script's original .meta GUID when moving it, so the existing scene component and Bird/Food Visual/Feedback references remain attached. In LateUpdate, read BirdMotor.IsFlying and CharacterController.isGrounded after movement resolves, then compare squared 3D distance to the configured radius. Latch collection before hiding the separate food visual.

Reuse location 01 without changing its transform, mesh, collider layout or the other two locations. Add one non-interactive world-space Canvas/Text, using the installed uGUI and built-in font; face the active camera only for feedback. No event system or raycaster is needed. Retain serialized scene references and Unity-generated metadata.

Verify compilation, scene wiring and the relevant collection conditions where Editor access permits. Manual Editor/simulator checks precede physical Quest/Android checks. Do not build or change T019/T020 validation status.
