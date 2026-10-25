# Foundation Interface Contracts

## Codex-to-Editor MCP

Transport: local stdio Unity CLI MCP server; project bound explicitly to `BirdLifeSimulator/`.
Discover actual tool schemas before use. Required observable operations: identify project/scene,
read hierarchy, create one unique temporary object, read it, delete that exact object, and read
absence. Operations may be provided by discovered Editor evaluation rather than dedicated tools.
Do not fabricate tool names. Log sanitized results and preserve original scene contents/dirty state;
if restoration cannot be established, report failure and do not save unintended changes.

## Shared input action

Intent: `ActivateSmokeTarget`; observable result: toggle the shared target color once per press.
Touch: one on-screen button; desktop test: Space or primary mouse click on the test control;
VR: controller select on a visible target/control, driven by the same shared behavior.
No continuous flight/look gameplay. Camera pose testing is separate from the target action.
Inactive mode bindings produce no response. Simulator must not consume non-VR keyboard bindings.

## Profile selection/build

Select `AndroidTouch` or `QuestStandalone` in File > Build Profiles and activate it. Apply the
profile's supported overrides and validate XR policy before Play/build. If native switching
cannot own all shared Android settings, a single Editor selector supplies this validation step;
ordinary build attempts in mismatched state fail with the required selection procedure.
Touch: no XR auto-start or Quest-only required manifest features; conventional camera.
Quest: OpenXR loader and Quest feature/controller profile; XR Origin; ARM64/IL2CPP.
Both use FoundationSmoke and separate application identifiers/output directories. No simulator
in either APK. Record effective settings and artifact identity, not merely the selected name.

## Reporting interface

README links validation evidence and explains what exists, prerequisites, mode/profile selection,
MCP setup, simulator controls, build outputs and known limitations. AI usage record names material
assistance and actual human checks. Verification statuses follow data-model.md; laptop touchscreen
observations carry a supplementary label and cannot populate Android-device pass entries.
