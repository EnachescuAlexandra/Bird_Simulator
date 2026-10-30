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

**AI assistance disclosure:** Codex assisted with design review, planning, implementation,
debugging and documentation. The student directed decisions and performed the reported
Editor/Quest/Android tests. Read the [public disclosure](docs/ai-usage.md) and
[genuine interaction excerpts](docs/ai-evidence/selected-excerpts.md). Unity samples retain
separate attribution; functional testing does not imply independent code review.

- [Technical setup and reproduction guide](docs/manual-setup.md)
- [Validation report and remaining checks](docs/foundation-validation.md)
- [Spec Kit specification](specs/001-technical-foundation/spec.md), [implementation plan](specs/001-technical-foundation/plan.md) and [tasks](specs/001-technical-foundation/tasks.md)
- [Public AI disclosure and authentic excerpts](docs/ai-usage.md)
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
To view the dashboard locally, press **Ctrl+O in your browser** and select `docs/dashboard.html`.
Opening it as a VS Code tab or a GitHub file shows the source instead of the rendered page.

The technical foundation includes separate AndroidTouch and QuestStandalone build profiles,
a shared smoke-test scene, isolated input/camera configurations and successful APK builds
for both platforms. Basic device tests are user-reported: controller visibility, tracking and
trigger interaction on Meta Quest 2, and AndroidTouch functionality on an Android 15 device.

**The weekly IMR grayboxing assignment is complete.** The
[Main Modules document](https://docs.google.com/document/d/1s0xvUugMEB8GFLSb5C0Golx6jKGv3lgCni7PzFzqHis/edit?tab=t.0)
is finished. `FlightPrototype` contains a 60 × 60 m woodland with a 20 × 20 m clearing,
obstacles, landmarks, a refuge and three feeding placeholders. A Unity Editor script generated
the initial layout. The environment combines standard Unity primitives with genuine,
procedurally generated, editable ProBuilder meshes: ground, clearing, refuge landing surface,
stump, rock shelf, fallen log, boulder and standing stone landmark.

The graybox was physically tested on Meta Quest 2. Flight, real two-controller wingbeat
gestures and trigger fallback work on Quest; the Android APK launches and its basic touch
flight controls work. These features go beyond the minimum grayboxing requirement.
Untested multitouch, gesture/flight edge cases and VR comfort/performance remain open.
Ground walking and VR viewpoint/recenter improvements—including reported lag and restart
offsets—are deferred. See the [dashboard](docs/dashboard.html),
[AI disclosure](docs/ai-usage.md) and [validation record](docs/foundation-validation.md).

## Development Progress

Only milestones supported by the project records are listed.

| Week | Verified progress |
| --- | --- |
| Week 1 · 19–25 October 2026 | Technical foundation completed as a local checkpoint: Editor checks, two APK builds and reported basic device tests. Performance/comfort, fresh reproduction and remaining acceptance reviews are still pending. |
| Week 2 · 26 October–1 November 2026 | **IMR grayboxing assignment completed**, with Main Modules and physical Quest testing. Deliverables and remaining validation are described above. |
| Week 3 · 2–8 November 2026 | **Planned:** food collection, objectives and restart; detailed scope to be specified later. |
| Week 4 · 9–15 November 2026 | **Planned:** integration, testing and demonstration; no completion or validation implied. |

## Getting Started

Use Unity **6000.3.25f1** with Android Build Support and its bundled SDK, NDK and OpenJDK.
Open the Unity project in `BirdLifeSimulator/`; package dependencies are recorded in its
manifest and lock files. Open `Assets/Scenes/FlightPrototype.unity` for the weekly prototype; FoundationSmoke remains the technical reference.

Follow the [full setup and reproduction guide](docs/manual-setup.md) for Editor controls,
XR simulation, profile selection, optional Codex MCP tooling and build instructions.
