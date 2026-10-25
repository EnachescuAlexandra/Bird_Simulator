# Implementation Plan: Technical Foundation

**Branch**: `main` (unchanged; workflow feature identifier: `001-technical-foundation`)
**Date**: 2026-10-24 | **Spec**: [spec.md](spec.md)
**Input**: `specs/001-technical-foundation/spec.md`

## Summary

First establish the official Unity CLI MCP connection and prove a reversible object edit.
Then add shared smoke behavior with separate touch and VR input/camera rigs, OpenXR/XRI
simulation, and native Android Touch and Meta Quest Build Profiles. Finish with separate build
and device evidence and truthful documentation. This document authorizes no implementation.

## Technical Context

**Language/Version**: C# supported by existing Unity 6000.3.25f1; keep this Editor version.
**Primary Dependencies**: retain URP 17.3.0, Input System 1.20.0, Test Framework 1.6.0.
Select OpenXR 1.16.1 and XRI 3.3.2, designated releases for 6000.3. Use XRI's XR Interaction
Simulator sample; resolve XR Plug-in Management transitively and record its exact lock version.
MCP: official Unity CLI 1.0.0-beta.14 candidate plus `com.unity.pipeline`; exact Pipeline patch
must be selected from the official registry and pinned before installation (see research).
**Storage**: Unity assets and Markdown evidence only; no database or cloud runtime.
**Testing**: Edit/Play Mode checks where meaningful, manual Editor rendering/input inspection,
separate APK build verification and physical device runs.
**Target Platform**: Windows Editor; Android touch; Meta Quest standalone. Hardware availability
unconfirmed. Future Windows player remains possible, but no Windows build is required.
**Project Type**: single Unity project with Editor-only tooling.
**Performance Goals**: proposed smoke baseline: Android 60 fps, Quest 72 Hz, five minutes after
warm-up, with measured CPU/GPU frame timing. Confirm device refresh support and freeze budgets
before testing; these are targets, not achieved results or comprehensive comfort validation.
**Constraints**: no gameplay/locomotion; no new services or unnecessary frameworks; preserve GUIDs.
**Scale/Scope**: one smoke scene, two build profiles, two runtime modes, two headset-free Editor checks.

## Constitution Check

Pre-research and post-design review: PASS for all seven principles.

| Principle | Design evidence |
| --- | --- |
| Manageable scope | Foundation only; no bird, forest, flight, food, refuge or future features |
| Shared logic | One smoke action; input/camera isolated by mode |
| Editor VR/device distinction | Simulator gate separate from touch/Quest device gates |
| Small increments | Stages below with observable exit criteria; meaningful tests only |
| Asset/dependency integrity | Existing versions retained; pin additions and use Editor/MCP |
| Documentation/attribution | README, AI record and source/license entries planned with evidence |
| Simple architecture | One scene and mode selector; native profiles; no general framework |

External capabilities are documented, not experimentally proven. The official MCP implementation
is currently beta: use it in an isolated first stage, record exact versions, and stop if the
connection gate fails. Do not silently change integration or relax Codex policy.

## Project Structure

### Documentation (this feature)

```text
specs/001-technical-foundation/
  spec.md                         # preserved
  checklists/requirements.md      # preserved
  plan.md
  research.md
  data-model.md
  quickstart.md
  contracts/foundation.md
```

Feature selection is persisted in `.specify/feature.json`. No tasks.md is produced in planning.

### Source Code (repository root)

Proposed implementation paths, not created by this phase:

```text
BirdLifeSimulator/Assets/
  Scenes/FoundationSmoke.unity
  Foundation/Runtime/             # shared smoke action, small mode bootstrap
  Foundation/Input/               # touch/desktop and VR actions/adapters
  Foundation/Prefabs/             # non-VR and VR camera/input rigs
  Foundation/Editor/              # profile selection/prebuild validation if necessary
  Foundation/Tests/               # behavioral and mode-isolation checks
  Settings/Build Profiles/        # AndroidTouch and QuestStandalone assets
README.md                         # update only after setup is actually completed
 docs/ai-usage.md                  # material AI contributions + human review
 docs/foundation-validation.md     # evidence matrix
 docs/third-party-assets.md        # only when third-party content is included
```

**Structure Decision**: extend the existing Unity project, preserve SampleScene and metadata.
Use primitives and one shared target-color toggle; do not import complete gameplay examples.

## Implementation Stages and Exit Gates

Execute only one subsequently requested stage at a time; these are planning stages, not tasks.md.

| Stage | Manual user actions | Codex work after authorization | Exit evidence |
| --- | --- | --- | --- |
| 1. MCP | Install CLI, sign in if required, open correct project, allow Pipeline import, review Codex MCP config change, reload extension | Select/pin Pipeline version, discover tools, verify project path, read hierarchy, create/read/delete/read unique temporary object | MCP transcript and clean scene comparison; connection alone does not pass |
| 2. Smoke/non-VR | Observe Game view and keyboard/mouse behavior | Create minimal scene, shared toggle, non-VR rig and input adapters through Editor/MCP | One response per input, visible geometry, one active camera/listener, no new errors |
| 3. XR simulation | Approve/import planned packages, focus Game view and exercise simulator | Configure OpenXR/XRI, action-based XR Origin, VR-only Editor simulator and same toggle action | Pose and controller response without headset, touch-to-VR-to-touch isolation |
| 4. Build isolation | Confirm Android module/toolchain and inspect profile UI | Create native AndroidTouch/QuestStandalone profiles; validate/apply XR state and build preconditions | Repeat switching and restart; touch has no VR startup/requirements, Quest has intended loader/features |
| 5. Builds/devices | Supply hardware, enable developer mode/USB debugging, approve device trust; run comfort observation | Build distinct APKs, capture build reports/manifests; assist with deployment/profiling | Two build results; independent device outcomes, unavailable hardware pending |
| 6. Reproduction | Review instructions and AI record; repeat a documented check | Update README, evidence and attribution to match completed work | Reproduction and source/.meta review; no unsupported passed claims |

After connection Codex can use discovered Editor tools for object/component inspection,
scene/prefab edits, console reads, and tests where exposed. Package resolution, permissions,
modal dialogs, account sign-in, visual checks and physical interaction may still need the user.
Never infer tool names or operation support without inspecting the connected tool schemas.

## Mode and Build Design

- One shared smoke scene contains geometry, target and two mutually exclusive rigs.
- Touch mode uses a conventional camera and touch + keyboard/mouse bindings. VR uses an
  action-based XR Origin and controller input, with no locomotion. Both call the shared action.
- Simulator activation is Editor-only and VR-only. Windows Editor simulation does not require
  activating a physical desktop OpenXR runtime. No simulated devices enter player builds.
- Native Build Profiles: ordinary Android `AndroidTouch`, derived Meta Quest `QuestStandalone`.
  Use profile overrides for scene list, application identity and supported Player settings.
  Outputs: `BirdLifeSimulator/Builds/AndroidTouch/` and `BirdLifeSimulator/Builds/QuestStandalone/`.
- Android target equality is never a mode selector. A dedicated mode value/profile identity
  selects rig and input; generic Android compilation symbols cannot distinguish these products.
- Shared Android XR settings are a risk. In stage 4 verify loader, startup, OpenXR features and
  manifests. If native profile overrides cannot isolate them, implement one minimal idempotent
  Editor selector/prebuild validator that applies touch=no XR and Quest=OpenXR before validation.
  Do not rely on remembering checkboxes; building with mismatched state must fail clearly.
- Use Quest ARM64/IL2CPP and project validation recommendations, with the documented Quest
  controller interaction profile. Resolve graphics/API overrides against the chosen hardware.
  Touch must not inherit Quest-only required manifest features. No Meta SDK by default.

## Complexity Tracking

No constitutional violations or approved exceptions. Conditional profile guard addresses one
specific shared-Android-state risk; it is not a general build framework.
