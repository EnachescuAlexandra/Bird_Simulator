# BirdLife Simulator

## Project Overview

BirdLife Simulator is an educational simulation being developed as a Computer Science
bachelor's thesis project. It aims to introduce bird habitats and environmental challenges
through the perspective of a blackbird, combining habitat exploration with interactive learning.
The project is currently at the technical-foundation stage; habitat-based gameplay is planned.

## Author

**Alexandra-Maria Enăchescu** · Group **3E1**

Faculty of Computer Science, Alexandru Ioan Cuza University of Iași

## Documentation

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

- Bird flight from a blackbird's perspective.
- Habitat exploration and resource collection.
- Environmental scenarios and educational feedback.

## Platforms & Technologies

- **Meta Quest standalone** — primary platform.
- **Android touch** — secondary platform.
- **Unity 6.3 LTS** (6000.3.25f1), **C#**, **OpenXR** and **XR Interaction Toolkit**.
- **Blender** — planned for 3D modelling.
- **Windows Unity Editor** — keyboard/mouse and headset-free XR simulator testing;
  a Windows application build has not been completed.

## Current Development Status

The technical foundation includes separate AndroidTouch and QuestStandalone build profiles,
a shared smoke-test scene, isolated input/camera configurations and successful APK builds
for both platforms. Basic device tests are user-reported: controller visibility, tracking and
trigger interaction on Meta Quest 2, and AndroidTouch functionality on an Android 15 device.

**Flight gameplay is not implemented yet.** Performance and VR comfort validation remain
incomplete; detailed evidence and limitations are recorded in the documentation below.

## Development Progress

Only milestones supported by the project records are listed.

| Week | Verified progress |
| --- | --- |
| 19–25 October 2026 | **24 October:** technical foundation, Editor checks and both APK builds completed. **25 October:** documentation and Git audit consolidated, including previously reported basic device tests. |

## Getting Started

Use Unity **6000.3.25f1** with Android Build Support and its bundled SDK, NDK and OpenJDK.
Open the Unity project in `BirdLifeSimulator/`; package dependencies are recorded in its
manifest and lock files. Open `Assets/Scenes/FoundationSmoke.unity` for the current smoke test.

Follow the [full setup and reproduction guide](docs/manual-setup.md) for Editor controls,
XR simulation, profile selection, optional Codex MCP tooling and build instructions.
