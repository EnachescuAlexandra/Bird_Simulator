# Research: Technical Foundation

Researched 2026-10-24 using primary documentation. Documentation compatibility is not a local
integration test. No software was installed and no Editor/build/device acceptance check was run.

## Baseline inspection

Unity 6000.3.25f1; manifest/lock: URP 17.3.0, Input System 1.20.0, Test Framework 1.6.0.
Input System is enabled (`activeInputHandler: 1`). SampleScene is the enabled build scene;
serialized objects include Main Camera, Directional Light and Global Volume. No OpenXR, XRI,
Pipeline/MCP package or separate profile assets were found. The built-in XR module alone does
not establish XR support. `unity` was not found in this shell. Android settings include minimum
API 25, ARM64 and IL2CPP; these are baseline values, not verified Quest settings. Toolchain,
license/account state, touchscreen and Android/Quest hardware availability are unconfirmed.

## Decision 1: Official Unity CLI MCP first

**Decision**: use official `unity mcp` over stdio, backed by `com.unity.pipeline`, explicitly bound
to `BirdLifeSimulator/`. Do not install the deprecated Assistant MCP or use AI Gateway.
**Rationale**: official replacement, local Editor operation, free CLI without Unity AI subscription;
Unity 6.0+ meets the documented Editor requirement. A normal Unity Editor license and the user's
Codex entitlement still apply. Installation instructions include Unity account sign-in. No extra
AI subscription, AI credits, cloud runtime or production-pipeline cloud services are needed for
this local foundation. [Official migration/licensing](https://docs.unity.com/en-us/unity-cli/replace-mcp-server-unity-cli).

**Compatibility/prerequisites**: Windows 10 21H1+; standalone CLI; existing licensed Editor.
Plan Windows installation via `winget install Unity.CLI`, not executed now. Python/uv are not
prerequisites for this selected integration. [Official installation](https://docs.unity.com/en-us/unity-cli/use-unity-cli).

**Version decision**: candidate CLI `1.0.0-beta.14`, published October 21, 2026; no stable CLI
release was established by the consulted notes. Prefer official maintained tooling despite beta
status, with the first-stage connection gate before further work. Exact Pipeline patch is not
published on the accessible install guide; do not invent it. Stage 1 first runs registry version
discovery, inspects compatibility/release notes, and records a concrete compatible version before
`unity pipeline install --package-version <selected-version>`. Keep manifest and lock pinned.
If only prerelease Pipeline packages are available, record this fact and test the reversible gate.
[Release notes and version-selection commands](https://docs.unity.com/en-us/unity-cli/release-notes).

**Capabilities**: discover Editor commands and their parameter schemas; use scene/object operations
or discovered Editor evaluation to inspect hierarchy and perform the temporary-object round trip.
Also discover console, Play mode and test capabilities. The catalog in the actual connected
package is authoritative; shell-only success cannot replace the required MCP round trip.
[CLI command reference](https://docs.unity.com/en-us/unity-cli/unity-cli-reference).

**Setup later**: open project; interactive `unity auth login` if needed; install the selected
Pipeline package explicitly into this project; allow recompilation, refocus Editor, verify
`unity pipeline list`. [Pipeline installation](https://docs.unity.com/en-us/unity-cli/unity-pipeline/unity-pipeline-package).

**Codex VS Code**: register the Codex client, not the `vscode`/Copilot client. The extension and
Codex CLI share MCP configuration. Inspect only the Unity server entry and preserve other
settings/credentials. A stdio server entry must launch the absolute CLI executable with MCP mode
and an explicit project path. [OpenAI MCP documentation](https://developers.openai.com/codex/mcp/).

Preview `unity mcp configure codex --dry-run`; review its proposed changes before applying it.
Release notes indicate this helper can relax Codex sandbox/network settings, so prefer a minimal
manual MCP entry if it proposes unrelated policy changes. User performs host configuration and
extension reload. Never print credentials or grant unrestricted execution as a connection fix.

**Alternatives considered**: deprecated Assistant MCP is unsuitable for a new setup. CoplayDev
MCP for Unity is an actively maintained free MIT fallback, with explicit Codex support, Unity
2021.3+/6.x, Python 3.10+ and uv, scene/GameObject/test operations and Git-URL installation.
Its repository lists v10.3.0 dated October 4, 2026. If the official gate fails, evaluate a pinned
stable fallback separately; do not install both or silently substitute it. [Maintainer repository](https://github.com/CoplayDev/unity-mcp).

## Decision 2: Conservative official XR packages

**Decision**: OpenXR `1.16.1` and XR Interaction Toolkit `3.3.2`, the releases designated for
Unity 6000.3. Preserve the newer already-installed Input System rather than downgrade it.
Resolve required XR Plug-in Management/Core Utils dependencies through UPM and capture exact lock
versions during implementation. Full combination compatibility remains an implementation gate.
**Rationale**: sufficient controller-based smoke behavior; avoids unnecessary upgrades.
**Alternatives**: later compatible versions only for a demonstrated fix; no Oculus provider,
Meta All-in-One, hand tracking or AR packages for this stage.
[OpenXR package table](https://docs.unity3d.com/6000.3/Documentation/Manual/com.unity.xr.openxr.html),
[XRI package table](https://docs.unity3d.com/6000.3/Documentation/Manual/com.unity.xr.interaction.toolkit.html).

## Decision 3: XRI simulator and minimal rigs

**Decision**: XRI 3.3.2 XR Interaction Simulator, imported from the sample named XR Device
Simulator; import only necessary Starter Assets. Use an action-configured XR Origin. Activate
simulation only in VR Editor mode; keep the non-VR camera and keyboard/mouse path independent.
**Rationale**: documented Input System simulation of headset/controllers without physical hardware.
**Alternatives**: physical headset/link testing supplements but cannot replace headset-free testing;
no second simulation framework. [Simulator guide](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.3/manual/xr-interaction-simulator-overview.html).

## Decision 4: Native Build Profiles with explicit isolation verification

**Decision**: use Android Touch plus the native derived Meta Quest profile. Native Quest profiles
exist in this Editor line; Android Build Support is required. Quest shares Android Player settings
by default, so use explicit overrides. Selecting Quest can install OpenXR: create profiles only
in the authorized package/configuration stage and recheck resolution afterward.
**Rationale**: simplest native reproducible approach, no external build framework.
**Alternatives**: two generic Android names alone do not prove XR isolation; adopt the narrow guard
in plan.md only if necessary. [Unity 6000.3 Quest profiles](https://docs.unity.com/en-us/engine/6000.3/manual/xr/configuring-project-for/meta-quest-develop/build-profile).

XR provider configuration is target-based. Our inference: two Android-derived profiles may share
loader/startup state not covered by ordinary Player overrides. Test touch -> Quest -> touch,
Editor restart, and both generated manifests before accepting isolation.
[XR Plug-in Management](https://docs.unity3d.com/6000.3/Documentation/Manual/xr-plugin-management.html).

## Decision 5: Evidence and device gates

Hardware availability stays unconfirmed. Laptop touchscreen checks are supplementary and never
satisfy Android touch acceptance. Simulator checks do not prove headset tracking, performance or
comfort. Record successful builds independently of launches. Proposed device budgets and five-minute
observations are project planning targets, not source claims; adapt them to confirmed hardware
before execution. This resolves the testing approach without inventing hardware availability.
