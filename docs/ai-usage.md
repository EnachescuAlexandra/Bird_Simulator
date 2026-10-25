# AI assistance record

Updated 2026-10-25. Tool: OpenAI Codex in VS Code, with PowerShell/file inspection
and official Unity CLI/Pipeline MCP. This records material assistance, not human authorship
or an assertion that every generated line has received a human code review.

## Where AI was used

The table below identifies concrete locations and purposes, rather than attributing the
entire project to AI. Codex generated draft code and subsequently edited/debugged the listed
foundation components under the student's instructions. The record is at file/component
level; it is not a reliable line-by-line reconstruction of every edit or an authorship percentage.
Paths are relative to the repository root.

| Location | AI contribution and purpose | Human contribution / verification |
| --- | --- | --- |
| [SmokeTarget.cs](../BirdLifeSimulator/Assets/Foundation/Runtime/SmokeTarget.cs) | Drafted/edited shared cyan/yellow toggle, activation count and initial-state reset | Student reported visible color changes and reset tests; no complete code review claimed |
| [NonVRSmokeInput.cs](../BirdLifeSimulator/Assets/Foundation/Runtime/NonVRSmokeInput.cs) and [NonVR.inputactions](../BirdLifeSimulator/Assets/Foundation/Input/NonVR.inputactions) | Implemented Space and button adapters to the shared behavior; prevented duplicate UI keyboard activation | Student tested Space/button sequence and Android functionality |
| [VRSmokeTest.cs](../BirdLifeSimulator/Assets/Foundation/Runtime/VRSmokeTest.cs) | Implemented right-controller trigger adapter and Editor-only simulator activation | Student reported simulator response, then Quest controller tracking/trigger response |
| [FoundationMode.cs](../BirdLifeSimulator/Assets/Foundation/Runtime/FoundationMode.cs) | Implemented runtime bootstrap and exclusive mode/input/camera selection; profile-specific player default with non-VR Editor default | MCP inspected isolation; student checked switching, repeated Play and reset |
| [FoundationBuildGuard.cs](../BirdLifeSimulator/Assets/Foundation/Editor/FoundationBuildGuard.cs) | Implemented shared-Android XR synchronization and prebuild validation; corrected unsafe Play-transition synchronization after a reported defect | Student reported the defect and confirmed the post-fix manual test; validation evidence retains the failure |
| [FoundationSmoke.unity](../BirdLifeSimulator/Assets/Scenes/FoundationSmoke.unity) and [VRRig.prefab](../BirdLifeSimulator/Assets/Foundation/Prefabs/VRRig.prefab) | Used Editor/MCP to wire UI/targets, adapt existing XRI prefab and simulator, and configure modes | Initial smoke geometry was made manually by the student; student approved the saved non-VR state and observed rendering. Unity authored the source XRI samples |
| [Build Profiles](../BirdLifeSimulator/Assets/Settings/Build%20Profiles/), [manifest](../BirdLifeSimulator/Packages/manifest.json), [lock file](../BirdLifeSimulator/Packages/packages-lock.json), XR/XRI project configuration | Assisted package selection/setup, profile overrides and build verification; recorded actual resolved versions and manifests | Student performed manual Quest OpenXR/Fix All setup and device operation; not all Editor serialization changes have established authorship |
| [Feature documents](../specs/001-technical-foundation/), [constitution](../.specify/memory/constitution.md), [README](../README.md) and [validation](foundation-validation.md) | Drafted/refined specifications, task breakdown, technical research, public README, test evidence and continuation documentation | Student supplied scope, educational direction, author details and reported outcomes. Full document/attribution review remains pending |

Do not present AI-assisted implementation as entirely hand-written. Human design decisions,
manual Unity actions and reported tests above remain distinct contributions. Original Unity
samples retain their separate provenance in [third-party assets](third-party-assets.md).

## Timeline and verification evidence

| Date | Contribution and affected artifacts | Verification / human involvement |
| --- | --- | --- |
| 2026-10-24 | Spec Kit constitution and technical-foundation specification, plan, research, contracts and tasks under `.specify/` and `specs/001-technical-foundation/` | User supplied scope, principles and staged instructions; future features excluded |
| 2026-10-24 | Official CLI/Pipeline setup, local Unity skill and minimal user MCP entry | Executable/package/tool discovery and reversible Editor-object evidence recorded; user performed server starts/reloads; policies preserved |
| 2026-10-24 | `Assets/Foundation/` shared target, input adapters, input asset, mode selector, VRRig and scene wiring | User supplied initial smoke geometry, approved non-VR scene save, and manually checked keyboard/button/simulator behavior; MCP verified isolation/compilation |
| 2026-10-24 | Native build profiles, OpenXR/XRI setup and `FoundationBuildGuard.cs` | User configured Quest OpenXR validation; guard regression reported by user and corrected; post-fix Play and restart checks confirmed; effective XR policies checked |
| 2026-10-24 | Both APK builds and manifest/report inspection | Both succeeded; SHA256, messages and limitations recorded; user subsequently reported Quest 2 tracking/trigger and Android 15 functionality |
| 2026-10-25 | README, this record, attribution, validation/continuation/task updates and Git audit | Documentation/file inspection only; no new builds or implementation changes. `.utmp` ignore and removal of temporary constitution review comment verified from interrupted work |
| 2026-10-25 | Week 2 specification, technical design/research, contracts, validation guide and tasks in `specs/002-flight-system/`; README roadmap | Student supplied milestone/scope and eight-hour budget. Codex inspected committed architecture and official movement/XR/input documentation; document consistency reviewed, no flight implementation/runtime/build/device test performed |

Human confirmations cover the specific reported tests, not an independent audit of all code,
licenses, documentation or performance. No measured frame timing, timed warm-up/observation,
comfort acceptance or fresh README-based reproduction is claimed. On 2026-10-25 the student
confirmed reading and validating this AI record and the foundation documentation. Separate
review of third-party attribution/licenses was not explicitly confirmed, so T023 remains
partially complete; T024 reproduction remains pending. Existing defects/warnings and hardware identity gaps are
recorded in [validation](foundation-validation.md).

Third-party samples are Unity-authored, not AI-generated; see
[third-party assets](third-party-assets.md). No AI-created bird, forest art or flight system
was added. Credentials and conversation transcripts are not part of this record.
