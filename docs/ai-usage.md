# Public AI assistance disclosure

AI tool: OpenAI Codex, used with source inspection, PowerShell and Unity CLI/Pipeline. This prototype is AI-assisted work directed and physically tested by the student; it is not presented as entirely handwritten or independently audited code.

## What AI helped with, and who decided

| Activity | AI contribution | Student contribution / limits |
| --- | --- | --- |
| Brainstorming and scope discussion | Explored implementation options and tradeoffs within the supplied bird/woodland concept | Student supplied the thesis/educational concept, Main Modules reference and platform/coursework priorities. No separate pre-session brainstorming transcript is claimed. |
| Critical design review | Compared scene duplication with component reuse; evaluated shared motor/adapters, tracked-camera ownership and ProBuilder choices | Student challenged over-complex approaches, required genuine editable geometry and corrected the required runtime mode-switching workflow. |
| Specification and planning | Drafted/refined Spec Kit spec, plan, contract, task ordering and acceptance/evidence records | Student defined scope and later authorized reordering, implementation stages and scoped weekly closure. Detailed unperformed criteria remain T021. |
| Code and scene authoring | Drafted/edited motor, platform adapters, gesture detector, viewpoint integration, procedural geometry, UI and narrow build tooling | Student supplied constraints, approved stages, operated Unity as needed and reviewed visible behavior; a complete independent line-by-line review is not asserted. |
| Debugging and evaluation | Inspected XRI behavior and mode ownership; identified the extra EventSystem lifecycle problem and proposed a scene-local fix | Student reported defects, restarted tooling after reloads, confirmed UI/simulator behavior and performed physical tests. |
| Documentation and dashboard | Wrote/refined README, disclosure, evidence summaries, task tracking and standalone HTML dashboard | Student requested the professor-facing handoff and supplied test outcomes/known limitations. No test result was invented to fill missing evidence. |

## Concrete decisions, rather than code generation alone

- **Preserve a known-good baseline:** the student explicitly prohibited repeating completed foundation setup. AI compared existing assets and evolved FlightPrototype while retaining FoundationSmoke. [E01](ai-evidence/selected-excerpts.md#e01--inspect-first-and-preserve-the-foundation)
- **One movement implementation:** a shared FlightCommand/BirdMotor/FlightDynamics core replaced the need for separate Quest/Android movement rules; input remains platform-specific. [E02](ai-evidence/selected-excerpts.md#e02--shared-motor-separate-inputs)
- **Evaluate the actual deliverable:** genuine ProBuilder geometry was required, rather than treating package installation or renamed primitives as completion. [E03](ai-evidence/selected-excerpts.md#e03--require-genuine-editable-geometry)
- **Accept a human correction:** the student pointed out FoundationSmoke already switched modes during Play. Direct FoundationMode reuse was chosen over duplicating a scene or keeping a second selector. [E04](ai-evidence/selected-excerpts.md#e04--challenge-the-camera-mode-design)
- **Diagnose instead of layering managers:** XRI can create an EventSystem when none is active. AI inspected that path and placed the Quest EventSystem under the existing rig so its lifetime follows rig activation; the student confirmed switching/UI behavior. This is a paraphrase of the S5 user-facing assistant message at JSONL line 2753, not a reconstructed quotation.
- **Use device feedback to revise priorities:** the student confirmed real Quest wingbeats/trigger fallback and Android controls, but reported arm fatigue, viewpoint limitations and restart offsets. Initially deferred T020 work was subsequently implemented under the professor's seated-first requirement, alongside T019 ground movement and feature 003 food collection. Codex assisted implementation, scene integration and focused checks; the student supplied simulator results and physical Quest observations. Known yaw/collision/controller-visibility issues and physical comfort/device acceptance remain open. Genuine prompts and scoped results are recorded in [E09–E12](ai-evidence/selected-excerpts.md#e09--seated-first-vr-viewpoint-work); [E06–E08](ai-evidence/selected-excerpts.md#e06--physical-quest-feedback-and-fallback) retain their historical scope.

## What was actually verified

Existing records distinguish Editor compilation/reference/motor checks from student-reported physical tests. The student reports that Quest launches and joystick, real two-controller wingbeats and trigger fallback work; Android launches and basic touch flight controls work. [E05–E08](ai-evidence/selected-excerpts.md#e05--human-editor-validation) and the [validation record](foundation-validation.md) preserve that distinction. T001–T018 are closed for weekly delivery/basic functional scope, not as a blanket pass of every original detailed criterion.

Timed route/glide checks, interruption/rearming/rejection cases, Android simultaneous multi-touch and measured comfort/performance remain follow-up validation. No new Play session, APK build, device test or performance measurement was performed for this documentation audit. No authorship percentage, invented time log or claim of full human code review is supplied.

## How the professor can inspect the workflow

1. Read the [source inventory and privacy limits](ai-evidence/sources.md) and [genuine selected interaction excerpts](ai-evidence/selected-excerpts.md).
2. Compare those instructions/reports with the [feature-002 tasks](../specs/002-flight-system/tasks.md), [contract](../specs/002-flight-system/contracts/flight.md) and [actual source](../BirdLifeSimulator/Assets/Flight/).
3. Inspect the named commits with git show. Git establishes the recorded implementation changes; it does not independently prove device behavior.
4. Read the [validation record](foundation-validation.md) for reported results and remaining limitations. Original logs exist privately; no raw session export is published.

Five project-associated Codex logs were found and a targeted message review was performed. Only selected genuine excerpts are published, with their original meaning preserved; headers/private context are omitted. Exact local filenames and raw messages remain private. Earlier unavailable brainstorming or missing device recordings are not reconstructed. This disclosure is prepared for professor review; the student should still check the attribution and choose what to demonstrate privately.

<details>
<summary>Historical component-level register and supporting evidence (retain original scope)</summary>

# AI assistance record

Historical register. Tool: OpenAI Codex in VS Code, with PowerShell/file inspection
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

## Recorded stages and verification evidence

| Stage | Contribution and affected artifacts | Verification / human involvement |
| --- | --- | --- |
| Foundation planning | Spec Kit constitution and technical-foundation specification, plan, research, contracts and tasks under `.specify/` and `specs/001-technical-foundation/` | User supplied scope, principles and staged instructions; future features excluded |
| Tooling setup | Official CLI/Pipeline setup, local Unity skill and minimal user MCP entry | Executable/package/tool discovery and reversible Editor-object evidence recorded; user performed server starts/reloads; policies preserved |
| Smoke implementation | `Assets/Foundation/` shared target, input adapters, input asset, mode selector, VRRig and scene wiring | User supplied initial smoke geometry, approved non-VR scene save, and manually checked keyboard/button/simulator behavior; MCP verified isolation/compilation |
| Build configuration | Native build profiles, OpenXR/XRI setup and `FoundationBuildGuard.cs` | User configured Quest OpenXR validation; guard regression reported by user and corrected; post-fix Play and restart checks confirmed; effective XR policies checked |
| Foundation builds | Both APK builds and manifest/report inspection | Both succeeded; SHA256, messages and limitations recorded; user subsequently reported Quest 2 tracking/trigger and Android 15 functionality |
| Documentation review | README, this record, attribution, validation/continuation/task updates and Git audit | Documentation/file inspection only; no new builds or implementation changes. `.utmp` ignore and removal of temporary constitution review comment verified from interrupted work |
| Flight planning | Week 2 specification, technical design/research, contracts, validation guide and tasks in `specs/002-flight-system/`; README roadmap | Student supplied milestone/scope and eight-hour budget. Codex inspected committed architecture and official movement/XR/input documentation; document consistency reviewed, no flight implementation/runtime/build/device test performed |

Human confirmations cover the specific reported tests, not an independent audit of all code,
licenses, documentation or performance. No measured frame timing, timed warm-up/observation,
comfort acceptance or fresh README-based reproduction is claimed. The student
confirmed reading and validating this AI record and the foundation documentation. Separate
review of third-party attribution/licenses was not explicitly confirmed, so T023 remains
partially complete; T024 reproduction remains pending. Existing defects/warnings and hardware identity gaps are
recorded in [validation](foundation-validation.md).

## Static graybox increment

Codex read the student's finalized Main Modules PDF (including journey diagrams/storyboard),
corrected the existing feature-002 plan for Quest-first development, and authored
`Assets/Flight/Editor/FlightGrayboxBuilder.cs`. Unity Editor/MCP executed it to create
`Assets/Scenes/FlightPrototype.unity`, six plain URP graybox materials and their metadata.
Geometry uses Unity primitives; no external models, textures or sounds were imported.
The one-time builder refuses existing output so subsequent manual layout edits are preserved.
Codex verified compilation, references/counts, dimensions, central clearance, save/reopen,
overwrite refusal and unchanged protected-file hashes; inspected a Scene-view screenshot.
The student supplied requirements and restarted Pipeline after domain reload. Student visual
review, real headset scale/comfort, bird, XR integration and flight remain pending. This
increment does not extend earlier foundation/device evidence or claim human code review.
Checkpoint cleanup subsequently aligned README/planning, added T017 physical flight validation
and documented the collider mismatch. No scene/code correction or new Unity/device test
was performed during cleanup; MCP was unavailable. All existing assets were preserved.

Third-party samples are Unity-authored, not AI-generated; see
[third-party assets](third-party-assets.md). No final bird/forest artwork or flight system
was added; the primitive graybox is temporary. Credentials and full raw conversation transcripts are not published; selected reviewed excerpts are linked above.

## Weekly flight prototype closure

Codex assisted with ProBuilder/collider refinement; MockBlackbird procedural geometry, prefab and materials; FoundationMode reuse; VRViewpoint; Runtime/Core FlightCommand, FlightDynamics and BirdMotor; KeyboardFlightInput, QuestFlightInput, TouchFlightInput, MotionWingbeatInput, BirdFollowCamera and Flight.inputactions; scene wiring, native profile entry selection and FoundationBuildGuard. The student supplied priorities/design corrections and performed reported Editor, physical Quest 2 and Android tests. Codex consolidated Spec Kit/evidence/setup/provenance and authored docs/dashboard.html. Earlier pending statements describe the increment when written; later confirmations are in validation. Dashboard/document review and scoped Git checks are not new runtime/device tests, independent code review or comfort/performance certification. No final artwork or generated bitmap assets were introduced.
</details>
