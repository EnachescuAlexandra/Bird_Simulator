# Selected genuine AI-workflow excerpts

These are selected verbatim slices from user messages in an original Codex session log and the current conversation, not reconstructed prompts. IDE headers and all unselected surrounding content are omitted. The English wording and Markdown in each selected slice are unchanged; result paragraphs below are editorial summaries, not transcript quotations. Source aliases and message line numbers locate the earlier excerpts; later entries identify their source messages in the current conversation. No raw logs, hidden reasoning, tool payloads, screenshots, account paths or device identifiers are published.

Source S5 is defined in [sources.md](sources.md). Only the quoted slices, not complete messages, are approved for publication in this review. These excerpts show instructions and reports; they do not independently prove APK behavior or human code review.


## E01 — Inspect first and preserve the foundation

Source: S5, JSONL line 12, user message.

> **Do not rebuild, reconfigure or unnecessarily retest the completed foundation.**

**Result / interpretation:** The foundation was retained as a technical reference; later work evolved FlightPrototype instead of replacing the smoke scene. See c48c7a9 and b31efbc.

## E02 — Shared motor, separate inputs

Source: S5, JSONL line 12, user message.

> Use one shared flight motor with platform-specific input adapters.

**Result / interpretation:** FlightCommand/BirdMotor/FlightDynamics form the shared core; separate keyboard, Quest and touch adapters submit commands. See b31efbc and 50cca6b.

## E03 — Require genuine editable geometry

Source: S5, JSONL line 484, user message.

> Create genuine ProBuilder meshes with editable vertices, edges and faces. Do not rename ordinary Unity primitives to make them appear to be ProBuilder objects.

**Result / interpretation:** The scene contains actual ProBuilder meshes. The student subsequently confirmed editability; procedural primitives remain where appropriate. Evidence is in the T018 record.

## E04 — Challenge the camera-mode design

Source: S5, JSONL line 1269, user message.

> **Alternative:** reuse or minimally adapt FoundationMode in the existing FlightPrototype if this achieves the exact same behavior with fewer changes.

**Result / interpretation:** Direct FoundationMode reuse replaced the parallel StaticGrayboxView behavior. No third permanent scene was created. This was a design correction driven by the student, not an unsolicited architecture rewrite.

## E05 — Human Editor validation

Source: S5, JSONL line 2812, user message.

> - Wingbeat triggers one impulse per press, without repeating when held.

**Result / interpretation:** This is a student report of T014 Editor behavior, not an automated device test. Other touch checks are recorded in the same message and validation report.

## E06 — Physical Quest feedback and fallback

Source: S5, JSONL line 3118, user message.

> - Flight works using the left joystick, right trigger and real two-controller wingbeat gestures.

**Result / interpretation:** Recorded as functional physical Quest evidence. The same report says trigger fallback helps when arm movement is tiring; no comfort or performance certification follows.

## E07 — Physical Android feedback

Source: S5, JSONL line 3167, user message.

> Android: the APK launches and basic touch flight controls work.

**Result / interpretation:** Recorded as basic physical Android functionality. Simultaneous multi-touch and interruption edge cases remain follow-up validation, not passes.

## E08 — A limitation becomes future investigation

Source: S5, JSONL line 3167, user message.

> when restarting or re-entering the application on Quest 2, the user's initial viewpoint is sometimes offset relative to the bird, placing the user beside it rather than at the intended first-person or third-person position.

**Result / interpretation:** Recorded under T020. Tracking-origin initialization, recentering and viewpoint offsets are investigation targets, not established causes. Camera behavior was not changed.

## E09 — Seated-first VR viewpoint work

Source: current conversation, T020 diagnosis request, followed by Stage 1/Stage 2 implementation requests.

> Professor feedback: The experience should be playable mostly while seated, without requiring the user to physically turn or walk to follow the bird.

**Result / interpretation:** Codex inspected the existing rig and implemented bird-relative viewpoint placement, first/third-person switching, long-X recenter and optional gradual turn-follow while preserving HMD tracking. The student confirmed simulator behavior and later physically tested the viewpoint on Quest. Occasional incomplete yaw alignment after large turns remains open; comfort/recovery acceptance is not claimed.

## E10 — Shared ground locomotion

Source: current conversation, T019 implementation request.

> Reuse the shared BirdMotor and existing Quest, Android and keyboard input adapters.

**Result / interpretation:** Codex added configurable slow ground movement to the shared dynamics, preserving flight and walking–flight transitions. The student confirmed Editor/XR Device Simulator ground walking; this is not a physical Android/Quest regression pass.

## E11 — One reusable food portion

Source: current conversation, feature 003 implementation request.

> Scope: ONE collectible food portion only, as a small, reusable foundation for future feeding missions.

**Result / interpretation:** Codex reused one feeding placeholder, added automatic ground-only proximity collection and visual feedback, and later moved FoodCollectible into Assets/Gameplay/Runtime with its GUID/references preserved. The student confirmed collection, visual disappearance, feedback, single collection and Return to Nest persistence in XR Device Simulator. Flight-only rejection and physical food acceptance remain pending.

## E12 — Physical observations guide follow-up

Source: current conversation, focused end-of-week review request.

> The latest Quest APK builds and runs successfully.

**Result / interpretation:** The same student report identified occasional yaw misalignment after large turns, possible tree collision gaps and controller visibility near ground level. Codex performed a focused read-only diagnosis and recorded follow-up checks; it did not generate a build or claim that these issues were fixed. This week's requested prototype work is implemented, while physical validation continues.
