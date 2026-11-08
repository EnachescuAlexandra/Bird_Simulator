# BirdLife Simulator

## Project Overview

BirdLife Simulator is an educational simulation being developed as a Computer Science
bachelor's thesis project. It aims to introduce bird habitats and environmental challenges
through the perspective of a blackbird, combining habitat exploration with interactive learning.
The technical foundation, editable woodland graybox, shared flight/ground movement, seated-first
VR viewpoint improvements and one-food collection prototype are implemented. Feeding missions
and educational gameplay remain planned.

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

- Feeding missions, additional food portions and objectives beyond the one-food prototype.
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
The professor-requested prototype functionality and VR viewpoint improvements for this week's
milestone are implemented: an explorable woodland-edge habitat with clearing, trees, obstacles,
feeding locations and refuge; takeoff, controlled flight, wingbeats/gliding and landing; slow
ground walking/steering with walking–flight transitions (T019); and a bird-relative VR anchor,
first/third-person perspectives, gradual turn-follow, manual recenter and independent HMD
tracking for seated-first interaction (T020). Feature 003 adds one reusable food collectible
with automatic ground-only collection and visual feedback.

Graybox, movement and food collection have Editor/XR Device Simulator testing as applicable.
The latest Quest APK was built and launched, with physical viewpoint testing. Implementation
completion does not imply full device acceptance: occasional yaw misalignment after large
turns, possible tree collision gaps and controller visibility near ground level remain known
issues. VR comfort, recovery and remaining Quest/Android checks are ongoing; airborne food
rejection is still pending manual confirmation. See the [feature 002 tasks](specs/002-flight-system/tasks.md),
[feature 003 evidence](specs/003-food-collection/tasks.md) and [AI disclosure](docs/ai-usage.md).

## Development Progress

Only milestones supported by the project records are listed.

| Week | Verified progress |
| --- | --- |
| Week 1 · 19–25 October 2026 | Technical foundation completed as a local checkpoint: Editor checks, two APK builds and reported basic device tests. Performance/comfort, fresh reproduction and remaining acceptance reviews are still pending. |
| Week 2 · 26 October–1 November 2026 | **IMR grayboxing assignment completed**, with Main Modules and physical Quest testing. Deliverables and remaining validation are described above. |
| Week 3 · 2–8 November 2026 | **Requested prototype functionality implemented:** T019 ground movement, T020 seated-first viewpoints/recenter/turn-follow and one reusable food collectible (003), extending the graybox/flight baseline. Editor/simulator tests and Quest APK launch/viewpoint observations recorded above; known issues and physical comfort/device validation remain open. Objectives and broader feeding missions are future work. |
| Week 4 · 9–15 November 2026 | **Planned:** integration, testing and demonstration; no completion or validation implied. |

## Getting Started

Use Unity **6000.3.25f1** with Android Build Support and its bundled SDK, NDK and OpenJDK.
Open the Unity project in `BirdLifeSimulator/`; package dependencies are recorded in its
manifest and lock files. Open `Assets/Scenes/FlightPrototype.unity` for the weekly prototype; FoundationSmoke remains the technical reference.

Follow the [full setup and reproduction guide](docs/manual-setup.md) for Editor controls,
XR simulation, profile selection, optional Codex MCP tooling and build instructions.
