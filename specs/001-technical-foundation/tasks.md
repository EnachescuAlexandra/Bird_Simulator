# Tasks: Technical Foundation

**Input**: `specs/001-technical-foundation/` specification, plan, research, data model,
contracts and quickstart. Follow `AGENTS.md` and constitution v1.0.0.
**Status**: T001-T019 complete. T020/T021 partially verified: Quest 2 tracking/trigger and Android 15 functionality are user-reported passes; device details, performance and comfort remain pending. T022 complete. T023 AI/foundation documentation review confirmed by the student; separate third-party attribution review pending. T024 not executed from the new README. T025 file audit/acceptance matrix completed, final review and dependencies pending. See docs/foundation-validation.md (2026-10-25).

## Format and Execution Rules

Each checkbox has an ID, story label where applicable, concrete paths, outcome, dependencies,
verification and manual-action flag. `[P]` permits independent file work only after dependencies
pass; never concurrently mutate the same Unity Editor/project. All paths are repository-relative
except `%USERPROFILE%/.codex/config.toml`, which is user-global host configuration.

Perform only the subsequently authorized stage. Preserve existing assets/GUIDs and `.meta` files;
create new Unity assets through Editor/MCP when available. No commits or pushes without an
explicit request. Do not add gameplay, services, unrelated packages, or sandbox/network relaxations.

Append evidence as tasks execute to `docs/foundation-validation.md`; record category, requirement,
date, exact versions, environment/device, expected/actual result, evidence and limitation.
Statuses are “passed, failed, blocked, or pending”; “build never implies device pass.” Hardware is
“unconfirmed until the user confirms model/access.” Completed checks and pending manual checks
must be reported separately. A blocked task stays unchecked and blocks dependent implementation.

## Stage 1: Unity CLI/Pipeline and MCP (US1, P1; MVP)

### Setup (shared infrastructure)

- [x] T001 Inspect `BirdLifeSimulator/ProjectSettings/ProjectVersion.txt`, `BirdLifeSimulator/Packages/manifest.json` and `BirdLifeSimulator/Packages/packages-lock.json`; record Windows version, installed CLI/Editor/Codex versions, current Git state and hardware availability in `docs/foundation-validation.md`. Outcome: dated baseline and prerequisite list. Dependencies: none. Verify: exact versions or explicit not-found/unconfirmed entries; no credentials captured. Manual: yes, user confirms license/sign-in and device access when known.
- [x] T002 Install the official Unity CLI on Windows if absent, following the official links in `specs/001-technical-foundation/research.md`; record resolved executable, actual version/channel and installation source in `docs/foundation-validation.md`. Outcome: usable CLI without changing the Unity Editor version. Dependencies: T001. Verify: `unity --version` and help succeed; compare actual release with planned candidate, record any difference before proceeding. Manual: yes, host install/PATH and interactive prompts; Codex may assist only when authorized.

### Foundational prerequisites

- [x] T003 Discover published Pipeline versions and compatibility, select an exact version before installing into `BirdLifeSimulator/Packages/manifest.json`, and preserve the resolved `BirdLifeSimulator/Packages/packages-lock.json`. Outcome: Pipeline loaded in the correct open project. Dependencies: T002. Verify: record selected/installed Pipeline and dependency versions in `docs/foundation-validation.md`, compilation succeeds, and `unity pipeline list` identifies this project. Manual: yes, user opens Unity 6000.3.25f1, signs in if needed, handles import dialogs and refocuses Editor.
- [x] T004 Assess the official Unity agent plugin/skills instructions linked from `specs/001-technical-foundation/research.md`; configure only the minimal required Codex guidance if needed and record exact source/version/install location in `docs/foundation-validation.md`. Outcome: required Unity agent guidance available, or documented reason no additional install is needed. Dependencies: T003. Verify: inspect installed guidance and supported Codex setup without auto-installing unrelated tools or replacing `AGENTS.md`; distinguish optional plugin convenience from MCP requirements. Manual: yes if plugin installation or host prompts are needed; otherwise no.

### US1: Inspect and Edit the Open Project

**Goal**: reliable project-bound Editor read/write access. **Independent test**: hierarchy and
create/read/remove/read round trip; no smoke-scene/XR work needed.

- [x] T005 [US1] Preview Codex setup and merge only the Unity stdio MCP server entry into `%USERPROFILE%/.codex/config.toml`, using the absolute CLI executable and explicit `BirdLifeSimulator/` path. Outcome: minimal Codex extension connection. Dependencies: T004. Verify: review only sanitized Unity-entry delta, preserve other servers/credentials and sandbox policies, reload extension and discover MCP tools; do not configure the VS Code Copilot client. Manual: yes, user reviews/applies host configuration and reloads extension.
- [x] T006 [US1] Through MCP, verify project path, Editor version, active scene and hierarchy; store sanitized baseline in `docs/foundation-validation.md`. Outcome: correct live Editor identity and usable discovered operation schemas. Dependencies: T005. Verify: user-visible hierarchy matches; wrong/ambiguous project or unavailable tools is blocked, not passed. Manual: yes, user compares the visible Editor instance.
- [x] T007 [US1] Through MCP, create one uniquely named temporary object in the active scene under `BirdLifeSimulator/Assets/Scenes/`, read it, delete that exact object and reread the hierarchy; record the round trip in `docs/foundation-validation.md`. Outcome: verified reversible mutation. Dependencies: T006. Verify: “zero temporary objects or unintended scene changes remain”; compare original objects/references and dirty state, report incomplete cleanup and never discard unrelated user edits. Manual: yes, user confirms visual cleanup/scene state; Codex performs discovered MCP operations.

**Stage gate**: T007 passes before any subsequent Editor implementation. Do not silently switch
to a third-party integration or accept shell-only operations as the MCP verification.

## Stage 2: Shared Non-VR Smoke Scene (US2, P1, first slice)

**Goal**: useful headset-free smoke test. **Independent test**: geometry and one visible response
per Space/mouse activation; VR packages and hardware are not prerequisites.

- [x] T008 [US2] Create `BirdLifeSimulator/Assets/Scenes/FoundationSmoke.unity` and its metadata through Editor/MCP with ground, distinguishable primitives, light and target; preserve SampleScene. Outcome: minimal shared environment. Dependencies: T007. Verify: geometry visible, references valid, no gameplay or imported environment assets. Manual: yes, visual Game view review.
- [x] T009 [US2] Implement one color-toggle action in `BirdLifeSimulator/Assets/Foundation/Runtime/SmokeTarget.cs`. Outcome: shared test behavior. Dependencies: T008. Verify: “Each input press invokes one target toggle” and “repeated Play sessions reset to a documented initial state”; record initial colors/state. Manual: no, Codex can compile and exercise behavior through Editor tools.
- [x] T010 [US2] Create `BirdLifeSimulator/Assets/Foundation/Prefabs/NonVRRig.prefab` and `BirdLifeSimulator/Assets/Foundation/Input/NonVR.inputactions` with a conventional camera and Space/mouse/on-screen touch control wired to the shared action. Outcome: non-VR input path retained for future Windows use. Dependencies: T009. Verify: one active camera/listener, one response per press, no headset or XR runtime needed. Manual: yes, user exercises keyboard/mouse and observes rendering; laptop touch is optional supplementary evidence only.

**Stage gate**: recorded non-VR rendering and keyboard/mouse pass; Android touch remains untested.

## Stage 3: OpenXR, XRI, Simulator and Mode Separation (US2, P1)

**Goal**: simulate VR without losing non-VR operation. **Independent test**: simulated pose and
controller action, then non-VR -> VR -> non-VR with no duplicated responses.

- [x] T011 [US2] Add planned OpenXR 1.16.1 and XRI 3.3.2 through Package Manager to `BirdLifeSimulator/Packages/manifest.json`; record actual resolved versions, XR Plug-in Management/Core Utils and retained Input System in `BirdLifeSimulator/Packages/packages-lock.json` and `docs/foundation-validation.md`. Outcome: compatible XR dependencies. Dependencies: T010. Verify: successful resolution/compilation and no unrelated upgrades; stop and document incompatibility before choosing alternatives. Manual: yes for Editor prompts; Codex can assist with authorized package setup.
- [x] T012 [US2] Import only required XRI Starter Assets/XR Device Simulator samples under `BirdLifeSimulator/Assets/Samples/` and create `BirdLifeSimulator/Assets/Foundation/Prefabs/VRRig.prefab` with action-based XR Origin and controller select routed to SmokeTarget. Outcome: stationary VR setup and XR Interaction Simulator. Dependencies: T011. Verify: no locomotion, simulated head pose changes view, simulated controller select toggles target once; simulator is VR-only and Editor-only. Manual: yes, user focuses Game view and exercises simulator controls.
- [x] T013 [US2] Implement explicit mode bootstrap in `BirdLifeSimulator/Assets/Foundation/Runtime/FoundationMode.cs` and configure both rigs in `BirdLifeSimulator/Assets/Scenes/FoundationSmoke.unity`. Outcome: mode identity independent of the Android target. Dependencies: T012. Verify: IDs “AndroidTouch or QuestStandalone; unique, never inferred only from Android target”; runtime mode “nonVR or VR; owns exactly one active rig/input set and one audio listener”; “Repeating a mode selection is idempotent; switching disables the old input before enabling the new one”; simulator excluded from device players. Manual: no for automated inspection; user confirmation occurs in T014.
- [x] T014 [US2] Exercise and record both Editor modes using `specs/001-technical-foundation/quickstart.md` in `docs/foundation-validation.md`; add focused regression tests in `BirdLifeSimulator/Assets/Foundation/Tests/PlayMode/ModeBehaviorTests.cs` only where they meaningfully verify single responses and mode exclusivity. Outcome: US2 evidence with useful regression coverage. Dependencies: T013. Verify: simulated pose/controller response and non-VR -> VR -> non-VR/repeated Play sessions; no duplicate listeners/input callbacks, compilation errors or changed shared semantics. Manual: yes, rendering/input observation; label automated results separately.

**Stage gate**: US2 headset-free checks pass; simulator results prove neither tracking nor comfort
on physical Quest. No unnecessary test framework or tests mirroring configuration.

## Stage 4: Build Configurations and Build Verification (US3, P2, build slice)

**Goal**: reproducible target selection and distinct APKs. **Independent test**: select each profile,
verify effective settings and build; device launch is a different check.

- [x] T015 [US3] Inspect Android Build Support/SDK/NDK/OpenJDK prerequisites for Unity 6000.3.25f1 and record exact installed versions and missing prerequisites in `docs/foundation-validation.md`. Outcome: known build toolchain. Dependencies: T014. Verify: matching Editor modules and tool paths; install missing required modules only in this authorized stage, never upgrade the Editor. Manual: yes, user handles Hub/license/download prompts if needed.
- [x] T016 [US3] Create ordinary Android `BirdLifeSimulator/Assets/Settings/Build Profiles/AndroidTouch.asset` with FoundationSmoke, explicit non-VR mode, separate application ID and `BirdLifeSimulator/Builds/AndroidTouch/` output. Outcome: named touch configuration. Dependencies: T015. Verify: “xrPolicy: disabled for touch”; scene/input/camera match contract, not just the platform name. Manual: yes, inspect native profile UI; Codex can create/inspect through available Editor tools.
- [x] T017 [US3] Create native Meta Quest `BirdLifeSimulator/Assets/Settings/Build Profiles/QuestStandalone.asset` with OpenXR/Quest controller features, XR mode, ARM64/IL2CPP, separate application ID and `BirdLifeSimulator/Builds/QuestStandalone/` output. Outcome: distinct Quest configuration. Dependencies: T016. Verify: “xrPolicy: ... OpenXR for Quest”; inspect Player overrides/validation and recheck package lock after profile auto-install behavior. Manual: yes, review profile/validation UI; Codex automates supported configuration.
- [x] T018 [US3] Verify profile selection, XR loader/startup/features and mode state across touch -> Quest -> touch and Editor restart; if native overrides cannot isolate shared Android state, add only `BirdLifeSimulator/Assets/Foundation/Editor/FoundationBuildGuard.cs`. Outcome: reproducible minimal selection/prebuild enforcement. Dependencies: T017. Verify: “A mismatched profile/XR state blocks building”; repeated selection is idempotent and unknown mode fails clearly; document exact selection procedure in `docs/foundation-validation.md`. Manual: yes, restart/UI check; Codex tests guard behavior where introduced.
- [x] T019 [US3] Build both APKs into `BirdLifeSimulator/Builds/AndroidTouch/` and `BirdLifeSimulator/Builds/QuestStandalone/`, then record reports, artifact identity, resolved packages and inspected manifests in `docs/foundation-validation.md`. Outcome: independent touch/Quest build results. Dependencies: T018. Verify: expected shared scene, distinct application IDs, no simulator in either player, no Quest-only required features/XR startup in touch and intended OpenXR support in Quest; no batch Editor against an already locked project. Manual: conditional, user handles build/toolchain prompts. Failed/missing prerequisites stay failed/blocked; a successful build does not pass a device test.

**Stage gate**: both build outcomes and isolation evidence recorded. Successful-build acceptance
requires two successful artifacts; missing device access alone does not prevent builds.

## Stage 5: Documentation, AI Record and Remaining Device Checks

### US3: Physical Device Checks (P2)

**Independent test**: launch the matching artifact on each actual device; record unavailable
hardware as pending. These tasks depend on the relevant artifact, not documentation completion.

- [ ] T020 [US3] Confirm Android/Quest hardware access and freeze device-specific refresh/frame-time targets and observation duration in `docs/foundation-validation.md`. Outcome: executable device test conditions or explicit hardware-pending entries. Dependencies: T019 build evidence available. Verify: record model/OS/refresh rate/operator; assess planned Android 60 fps/Quest 72 Hz and five-minute warm-up-followed observation against actual hardware rather than assume support. Manual: yes, user confirms availability and physical access; unconfirmed hardware remains pending.
- [ ] T021 [US3] Deploy matching APKs from `BirdLifeSimulator/Builds/` and execute physical Android touch and standalone Quest checks in `docs/foundation-validation.md`. Outcome: device-specific launch/render/input/performance and initial Quest comfort evidence, or explicit pending reasons. Dependencies: T020 and successful relevant T019 artifact; test each available device independently. Verify: physical touch on Android; tracking/controller on Quest; measured frame timing and duration; stop on discomfort; no laptop/simulator substitution. Manual: yes, developer mode/USB trust, physical interaction and comfort observations. Leave task unchecked while required hardware checks remain pending.

### US4: Reproduce and Audit the Foundation (P2)

**Independent test**: follow README to repeat an implemented Editor check and select both build
profiles; compare AI record against actual changes. This review can finish while device tests wait.

- [x] T022 [P] [US4] Update `README.md` with actual prerequisites/versions, MCP setup and reversible test, installed official skills/plugin if any, both Editor modes/simulator bindings, profile selection, build paths and evidence links. Outcome: reproducible completed-work instructions. Dependencies: T019, or recorded blocked build attempts. Verify: mark unfinished steps and device availability truthfully; no speculative success, credentials, or unrelated permissions. Manual: no for drafting; human reproduction is T024.
- [ ] T023 [P] [US4] Create/update `docs/ai-usage.md` and, if assets/samples require attribution, `docs/third-party-assets.md`. Outcome: material AI contributions and third-party provenance traceable from documentation through implementation. Dependencies: T019, or recorded blocked build attempts. Verify: entries contain date/tool/contribution/affected artifacts/human review/completed checks/limitations; asset entries include source, author, license and attribution; do not claim unperformed human review. Manual: yes, user reviews material assistance and attribution.
- [ ] T024 [US4] Have a reviewer reproduce one headset-free Editor check and both profile selections from `README.md`; reconcile claims with `docs/ai-usage.md` and record results in `docs/foundation-validation.md`. Outcome: US4 reproducibility verified. Dependencies: T022, T023. Verify: no undocumented source edits, missing steps or incorrect pass claims; correct docs and retry on discrepancies. Manual: yes, human executes/reviews the guide.

### Polish and Cross-Cutting Review

- [ ] T025 Review tracked/unignored changes, asset GUIDs and `.meta` pairings under `BirdLifeSimulator/Assets/`, `BirdLifeSimulator/Packages/` and `BirdLifeSimulator/ProjectSettings/`; finalize acceptance summary in `docs/foundation-validation.md`. Outcome: honest milestone handoff with no scope drift. Dependencies: T024 plus T020/T021 outcomes or explicitly pending hardware entries. Verify: FR-001 through FR-014 mapped to evidence, separate Editor/build/device results, no temporary object/generated caches/build artifacts in source tracking, no flight/food/refuge/forest implementation; pending devices prevent any fully-device-validated claim. Manual: yes, user reviews final limitations. Do not commit or push.

## Dependencies and Execution Order

```text
T001 -> T002 -> T003 -> T004 -> T005 -> T006 -> T007  [US1 MVP]
 -> T008 -> T009 -> T010                            [US2 non-VR slice]
 -> T011 -> T012 -> T013 -> T014                    [US2 complete]
 -> T015 -> T016 -> T017 -> T018 -> T019            [US3 builds]
 -> T020 -> T021                                   [US3 devices; may remain pending]
T019 -> T022 + T023 -> T024                         [US4; independent of device wait]
T024 + explicit device outcomes/pending record -> T025
```

US1 has no gameplay dependencies. US2 is independently demonstrable but uses the requested
MCP-first execution gate. US3 requires the scene/modes; US4 validates documented implementation.
A failed prerequisite blocks affected successors, not unrelated evidence/documentation work.

## Parallel Opportunities by Story

- US1: none; connection identity and reversible mutation must be sequential.
- US2: none in the task graph; scene/rig/input work shares one Editor and serialized assets.
- US3: Android and Quest observations can be scheduled independently after T020 for each successful
  artifact, but serialize edits to `docs/foundation-validation.md` and the same connected Editor.
- US4: after T019, draft T022 (`README.md`) and T023 (`docs/ai-usage.md`, attribution) in parallel;
  combine only after human review. These are the only tasks marked `[P]`.

## Implementation Strategy and Coverage

MVP is Stage 1 only: documented live MCP hierarchy/object verification. Stop and report each
stage gate before the next requested increment. Do not auto-install or implement from this list.
Stage 2 adds the smallest useful headset-free scene; Stage 3 completes simulated VR; Stage 4
proves build isolation; Stage 5 makes results reproducible and tracks real-device limitations.

| Requirements | Tasks |
| --- | --- |
| FR-001 | T001-T007 |
| FR-002 to FR-005 | T008-T014, T016-T018 |
| FR-006 to FR-007 | T015-T019 |
| FR-008 to FR-009 | evidence throughout, T020-T021, T025 |
| FR-010 | T010, T013, T018 |
| FR-011 to FR-012 | T022-T024; retain evidence as work occurs |
| FR-013 to FR-014 | setup version capture, all stage boundaries, T025 |

Exact installed versions must be captured during setup, not copied from planned candidates.
Meaningful automated behavior checks are permitted by the specification; no mandatory TDD or
implementation-mirroring test suite is introduced. No extension hooks file was present at generation.















