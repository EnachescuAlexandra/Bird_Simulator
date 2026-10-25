# Data Model: Technical Foundation

Design contracts only; no database or runtime assets are created in planning.

## ModeConfiguration

- `id`: AndroidTouch or QuestStandalone; unique, never inferred only from Android target.
- `runtimeMode`: nonVR or VR; owns exactly one active rig/input set and one audio listener.
- `editorInput`: keyboard/mouse, or simulated headset/controllers respectively.
- `playerInput`: physical touchscreen, or tracked headset/controllers respectively.
- `xrPolicy`: disabled for touch; OpenXR for Quest; simulator is Editor-only.
- `profileAsset`, `sceneAsset`, `applicationId`, `outputDirectory`: explicit reproducible values.

Both configurations reference the same scene and shared smoke action. Each input press invokes
one target toggle. Repeating a mode selection is idempotent; switching disables the old input
before enabling the new one. A mismatched profile/XR state blocks building.

## SmokeScene

Ground, distinguishable primitives, light, visible target, non-VR rig and VR rig. No movement,
physics gameplay, bird, forest, food or refuge. Target state toggles between two visible colors.
Exactly one rig is active at a time; repeated Play sessions reset to a documented initial state.

## VerificationRecord

`checkId`, requirement IDs, category (Editor/build/device), mode, timestamp, exact Unity/package
versions, host/device and OS, status, expected/actual result, evidence path, limitation/reason.
Status: pending -> passed/failed/blocked after execution; blocked -> pending when prerequisite
returns. Passed requires evidence for that exact category; build never implies device pass.
Hardware availability: unconfirmed until the user confirms model/access. Capture frame timings,
refresh rate, observation duration and comfort notes separately from visual smoke results.

## MCPVerificationSession

Project absolute path, Editor instance/version, scene identity, original hierarchy and dirty state,
unique temporary object name/ID, creation/read/deletion/read evidence and final comparison.
States: disconnected -> identified -> readVerified -> objectPresent -> cleanedUp -> verified.
Failure after creation requires explicit cleanup status; never remove pre-existing named objects.

## ContributionRecord

Date, tool/provider, material AI contribution, affected artifacts, human reviewer, checks completed,
pending review and limitations. Asset entries add source URL, author, license and required attribution.
Do not include API keys, credentials or raw account configuration.
