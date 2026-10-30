# BirdLife Simulator

## Project Overview

BirdLife Simulator is an educational simulation being developed as a Computer Science
bachelor's thesis project. It aims to introduce bird habitats and environmental challenges
through the perspective of a blackbird, combining habitat exploration with interactive learning.
The technical foundation, editable woodland graybox and shared flight prototype are implemented;
resource collection and educational gameplay remain planned.

## Author

**Alexandra-Maria Enăchescu** · Group **3E1**

Faculty of Computer Science, Alexandru Ioan Cuza University of Iași

## Documentation

- [Main Modules](https://docs.google.com/document/d/1s0xvUugMEB8GFLSb5C0Golx6jKGv3lgCni7PzFzqHis/edit?tab=t.0)
- [State of the Art — research document](https://docs.google.com/document/d/18zvAq05GNr29XjrC6TAuxqI5UiN1kP1nkBq7VYK1ikQ/edit?tab=t.0#heading=h.bcldxwdefncd)

**AI assistance disclosure:** OpenAI Codex assisted with drafting and modifying foundation
C# code, Unity configuration, debugging, planning and documentation. The
[file-by-file AI contribution register](docs/ai-usage.md#where-ai-was-used) identifies the
affected files, the purpose of the assistance and human verification. Unity samples are
third-party assets, not AI-generated work. Functional tests do not imply a complete human
code review.

- [Technical setup and reproduction guide](docs/manual-setup.md)
- [Validation report and remaining checks](docs/foundation-validation.md)
- [Spec Kit specification](specs/001-technical-foundation/spec.md), [implementation plan](specs/001-technical-foundation/plan.md) and [tasks](specs/001-technical-foundation/tasks.md)
- [AI usage record](docs/ai-usage.md)
- [Third-party asset sources and licenses](docs/third-party-assets.md)

## Planned Features

The following are development goals, not implemented functionality:

- Ground walking/steering and further VR viewpoint improvements (deferred).
- Habitat exploration and resource collection.
- Environmental scenarios and educational feedback.

## Platforms & Technologies

- **Meta Quest 2 standalone** — primary platform.
- **Android touch** — secondary platform.
- **Unity 6.3 LTS** (6000.3.25f1), **C#**, **OpenXR** and **XR Interaction Toolkit**.
- **Blender** — planned for 3D modelling.
- **Windows Unity Editor** — existing XR simulator and optional keyboard debugging;
  standalone Windows is an optional later extension, not a current milestone.

## Current Development Status

[Project Dashboard & Progress Tracker](docs/dashboard.html) — standalone HTML snapshot;
[feature 002 tasks](specs/002-flight-system/tasks.md) remain authoritative.

The technical foundation includes separate AndroidTouch and QuestStandalone build profiles,
a shared smoke-test scene, isolated input/camera configurations and successful APK builds
for both platforms. Basic device tests are user-reported: controller visibility, tracking and
trigger interaction on Meta Quest 2, and AndroidTouch functionality on an Android 15 device.

**Weekly IMR grayboxing deliverable completed (student confirmation, 2026-10-30).**
`FlightPrototype` includes editable ProBuilder woodland geometry, refuge/resting surfaces,
three food placeholders, a replaceable mock blackbird, one reused XR rig and two VR viewpoints.
The student manually tested it on Quest 2 and Android: Quest joystick/trigger flight, real
two-controller wingbeats and trigger fallback work; Android launches and basic touch flight controls work (student-reported).

The shared flight motor, keyboard/Quest/touch controls and camera-follow are optional functionality
beyond the grayboxing assignment. Untested Android multi-touch, gesture/flight edge cases and
VR comfort/performance remain pending. Ground walking and VR viewpoint/recenter improvements,
including investigation of reported lag and intermittent Quest restart/re-entry viewpoint offsets, are deferred. See the
[validation report](docs/foundation-validation.md) and [feature 002 tasks](specs/002-flight-system/tasks.md).

## Development Progress

Only milestones supported by the project records are listed.

| Week | Verified progress |
| --- | --- |
| Week 1 · 19–25 October 2026 | Technical foundation completed as a local checkpoint: Editor checks, two APK builds and reported basic device tests. Performance/comfort, fresh reproduction and remaining acceptance reviews are still pending. |
| Week 2 · 26 October–1 November 2026 | **Weekly grayboxing deliverable completed.** Editable environment/mock bird and optional shared flight controls; student reports Quest 2 and Android device tests. Detailed multi-touch, edge-case and comfort acceptance remain pending. |
| Week 3 · 2–8 November 2026 | **Planned:** food collection, objectives and restart; detailed scope to be specified later. |
| Week 4 · 9–15 November 2026 | **Planned:** integration, testing and demonstration; no completion or validation implied. |

## Getting Started

Use Unity **6000.3.25f1** with Android Build Support and its bundled SDK, NDK and OpenJDK.
Open the Unity project in `BirdLifeSimulator/`; package dependencies are recorded in its
manifest and lock files. Open `Assets/Scenes/FlightPrototype.unity` for the weekly prototype; FoundationSmoke remains the technical reference.

Follow the [full setup and reproduction guide](docs/manual-setup.md) for Editor controls,
XR simulation, profile selection, optional Codex MCP tooling and build instructions.
