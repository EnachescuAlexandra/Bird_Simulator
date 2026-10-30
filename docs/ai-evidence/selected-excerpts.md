# Selected genuine AI-workflow excerpts

These are selected verbatim slices from user messages in an original Codex session log, not reconstructed prompts. IDE headers and all unselected surrounding content are omitted. The English wording and Markdown in each selected slice are unchanged; result paragraphs below are editorial summaries, not transcript quotations. Sources/timestamps and message line numbers allow the student to locate originals. No raw logs, hidden reasoning, tool payloads, screenshots, account paths or device identifiers are published.

Source S5 is defined in [sources.md](sources.md). Only the quoted slices, not complete messages, are approved for publication in this review. These excerpts show instructions and reports; they do not independently prove APK behavior or human code review.


## E01 — Inspect first and preserve the foundation

Source: S5, JSONL line 12, user message, timestamp 2026-10-30T11:45:27.717Z (UTC as logged).

> **Do not rebuild, reconfigure or unnecessarily retest the completed foundation.**

**Result / interpretation:** The foundation was retained as a technical reference; later work evolved FlightPrototype instead of replacing the smoke scene. See c48c7a9 and b31efbc.

## E02 — Shared motor, separate inputs

Source: S5, JSONL line 12, user message, timestamp 2026-10-30T11:45:27.717Z (UTC as logged).

> Use one shared flight motor with platform-specific input adapters.

**Result / interpretation:** FlightCommand/BirdMotor/FlightDynamics form the shared core; separate keyboard, Quest and touch adapters submit commands. See b31efbc and 50cca6b.

## E03 — Require genuine editable geometry

Source: S5, JSONL line 484, user message, timestamp 2026-10-30T12:56:17.545Z (UTC as logged).

> Create genuine ProBuilder meshes with editable vertices, edges and faces. Do not rename ordinary Unity primitives to make them appear to be ProBuilder objects.

**Result / interpretation:** The scene contains actual ProBuilder meshes. The student subsequently confirmed editability; procedural primitives remain where appropriate. Evidence is in the T018 record.

## E04 — Challenge the camera-mode design

Source: S5, JSONL line 1269, user message, timestamp 2026-10-30T14:07:17.307Z (UTC as logged).

> **Alternative:** reuse or minimally adapt FoundationMode in the existing FlightPrototype if this achieves the exact same behavior with fewer changes.

**Result / interpretation:** Direct FoundationMode reuse replaced the parallel StaticGrayboxView behavior. No third permanent scene was created. This was a design correction driven by the student, not an unsolicited architecture rewrite.

## E05 — Human Editor validation

Source: S5, JSONL line 2812, user message, timestamp 2026-10-30T18:51:19.936Z (UTC as logged).

> - Wingbeat triggers one impulse per press, without repeating when held.

**Result / interpretation:** This is a student report of T014 Editor behavior, not an automated device test. Other touch checks are recorded in the same message and validation report.

## E06 — Physical Quest feedback and fallback

Source: S5, JSONL line 3118, user message, timestamp 2026-10-30T19:50:36.253Z (UTC as logged).

> - Flight works using the left joystick, right trigger and real two-controller wingbeat gestures.

**Result / interpretation:** Recorded as functional physical Quest evidence. The same report says trigger fallback helps when arm movement is tiring; no comfort or performance certification follows.

## E07 — Physical Android feedback

Source: S5, JSONL line 3167, user message, timestamp 2026-10-30T20:21:44.354Z (UTC as logged).

> Android: the APK launches and basic touch flight controls work.

**Result / interpretation:** Recorded as basic physical Android functionality. Simultaneous multi-touch and interruption edge cases remain follow-up validation, not passes.

## E08 — A limitation becomes future investigation

Source: S5, JSONL line 3167, user message, timestamp 2026-10-30T20:21:44.354Z (UTC as logged).

> when restarting or re-entering the application on Quest 2, the user's initial viewpoint is sometimes offset relative to the bird, placing the user beside it rather than at the intended first-person or third-person position.

**Result / interpretation:** Recorded under T020. Tracking-origin initialization, recentering and viewpoint offsets are investigation targets, not established causes. Camera behavior was not changed.