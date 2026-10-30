> Privacy normalization (2026-10-25): personal checkout/home prefixes in this record are represented by `<REPO_ROOT>` and `<USER_HOME>`. These are documentation placeholders, not literal runnable paths. Relative paths, versions, commit/artifact hashes and test results are unchanged; original Git history was not rewritten.

# Foundation Validation

## Feature 002 — static graybox only, 2026-10-30

Separate increment; existing foundation acceptance and pending device/performance checks
below are unchanged. T001 and T003 implementation/Editor verification completed; student
visual review pending. No gameplay, bird, XR integration, build/profile change or APK trial.

Baseline: branch main, HEAD `4f627ce`; nine modified files (manifest, lock, README and six
002 documents), plus untracked ProBuilder Settings.json. Their existing work was retained.
Unity 6000.3.25f1; ProBuilder 6.1.4, Pipeline 0.9.1-exp.1, Input System 1.20.0, URP 17.3.0,
OpenXR 1.16.1 and XRI 3.3.2 retained. Live FoundationSmoke was clean, Play stopped.
Main Modules final PDF supplied by the student informed the layout; ideal application
systems are planned context, not this increment's acceptance scope. README URL not changed.

Created `Assets/Scenes/FlightPrototype.unity` through the Editor using the one-time
`Assets/Flight/Editor/FlightGrayboxBuilder.cs`; ordinary primitives and six plain URP
materials under Assets/Flight/Materials, with Unity-generated metadata. ProBuilder was
available; this simple layout needed no custom mesh editing. No imported art or new package.

| Scene element | Actual dimensions in meters / placement |
| --- | --- |
| Ground | 60 x 0.4 x 60; top y=0 |
| Clearing marker | 20 x 0.02 x 20; top y=0.02, no extra collider |
| Trees | Eight 6–9 m tall; cylinder trunks 0.6 m diameter and simple spherical crowns |
| Landmarks | Additional 10 m tree; standing stone 1.5 x 5 x 1.5 |
| Shrubs | Six, each 2.8 x 1.3 x 2.2 |
| Obstacles | Boulder 3 x 2 x 3; log 5 x 0.8 x 1.4 |
| Refuge | 4 x 0.4 x 3 platform at (0,0,-18); 1.4 x 1.4 nest placeholder, open front; FutureBirdStart at (0,0.4,-17) |
| Rest surfaces | Stump diameter 2, top y=1.2; shelf 3 x 2 x 2, top y=2 |
| Food locations | (-13,0,-7), (12,0,13), (-9,0,20); each 2 m feeding patch and 0.4 m cube, no interaction/trigger |
| Perimeter/height | Four 0.4 m-high edge strips inside +/-30 m; four thin 12 m corner markers, no roof |

Executed checks: script imported/compiled successfully; Editor ready, compilationFailed=false,
Console errors=0 and warnings=0 after creation. Scene contains 75 GameObjects, 50 renderers,
39 colliders, exactly three food-location roots, eight trees plus tree landmark, six shrubs,
two landmarks and two rest surfaces. No missing scripts/materials/shaders; all new authored
assets have .meta files. All environment renderer bounds fit +/-30 m and y<=12. A 40 m
forward ray at y=1 from (0,1,-16) confirms the central corridor is clear; this is not a
complete route/collision gameplay test. Save/reopen succeeded with dirty=false. Second
generation was deliberately refused; saved scene bytes unchanged. Scene screenshot inspected
at `BirdLifeSimulator/Temp/FlightGraybox-Step1.png` (local ignored evidence).

SHA256 before/after matched for FoundationSmoke, VRRig, manifest, lock, README and ProBuilder
Settings.json. No new tracked configuration/profile/guard changes or staged files. Constitution
and AGENTS unchanged. Foundation builds/tests were not repeated. Pipeline stopped on assembly
reload with Auto Start disabled; the student restarted it, then MCP resumed successfully.

Pending manual: inspect FlightPrototype in Scene view for recognizable landmarks, usable
approaches and proportions. There is intentionally no runtime camera/AudioListener yet:
Game view/Play is not the Step 1 review route. Next is T004/T008 (primitive bird and one
reused XR rig), then viewpoints/guard, then a distinct static Quest APK and physical scale
trial. Real Quest scale, visibility, orientation and perceived comfort are not validated.

### Checkpoint review — known collider limitation

Static inspection found default SphereColliders on the non-uniformly scaled shrubs and
boulder, and a default CapsuleCollider on the flattened RestStump cylinder. These collision
shapes do not match the visible surfaces; the earlier renderer dimensions and central-ray
check do not establish contact accuracy. Keep this static scene editable, but inspect and
adjust only those colliders in Unity before T005 movement verification, including a downward
contact check on the stump's visible y=1.2 resting surface. Preserve the geometry and update
the builder consistently if its collider setup is corrected.

This checkpoint changed documentation only. MCP reported no connected Pipeline instance,
so no collider correction or new Unity compilation/runtime verification was attempted.
The scene/builder were preserved; this limitation remains pending, not a passed collision
test. Planning now uses refuge start/reset, a dedicated button for switching both static
viewpoints in one APK, and T017 real Quest flight validation before T013 gesture tuning.

## T001 Baseline — 2026-10-24

**Result**: baseline recorded; T001 complete. No installation, Unity configuration, MCP mutation,
build, or physical-device test was performed. T002 and subsequent tasks have not started.
Date uses Europe/Bucharest. Category: environment/static inspection. Requirements: FR-001,
FR-008, FR-013; this baseline does not pass their later integration acceptance checks.

### Observed environment

| Item | Observed result | Evidence |
| --- | --- | --- |
| Repository/project | Bird_Simulator / BirdLifeSimulator/ | Workspace paths and project files |
| Windows | Windows 11 Pro, 10.0.26200, build 26200, 64-bit | Win32_OperatingSystem read-only query |
| Required Unity | 6000.3.25f1, revision e1dba0a9aba4 | BirdLifeSimulator/ProjectSettings/ProjectVersion.txt |
| Installed required Editor | 6000.3.25f1_e1dba0a9aba4 | Unity.exe ProductVersion in standard Hub installation directory |
| Other installed Editor | 6000.5.3f1_c2eb47b3a2a9 | Unity.exe ProductVersion; not selected for this project |
| Unity processes | Three processes use the 6000.3.25f1 executable | Get-Process Unity; project identity/open scene not established |
| Unity CLI | Not found in this shell's PATH | Get-Command unity; not proof of absence elsewhere |
| Codex VS Code extension | openai.chatgpt 26.1021.22027, win32-x64 | Installed extension package.json |
| Bundled Codex executable | codex-cli 0.164.0-alpha.11.1 | codex --version; resolves inside that extension |
| VS Code | 1.143.1, x64; commit e646868227c3e3a9e3f24277c8c7220be42c0be7 | code --version |
| Git | 2.49.0.windows.1 | git --version |
| Python | 3.13.5 through py -3.13 | Version command; earlier python/python3 WindowsApps aliases were unusable |
| uv | 0.13.5, commit 46776a95d, x86_64-pc-windows-msvc | uv --version |
| specify command | Not found in this shell's PATH | Get-Command specify; local Spec Kit files exist |

Windows inventory initially failed inside the sandbox. A user-approved read-only check outside
it returned the OS and executable versions above. No permissions or configuration were changed.
No account configuration, credentials, or tokens were read or copied.

### Observed project packages

Both `BirdLifeSimulator/Packages/manifest.json` and `packages-lock.json` were parsed. They list
45 direct dependencies and 57 locked entries. Relevant exact versions:

| Package | Manifest | Lock |
| --- | --- | --- |
| com.unity.inputsystem | 1.20.0 | 1.20.0 |
| com.unity.render-pipelines.universal | 17.3.0 | 17.3.0 |
| com.unity.test-framework | 1.6.0 | 1.6.0 |
| com.unity.modules.xr | 1.0.0 | 1.0.0 |
| com.unity.render-pipelines.core | Transitive | 17.3.0 |
| com.unity.render-pipelines.universal-config | Transitive | 17.0.3 |
| com.unity.test-framework.performance | Transitive | 3.5.0 |

Pipeline, OpenXR, XR Interaction Toolkit and XR Plug-in Management are absent from both package
lists. The built-in XR module is not an operational XR configuration. Package entries establish
recorded dependencies, not successful import, compilation, or runtime behavior. No package changed.

### Git baseline before T001 writes

Branch: `main`. HEAD: `ed2472bd0001126b3d81f224c3d0ec9655269376`
(`Initialize Unity project`). `git status --short` returned:

```text
?? .agents/
?? .specify/
?? AGENTS.md
?? specs/
```

`git status --short -- BirdLifeSimulator/` returned no entries. This covers tracked and
unignored files; it does not audit generated ignored files. T001 adds this document and marks
its checkbox in `specs/001-technical-foundation/tasks.md`; no staging or commit is performed.

### User confirmation and later checks still needed

| Item | Status | Required follow-up |
| --- | --- | --- |
| Unity license validity/account sign-in | Unconfirmed | User confirms usable license and sign-in; no account introspection performed |
| Codex account/session entitlement | Unconfirmed | User confirms active extension authentication; installed files do not prove access |
| Correct open project/scene | Pending MCP setup | Verify explicit project path and scene through MCP in T006 |
| Android phone/tablet access, model/OS | Unconfirmed | User confirms device and physical access |
| Quest access, model/OS/controllers | Unconfirmed | User confirms hardware/access; developer mode and USB trust checked later |
| Laptop touchscreen | Unconfirmed | Optional supplementary test only; never substitutes for Android device testing |
| Android Build Support/SDK/NDK/OpenJDK | Unconfirmed | Inspect exact installed versions in T015, not in this baseline task |
| Physical performance/VR comfort | Pending | Define device-specific conditions and execute actual device checks later |

### Open deployment assumption (user-provided context)

BirdLife Simulator is intended as the bird module of a larger digital-panel project and may be
launched as a separate application. The panel OS, launcher and integration mechanism are not
confirmed. This is an open deployment assumption, not an implemented requirement or a decision
that the panel runs Android/Windows. No embedded-app integration, kiosk behavior, inter-app
communication or deployment tooling is introduced. Resolve deployment through a later authorized
specification if necessary; existing foundation target requirements remain unchanged.

### Verification summary and next action

Completed: project version/package inspection, Windows and tool version inventory, installed
Editor file inspection, Git baseline and explicit unknowns recorded. T001's expected baseline
exists; no hardware detail is fabricated. All Editor/MCP, smoke, build and device acceptance
checks remain unexecuted/pending. No feature check is marked passed based on this inventory.

Next concrete action, only when requested: T002, inspect the official Unity CLI installation
instructions, install it if still absent, and record its executable/version/channel. Do not start
Pipeline installation, MCP configuration or T003 onward as part of T002.

Material AI assistance for this baseline: Codex performed read-only inventory and drafted this
document and the T001 status update. Command outputs were checked for consistency with the
recorded versions; user review of this document and account/hardware confirmations remain pending.
A separate full AI usage record is planned in T023, not created by T001.

## T002 Unity CLI Installation — 2026-10-24

**Result**: passed; T002 complete. Category: host installation/CLI verification. T003 onward
not started. This proves the CLI executable works, not Pipeline, MCP or Editor connectivity.

### Pre-install inventory

`Get-Command unity` found no command. Checked standard standalone locations under
`%LOCALAPPDATA%/Unity/bin`, `%USERPROFILE%/.unity/bin`, the WinGet Links alias, WinGet package
inventory and current-user Unity Appx registrations. No existing CLI installation was found.
`winget list --id Unity.CLI --exact --source winget` reported no installed matching package.
This avoids duplicating a known install; it is not an exhaustive scan of all disks.

### Official installation source

Method: official Windows WinGet/MSIX distribution, documented in
[Unity CLI installation](https://docs.unity.com/en-us/unity-cli/use-unity-cli) and research.md.
Executed `winget install --id Unity.CLI --exact --source winget` with noninteractive/source/package
agreement flags. WinGet verified the installer hash and reported `Successfully installed`, exit 0.
Package ID: `Unity.CLI`; registered name: `UnityTechnologies.UnityCLI`; package version:
`1.0.0.20014`. The four-part MSIX version is distinct from the CLI semantic prerelease version.

### Verified executable and release

- Resolved command/alias: `<USER_HOME>\AppData\Local\Microsoft\WindowsApps\unity.exe`.
- Registered installation directory:
  `C:\Program Files\WindowsApps\UnityTechnologies.UnityCLI_1.0.0.20014_x64__2vrhnee42bhxm`.
- `unity --version`: `1.0.0-beta.14`, exit 0; matches the planned CLI candidate.
- `unity --help`: printed usage, options and command catalog, exit 0.
- `unity version --format json`: success, version `1.0.0-beta.14`, channel `beta`, platform
  `win32`, architecture `x64`, commit `659e85ceeba50968f42615caf8459254bb0da3d3`,
  build date `2026-10-21T14:34:53Z`.
- Post-install `Get-Command unity` resolved the alias above; WinGet inventory confirms installation.

Installation and executable checks ran with user-approved access outside the sandbox; no sandbox
policy or MCP configuration was changed. No persistent PATH edit was made manually. The package
registered its command alias. If an existing VS Code terminal cannot find `unity`, reopen the
terminal or VS Code; this session's verification resolved and executed the absolute alias.

### Scope and pending work

No Pipeline, Unity plugin/skills, MCP setup, sign-in, Unity scene/settings/package edits or builds
were performed. The CLI advertised optional `unity setup codex`; it was not executed because
agent guidance/setup belongs to T004. Account/license and hardware confirmations remain as in
T001. No manual action is currently needed to complete T002.

Next action only when requested: T003 — discover and select a compatible exact Pipeline version
before installing it into the explicitly identified project. CLI installation does not pass MCP
verification. Material AI assistance: Codex checked inventory, ran the authorized installer,
verified command results and recorded this evidence; human review of this record remains pending.

## T003 Pipeline Installation Attempt — 2026-10-24

**Result**: blocked pending Editor package refresh; T003 remains unchecked. No later task started.

### Version selection and installation

Installed CLI help was inspected. In CLI 1.0.0-beta.14 the exact-version install flag is
`--version`, not the `--package-version` spelling in the earlier planning guide.
`unity pipeline list-versions --format json` succeeded and listed 0.9.1-exp.1, 0.9.0-exp.1,
0.8.0-exp.1, 0.7.0-exp.1, 0.6.0-exp.1, 0.5.0-exp.1, 0.4.0-exp.1, 0.3.1-exp.1,
0.3.0-exp.1 and 0.2.0-exp.2. No stable version was offered.

Selected `com.unity.pipeline` **0.9.1-exp.1**, the latest published version. Read-only metadata
from `https://packages.unity.com/com.unity.pipeline` declares minimum Unity `6000.0`, compatible
at the declared-version level with this project's `6000.3.25f1`. Actual resolution/compilation
is still required. Its declared dependencies are Test Framework 1.1.33, Mono Cecil 1.11.6,
UIElements/JSONSerialize/ScreenCapture modules 1.0.0, and Newtonsoft JSON 3.0.2; these are
package requirements, not verified newly installed versions. Existing direct Test Framework
1.6.0 was not deliberately downgraded.

Executed only:

```powershell
unity pipeline install --version 0.9.1-exp.1 --project-path '<REPO_ROOT>/BirdLifeSimulator' --non-interactive --format json
```

CLI reported success, exit 0, exact project path, version 0.9.1-exp.1 and alreadyInstalled=false.
The manifest now pins that exact version. No other Unity project was targeted.

### Verification results and blocker

- `unity pipeline list --format json`: success, exit 0; identifies the correct running
  BirdLifeSimulator instance and reports Pipeline version 0.9.1-exp.1, but
  `pipelineServer.isReachable=false` and zero reachable servers.
- `unity recompile --project-path <exact project path> --timeout 45 --format json`: failed,
  exit 1, `EDITOR_NOT_RUNNING`: no Pipeline instance found for this project. The Editor process
  exists; this error does not establish that the application is closed.
- `Packages/manifest.json`: exact Pipeline version present.
- `Packages/packages-lock.json`: no Pipeline entry yet; no Pipeline directory found in
  `Library/PackageCache`. Therefore package resolution and compilation have NOT passed.
- Git reports only `BirdLifeSimulator/Packages/manifest.json` changed within the Unity project
  at this checkpoint. No scene, settings, XR package or MCP configuration was changed.

Manual next step: bring the already-open **BirdLifeSimulator** Unity 6000.3.25f1 Editor window
to the foreground, allow Package Manager refresh/import and compilation to finish, and handle
any package-resolution dialog. If errors appear, report their text without credentials. Then
resume T003 to verify the resolved lock/cache, exact installed dependency versions, compilation
and `unity pipeline list` reachability before marking it complete.

No sign-in was requested by the installer; no authentication command was run. If a later
operation explicitly requires it, the manual command is `unity auth login` in a normal terminal;
do not paste credentials or authentication tokens into chat. Do not run it merely on speculation.

Material AI assistance: Codex selected the exact version from official registry metadata,
performed the authorized project-specific install and recorded verification failures. No
agent-plugin installation, MCP setup, XR setup, commit or push was performed.

### T003 resumed verification — 2026-10-24

**Result**: passed; supersedes the pending-refresh blocker above. Exact target remains
`<REPO_ROOT>\BirdLifeSimulator`.

- Manifest, resolved lock and PackageCache metadata agree on Pipeline `0.9.1-exp.1`.
- Resolved dependency versions: Test Framework `1.6.0` (retained), Mono Cecil `1.11.6`,
  Newtonsoft JSON `3.2.2`, UIElements/JSONSerialize/ScreenCapture modules `1.0.0`.
- `unity pipeline list --format json`: exit 0; correct project, running Main Editor,
  Pipeline `0.9.1-exp.1`, reachable local server on port 7800.
- Project-specific `unity recompile --timeout 45 --format json`: exit 0, `up_to_date`,
  compiling=false, failed=false, zero errors and zero warnings.
- No sign-in was required. No MCP configuration or later task was executed.

Current Unity-project Git changes: manifest and lock, plus an observed serialized addition
`overrideShaderVariantLimit: 0` in `ProjectSettings/ShaderGraphSettings.asset`. This setting was
not manually edited by Codex; its author/source is not established by Git. It is preserved,
not reverted or treated as a deliberate foundation setting change.

Only T003 was newly checked. T004 is next; `docs/manual-setup.md` is the continuation guide.

## T004 Minimal Official Unity Guidance — 2026-10-24

**Result**: passed; T004 complete. Reused T003 verification without recompiling/researching XR.

Reviewed [official Unity Codex plugin instructions](https://docs.unity.com/en-us/ai/unity-plugin/codex)
(marketplace/plugin installation) and installed CLI help. Selected the narrower supported
`unity skill install codex --local` path rather than the full multi-feature Unity plugin.
[OpenAI skill documentation](https://developers.openai.com/codex/skills/) confirms standalone
skills in the IDE extension and repository discovery under `.agents/skills`.

Inventory found no existing Unity CLI skill in repository/user skill locations; dry-run reported
`would-install`. Executed `unity skill install codex --local --format json`: exit 0, installed.
Source: official embedded guidance shipped with Unity CLI **1.0.0-beta.14**, previously verified
build commit `659e85ceeba50968f42615caf8459254bb0da3d3`; no separate skill semantic version declared.
Location: `<REPO_ROOT>\.agents\skills\unity-cli`.
The tree contains 15 documentation files: SKILL.md, CHANGELOG.md, SECURITY.md and 12 references.
No generators, services, runtime packages or full agent plugin were installed.

Verification: `unity skill list --format json` reports local `unity-cli`, enabled=true, correct
path. SKILL.md is readable with name/description metadata. Hash comparison confirms AGENTS.md,
all ten existing Spec Kit skill files and user Codex config.toml unchanged. No MCP configuration
or sandbox policy change. CLI bookkeeping for its skill install is expected; no T005 operation ran.

This verifies installation/enabled state and the documented discovery location, not a live
VS Code skill-menu observation. On the next turn, select `$unity-cli` or `/skills` if needed;
reload VS Code only if it does not appear. No additional installation or manual configuration
is required now. New guidance does not authorize automatic upgrades or later tasks.
Next task: T005, only when requested. Codex performed installation and verification; human
review of this evidence remains pending.

## T005 Minimal Codex MCP Configuration — 2026-10-24

**Result**: configuration verified; tool discovery pending extension reload. T005 remains
unchecked until discovery is verified. T006/T007 were not executed.

Used installed unity-cli guidance and existing T003 results. Previewed
`unity mcp configure codex --dry-run --project-path <exact project path> --format json`.
The preview proposed the Unity entry AND sandbox network-policy relaxation. The helper was
not applied. Instead appended only `[mcp_servers.unity]` to the user Codex config.toml:

```toml
[mcp_servers.unity]
command = '<USER_HOME>\AppData\Local\Microsoft\WindowsApps\unity.exe'
args = ['mcp', '--project-path', '<REPO_ROOT>\BirdLifeSimulator']
```

No Unity entry previously existed. Parsed TOML before/after and compared all data except the
new entry; unchanged. Original configuration bytes remain an identical prefix, preserving
other server entries, credentials and permission/sandbox policies. No secret values displayed.
`codex mcp get unity --json`: exit 0; enabled=true, stdio transport, expected executable and
explicit project argument. Executable existence verified. No MCP tools for Unity are exposed
in this already-running session; configuration parsing does not prove tool discovery.

Manual action: in VS Code press Ctrl+Shift+P, run `Developer: Reload Window`, then open a new
Codex chat in this repository. Ask to finish T005 tool discovery only, without T006/T007.
If connection fails, report its sanitized error; do not relax sandbox policy or switch integration.
No Unity scene/settings/packages, AGENTS.md or Spec Kit skills changed by T005. No commit/push.

### T005 discovery verification after reload — 2026-10-24

**Result**: passed; T005 complete. The reloaded Codex session exposes 110 `mcp__unity__*` tools,
including `get_hierarchy`, `create_many` and `recompile_status`, with their tool descriptions.
Evidence: session tool metadata discovery, not a shell command inventory. No Editor tool was
invoked: project/scene/hierarchy verification remains T006, reversible-object test remains T007.
Only T005 was newly checked; continuation guide now points to T006. No configuration, policy,
scene or package changes were needed; no further manual reload required for T005.

## T006/T007 Live MCP Verification — 2026-10-24

**Result**: automated MCP checks succeeded; visual user confirmation pending. Leave T006/T007
unchecked until the requested visual comparison is received. No later task executed.

T006: MCP eval returned Application.dataPath
`<REPO_ROOT>/BirdLifeSimulator/Assets`, Editor `6000.3.25f1`,
Play mode=false. list_open/get_hierarchy returned active loaded `Assets/Scenes/SampleScene.unity`,
initial dirty=false, three roots: Main Camera (59272), Directional Light (59282),
Global Volume (59290), no children. Project identity matched before mutation.

T007: create_many created exactly one empty GameObject `__T007_MCP_20261024_8c4a91f2`,
instance ID -36072. Subsequent get_hierarchy confirmed that object and the original three roots;
dirty=true. MCP eval checked exact instance ID, name and scene before deleting only that object
with Undo.DestroyObjectImmediate. It returned absent=true; final get_hierarchy confirmed its
absence and the original roots/IDs/components/active states/children unchanged.

Final dirty=true versus initial dirty=false. This difference remains honestly unresolved;
no scene was saved, reopened, reverted, globally undone or forcibly marked clean. Hierarchy
comparison alone does not establish equality of every serialized property. Original objects and
unrelated edits were not intentionally changed. User asked to visually confirm SampleScene and
the three original roots with no temporary object; reply pending. The dirty indicator may remain.

Visual confirmation received: user confirmed SampleScene, the three expected roots and absence
of the temporary object. T006 and T007 complete for live identity and reversible-object checks.
Dirty-state caveat remains: false -> true, not restored; no saved scene-file diff is reported by
Git. This is not a claim of byte-identical in-memory scene serialization. No further cleanup or
scene save was performed. T008 is next only when explicitly requested.

## T008-T010 Non-VR Smoke Implementation — 2026-10-24

T008 complete: inspected active scene once through MCP. Correct project/Editor; existing scene
is already `Assets/Scenes/FoundationSmoke.unity`, initially clean, not SampleScene. Reused its
Ground, SmokeTarget, Main Camera, Directional Light and Global Volume without recreation/rename.
User reported correct Play rendering and no Console errors before implementation.

T009 complete: added `Assets/Foundation/Runtime/SmokeTarget.cs`: shared Activate method toggles
cyan/yellow with a renderer property block, activation counter, and initial-state reset in Awake.
Play test observed initial count=0/cyan, then one button event gave count=1/yellow.

T010 implemented; manual keyboard/pointer verification pending, checkbox left unchecked.
Added NonVRSmokeInput.cs and Input/NonVR.inputactions under Assets/Foundation. Space action and
one on-screen button invoke the same target method. Button navigation and UI submit/move/cancel
are disabled to prevent keyboard UI submission duplicating Space. Existing non-VR camera reused
in place rather than creating a duplicate rig/prefab. One active camera and AudioListener verified.
InputSystemUIInputModule supplies mouse/touch pointer handling; no packages installed.
MCP wired references and saved only the initially clean FoundationSmoke scene; metadata generated
by Unity. Existing geometry preserved. Two setup eval snippets initially failed compilation due
to eval namespace/extension-method syntax, without executing; corrected explicit static calls
succeeded. Script compilation status completed, failed=false, errors=[] after implementation.

Verification limitation: simulated Space calls returned success but activation count remained 0;
frame count was frozen at 1, then 1199 after focusing Editor. Cannot claim end-to-end keyboard
pass from those calls. Direct button.onClick invocation produced exactly +1; this validates event
wiring, not physical pointer routing. Exited Play mode. No background/tick settings changed.

Pending short manual test: open FoundationSmoke, Play, click Game view, press/release Space once,
then click the Toggle target button once, then press Space again. Expected cyan -> yellow ->
cyan -> yellow, exactly one change each; no new Console errors. Stop/Play resets to cyan.
Physical touch/Android tests remain pending. T011 onward not executed; no commit/push.

### T010 user-reported manual verification — 2026-10-24

User reports Space -> button click -> Space produced cyan -> yellow -> cyan -> yellow,
exactly one change each, with no Console errors. T010 complete based on this user-reported
manual check plus prior automated wiring/compilation evidence. Physical Android touch and
restart/reset behavior were not independently reported by this confirmation.

## T011 XR Package Installation — 2026-10-24

**Result**: passed; T011 complete. Used MCP PackageManager.Client.AddAndRemove with exact
`com.unity.xr.openxr@1.16.1` and `com.unity.xr.interaction.toolkit@3.3.2`, only in the verified
BirdLifeSimulator project, outside Play mode. No samples requested or XR rigs created.

Actual manifest/lock: OpenXR **1.16.1**, XRI **3.3.2**. Transitive lock versions: XR Plug-in
Management **4.6.1**, XR Core Utils **2.6.0**, XR Legacy Input Helpers **2.1.13**.
Input System remains **1.20.0**; URP remains **17.3.0**. Comparison with HEAD shows no existing
package version upgrades; Pipeline/Newtonsoft additions predate T011.

Import generated XR/XRI settings assets and metadata automatically, including OpenXR package
settings, interaction-layer/editor/simulator settings, and an OpenXR configObjects entry in
EditorBuildSettings. These are package initialization artifacts, not imported sample content
or deliberately configured loaders/rigs. No Assets/Samples folder found. Existing unrelated
working-tree changes were preserved.

During import MCP eval returned `socket hang up`. Follow-up CLI project-specific recompile
succeeded, exit 0: up_to_date, failed=false, errors=0, warnings=0; lock confirms successful
resolution. No alternative versions or repeated research were needed. Runtime XR/device
compatibility is not established by this compilation check. T012 onward not executed.
Next: T012 only when requested; no manual action needed to complete T011. No commit/push.

## T012 stationary legacy simulator setup — 2026-10-24

Implemented; manual acceptance pending, T012 remains unchecked. Continued interrupted import:
Starter Assets and modern XR Interaction Simulator were already imported by the interrupted
request. Preserved those files; imported only missing XR Device Simulator legacy on explicit
user preference. Modern sample is not instantiated. No package versions changed.

Reused Starter Assets `Prefabs/XR Origin (XR Rig).prefab` and legacy `XR Device Simulator.prefab`.
Saved connected variant `Assets/Foundation/Prefabs/VRRig.prefab`, scene instance named VRRig,
inactive by default. Existing FoundationSmoke geometry/non-VR setup retained. Disabled sample
Locomotion subtree, CharacterController, teleport/gaze interactors; no flight or locomotion added.
Added Foundation/Runtime/VRSmokeTest.cs: right XR controller triggerPressed invokes shared
SmokeTarget once per press. Activating this local test rig temporarily disables Main Camera and
FoundationCanvas/input; deactivation restores their prior states. This is a T012 local harness,
not T013 platform selection. Scene references are instance overrides, internal simulator in prefab.
Simulator tagged EditorOnly and activated only when Application.isEditor. Global simulator
auto-instantiation is disabled. Scene saved initially from clean state.

Exact roots: Main Camera; Directional Light; Global Volume; Ground; SmokeTarget;
FoundationCanvas; FoundationEventSystem; VRRig. Key rig paths: VRRig/Camera Offset/Main Camera,
VRRig/Camera Offset/Left Controller, VRRig/Camera Offset/Right Controller,
VRRig/Locomotion (inactive), VRRig/XR Device Simulator (EditorOnly).

Compilation: project-specific CLI recompile exit 0, up_to_date, failed=false, errors=0/warnings=0.
Runtime verification interrupted: after entering Play, three MCP calls (activate/restore/stop)
reported no Pipeline instance. Thus runtime switching/select correctness is not claimed passed.
No loops/alternative rigs used. Manual acceptance and T012 checkbox remain pending.

Actual legacy asset bindings: hold right mouse to manipulate head; hold Space to manipulate
right controller; left Ctrl is rotation override; left mouse is trigger; WASD/QE translate the
selected simulated device (simulation positioning only, not game locomotion). Source: imported
XR Device Simulator Controls and controller inputactions.

Manual test: stop Play if still active; enter Play, activate VRRig checkbox in Inspector, focus
Game view. Hold right mouse and move mouse for head pose; hold Space + left mouse click once
for right-controller trigger. Expect one target color toggle, no non-VR Space toggle. Disable
VRRig to restore camera/button and confirm non-VR Space still works. Report result/errors before
checking T012. No T013 onward, commit or push.

### T012 file-only follow-up — 2026-10-24

User reports simulated perspective movement and manual Android OpenXR configuration/Fix All;
VR input acceptance not yet reported. T012 remains unchecked. Pipeline isolation remains
m_AutoStart=false; no MCP calls, installs or setting changes performed.
Static wiring verified: VRRig's VRSmokeTest target override references scene component
1313304142 (BirdLife.Foundation.SmokeTarget). Its runtime Button action binds
<XRController>{RightHand}/triggerPressed and invokes target.Activate on WasPressedThisFrame.
Legacy sample uses Space for right-controller manipulation and left mouse for trigger.
Recent Editor.log tail (220 lines, last write 20:05:04 local) contains no Exception/error CS;
this is a limited log observation, not a newly executed compilation/runtime test.

Discrepancy: on-disk OpenXR Package Settings Android feature records for Oculus Touch,
MetaQuestFeature and OculusQuestFeature still serialize m_enabled=0. Unity m_Enabled=1 is
not the OpenXR feature toggle. User-reported checkbox changes may be unsaved or in another
settings asset; do not claim verified Android Quest configuration from these files.
No correction attempted; Android loader setup is not needed for legacy simulated-input testing.

Single pending manual test: Play FoundationSmoke, enable VRRig and focus Game view. Hold Space,
click/release left mouse once, release Space; repeat once. Expected cyan -> yellow -> cyan,
one toggle per click, no extra Space toggle, no Console errors. Report outcome before T012 completion.

### T012 acceptance and T013 implementation — 2026-10-24

User confirms T012 manual cyan -> yellow -> cyan, one change per click. T012 marked complete
as user-reported manual acceptance. T013 code implemented; checkbox remains pending Unity
compilation/runtime verification while Pipeline isolation is preserved. No MCP calls or T014 work.

Added Assets/Foundation/Runtime/FoundationMode.cs and new GUID metadata. At Play startup in
FoundationSmoke only, it creates a runtime-only FoundationMode root, discovers the existing named
rigs, and defaults to explicit AndroidTouch mode. Inspector Selected Mode or SelectMode API
chooses AndroidTouch/QuestStandalone independently of platform. Switching disables old canvas,
UI EventSystem, camera and VR rig before activating the selected set; repeated same selection is
idempotent. Existing VRSmokeTest now owns only VR select/simulator, avoiding competing restore logic.
Legacy serialized camera/canvas references retained to avoid unnecessary prefab changes.
No scene YAML or prefab edits made while Editor tooling is isolated; runtime bootstrap wires the
existing scene without persisting another rig. Simulator retains EditorOnly tag and Editor check.

Static checks: git diff --check passed; source reviewed for unsubscribe/disposal and old-input-first
switching. Editor has not yet imported the new script (no Unity-generated metadata before metadata
creation); no new compilation/runtime pass claimed. Last inspected Editor.log tail has no new
exception/compiler error, which does not verify this new code. User must allow refresh before test.

OpenXR discrepancy: current serialized Android OculusTouchControllerProfile and MetaQuestFeature
have m_enabled=1 (also Touch Plus/Pro=1). File timestamp 20:07:20 is later than the earlier disabled
snapshot. Earlier statement described then-current disk state, not current UI or proof of failed
configuration. Exact cause of delayed persistence cannot be determined retrospectively; no evidence
justifies changing these features. OculusQuestFeature legacy remains 0 and is distinct from enabled
MetaQuestFeature. XRGeneralSettingsPerBuildTarget currently contains only Standalone with empty
loaders: Android provider/loader persistence is not proven by feature flags alone. Leave unchanged;
no Android build configuration task is authorized here.

Git inventory checkpoint: 663 paths = 4 tracked modifications + 659 untracked. Categories:
2 package files; 18 Foundation/scene paths; 11 docs/specs/AGENTS; 30 Spec Kit; 15 Unity guidance;
562 sample paths plus Samples.meta; 20 XR/XRI settings; 4 ProjectSettings paths.
Necessary project/tooling/docs: package manifests/lock, Foundation code/scene and paired metadata,
required referenced sample assets, XR configuration, Spec Kit/guidance/docs. Review before staging:
large sample demos and unused modern simulator imported before legacy choice (do not remove blindly;
retain required prefab dependencies), auto-generated XR/XRI settings and unrelated ProjectSettings
serialization/isolation configuration. Generated Library/Temp/Logs/UserSettings are ignored and not
part of the 663 paths; imported samples/Unity settings are generated-origin but may need versioning.
Nothing deleted, staged, committed or pushed.

### T013 verification follow-up — 2026-10-24

User reports FoundationMode exists only in Play and both selected modes render. Expected for
runtime-only bootstrap; this does not establish camera/listener/input exclusivity.

Read-only findings: saved FoundationSmoke root Main Camera has m_IsActive=1. User-observed
inactive Main Camera and dirty Edit scene therefore differ from disk and remain unsaved.
FoundationMode has no ExecuteAlways/ExecuteInEditMode and performs switching only through its
Play bootstrap/Update; no save/dirty/reset calls exist. This supports a possible leftover manual
Edit-mode toggle, but exact authorship and other dirty edits cannot be determined from files.
Do not save, revert, clear dirty, or automatically enable the camera.

Assembly-CSharp.dll timestamp 20:12:15 is newer than FoundationMode.cs (20:10:54), supporting
that Unity processed the script. Inspected Editor.log includes a compilation request and no
matched error CS/compilation-failed entries. This is not a fresh completed recompile result.
Static code review: disables nonVR canvas/input/EventSystem and camera, then VR rig before
activating selected mode; repeated mode request early-returns; VR adapter disposes its action
on disable. Actual counts/input state remain unverified.

CLI read-only pipeline list: correct running project, no Safe Mode, Pipeline server unreachable.
CLI status: STATUS_PIPELINE_LOAD_PENDING; known isolation m_AutoStart=false explains why no
server is available, rather than proving a compile failure. No MCP attempted and isolation unchanged.

T013 remains unchecked. To permit live verification only if Editor is stable: user chooses
Window > Pipeline > Start Server (temporary, do not change Auto Start), then reports it started.
Next verification must capture current Edit-mode dirty/object states before touching Play; compare
saved/live values, then inspect each mode's active Camera/AudioListener/input counts and action
state without saving/resetting the scene. T014 not started.

### T013 live verification completed — 2026-10-24

**Result**: passed; T013 checked. T014 not started. Correct live project and FoundationSmoke
confirmed. User temporarily started Pipeline in Play after its initial domain-reload disconnect;
Auto Start/isolation configuration was not changed.

| Sequence | Active Cameras | Active AudioListeners | NonVR component/action | VR component/action | Button delta | Right trigger delta |
| --- | --- | --- | --- | --- | --- | --- |
| AndroidTouch | 1 | 1 | enabled | disabled | +1 | 0 |
| QuestStandalone | 1 | 1 | disabled | enabled | 0 | +1 |
| AndroidTouch | 1 | 1 | enabled | disabled | +1 | 0 |

Repeated SelectMode(same mode) before each measurement did not duplicate callbacks. Tested input
with temporary synthetic right-hand controller (removed afterward), actual Button.onClick event,
and action/component enabled state. Active VR select responded +1, disabled VR select 0; hidden
nonVR button had no listener response in VR. Space action disabled in VR, enabled in touch.
Synthetic keyboard events were inconclusive (also zero in active touch); not counted as proof.
User then confirmed a physical Space press in the returned AndroidTouch mode toggles exactly once.
This combines runtime isolation evidence with explicit user manual keyboard evidence, not Android
hardware validation. No test scripts/assets were added. One attempted eval used an unavailable
public InputSystem.Update overload and one bitfield Change overload; neither was a project compile
error. Corrected test used reflected dynamic input update; temporary devices removed in finally.
MCP recompile_status: completed, failed=false, compilationFailed=false, errors=[].

Unsaved Edit state identified before Play by comparing live objects to an isolated preview of the
saved scene, then closing only that preview: Main Camera saved active=true/live=false;
VRRig saved active=false/live=true. Additional normalized-property differences were Canvas layout
(position/size/scale/pivot, and button local Y), consistent with Editor UI layout calculation;
not established as manual edits. Object-reference instance IDs were excluded from comparison.
No source-of-edit attribution is provable. Runtime-only FoundationMode does not write scene files.

After exiting Play, live Main Camera=false, VRRig=true, FoundationCanvas/EventSystem=true,
dirty=true: original observed Edit flags preserved. No save, revert/reset or dirty clearing.
Disk scene SHA256 before/after unchanged:
A5CC528500FFA91FA66460F2909635F74122A185C6CE0C122C30218082941AD8.
Preview comparison is serialized-property inspection, not a claim of exhaustive byte-equivalence
of live memory; the identified activation differences and dirty state deliberately remain.

### T014–T018 progress — 2026-10-24

User clarification: Meta Quest standalone is the primary target, Android touch secondary.
FoundationMode's Quest default now requires `BIRDLIFE_QUEST && !UNITY_EDITOR`:
Quest player=VR, AndroidTouch player=non-VR, Editor default=non-VR regardless of profile.
Verified the conditional in source; compilation/runtime verification of this refinement
remains pending Pipeline reconnection. No additional task is marked complete.

- Reused T010/T012 user input/rendering confirmations and T013 live exclusivity/idempotence
  evidence. T014 still needs an explicit repeated-Play/reset observation; no redundant test
  suite was added and that criterion is not claimed passed.
- Scene differences clarified: Canvas is Screen Space Overlay with Canvas-driven root layout;
  button anchors/pivot and derived local position were preserved. User requested the initial
  non-VR state. Through Editor/MCP set Main Camera active, VRRig inactive and saved
  `Assets/Scenes/FoundationSmoke.unity`; SaveScene=true, dirty=false afterward.
- T015 verified: Unity 6000.3.25f1 Android Build Support installed and supported; bundled SDK,
  NDK and OpenJDK exist under that Editor's `Data/PlaybackEngines/AndroidPlayer/`.
  SDK Build Tools 36.0.0, platform-tools 36.0.0, command-line tools 16.0; installed platforms
  android-34/35/36/37.0. NDK 27.2.12479018 (r27c); Temurin OpenJDK 17.0.18+8.
  java.exe, adb.exe and clang.exe present; no custom EditorPrefs tool-path overrides found.
  No Hub installation needed. This is prerequisite inspection, not a successful APK build.
- Created native Build Profiles under `Assets/Settings/Build Profiles/`: ordinary Android
  `AndroidTouch.asset` and derived Meta Quest `QuestStandalone.asset`, each with only
  FoundationSmoke enabled and its own `BIRDLIFE_TOUCH` / `BIRDLIFE_QUEST` define.
  Player overrides: `com.birdlife.simulator.touch` / `com.birdlife.simulator.quest`, min SDK
  25 / 29, target SDK 36, ARM64 and IL2CPP. Effective activated settings remain to verify.
- Android XR management is shared by target group rather than these profile overrides.
  Added the planned minimal `Assets/Foundation/Editor/FoundationBuildGuard.cs`: explicit
  BirdLife profile selection, touch=no loaders/startup/features, Quest=OpenXR plus Meta Quest
  Support and Oculus Touch Controller Profile; selected scene/defines/XR/output validation
  blocks mismatches. Outputs are `Builds/AndroidTouch/AndroidTouch.apk` and
  `Builds/QuestStandalone/QuestStandalone.apk`. FoundationMode defaults from the explicit
  profile define, preserving its existing runtime selector. No Windows restriction added.
- Compilation before target switching: completed, failed=false, errors=[]. Initial selection
  of AndroidTouch triggered Android asset/shader reimport; the 30-second eval timed out,
  followed by a 15-second read timeout while imports were still advancing in Editor.log.
  Import-time Terrain/Lit dependency/unsupported-shader warnings observed; no claim yet
  that the Android import or profile isolation checks have passed. No repeated retry loop.
- T016/T017/T018 remain unchecked until effective profile settings, switching, guard behavior
  and restart have been verified. T019 not started; no APK/device result claimed.
- Subsequent MCP read reached the Editor: active target Android, active profile
  `Assets/Settings/Build Profiles/AndroidTouch.asset`, isCompiling=true, isUpdating=false.
  ValidateActive returned `Android XR settings missing.` before the post-compilation selector
  had run. Recompile status still compiling, failed=false, errors=[]. A later read timed out
  during the compilation/reload transition. Await user confirmation of completed compilation
  and manual Pipeline Start Server; do not count this intermediate state as profile validation.

### T016/T017 effective settings verified — 2026-10-24

After the user restarted Pipeline: correct live project, Unity 6000.3.25f1, Edit Mode,
FoundationSmoke dirty=false; compilation completed, failed=false, errors=[].

| Effective profile | Application ID | Min / target SDK | Backend / ABI | Android XR |
| --- | --- | --- | --- | --- |
| AndroidTouch | com.birdlife.simulator.touch | 25 / 36 | IL2CPP / ARM64 | No loaders, startup disabled, no enabled OpenXR features |
| QuestStandalone | com.birdlife.simulator.quest | 29 / 36 | IL2CPP / ARM64 | OpenXR loader/startup; only MetaQuestFeature and OculusTouchControllerProfile enabled |
| AndroidTouch again | touch restored | 25 / 36 | IL2CPP / ARM64 | Zero loaders and zero enabled features restored |

ValidateActive returned null for all three states; this checks initialization/automatic loading/
running as well as loader/features, scene and defines. Player assembly defines observed:
Quest=BIRDLIFE_QUEST only; returned touch=BIRDLIFE_TOUCH only. Correct absolute APK output
paths observed in both modes. Temporary inverted startup flag was rejected with
`XR startup policy mismatch.`; finally restored and validated null. No build was launched.
Profile switching worked through the running Editor without another module installation.
T016/T017 checked; T018 still pending actual Editor restart/repeated-selection checks.

T014 repeated-Play attempt: MCP start reported entered Play; the following runtime query
timed out after 15 seconds during the Play transition with Auto Start disabled. Its button
invocation may execute if queued; no result/response/reset is counted as passed. User must
stop Play before the final manual test. Prior T010/T012/T013 results remain valid and reused.

### T014/T018 guard regression correction — 2026-10-24

User-reported result: QuestStandalone persisted after Editor restart. Two Play sessions both
started non-VR; each reset to cyan and one button click changed it to yellow exactly once.
However, the run failed overall: `Attempt to add OpenXRLoader for Android while in Play mode`
and `Foundation profile configuration failed: Cannot assign OpenXR loader` originated from
the old CheckSelection/ApplyActive path. T014/T018 remain unchecked pending correction checks.

Removed EditorApplication.update polling. Synchronization is now a single delayed attempt
on initialization, compilation completion, explicit selection and EnteredEditMode; it never
reschedules itself. Play lifecycle state is persisted through domain reload using SessionState;
pending callbacks are canceled on Play transitions. ApplyActive and profile selection independently
reject Play, transitions, compilation/import and building before any mutation. Existing OpenXR
loader is reused without reassignment. Prebuild remains read-only validation and rejects
mismatched state; it does not repair XR settings during a build.

MCP reconnected in Edit Mode to the correct project with dirty=false. Requested source refresh
for recompilation; results and post-correction Play acceptance still pending. No scene/settings
reset, package installation, build, staging, commit or push.

After source refresh, Editor.log reports compilation followed by `Mono: successfully reloaded
assembly`; Assembly-CSharp-Editor.dll was regenerated. No C# error was observed in that reload
section. XR Management emitted `Call to StopSubsystems without an initialized manager` during
OnDisable on assembly reload: recorded as a warning, not a clean-Console claim. Pipeline
disconnected with Auto Start disabled; recompile_status could not connect. Live guard/Play
revalidation therefore remains pending; prior user restart/reset results do not pass this fix.

### T014/T018 completed after correction — 2026-10-24

User confirmed the requested post-fix test passed: two Play -> click -> Stop sessions,
non-VR/cyan startup/reset, one yellow change per click, without the reported loader/guard
errors. This is user-reported manual evidence; earlier restart persistence, simulator/controller
and T013 exclusivity checks are reused rather than repeated. No device test is inferred.

Live MCP after confirmation: correct BirdLifeSimulator project, Edit Mode, isCompiling=false,
QuestStandalone active, OpenXR loader present, ValidateActive=null and FoundationSmoke
dirty=false. Loaded corrected guard reports CanSynchronize=true. Recompile status completed,
failed=false, compilationFailed=false, errors=[]. A reversible in-memory test temporarily set
only the guard's stableEditMode flag false: ApplyActive rejected with the stable-Edit-Mode
message before settings mutation. Flag restored in finally; ValidateActive remained null,
dirty=false. This tests the rejection gate, not an additional physical Play/input simulation.

T014/T018 checked using these results plus the existing profile-switch/mismatch evidence.
Historical XR reload warning is retained above. Updated only tasks and existing evidence/
continuation guide; T019 not started, no build/device pass, staging, commit or push.

### T019 build execution — 2026-10-24

User authorized only both APK builds and their verification. Quest built first because it was
already active and is the primary target. Before build: correct project/Unity 6000.3.25f1,
stable Edit Mode, FoundationSmoke clean and sole enabled scene, ValidateActive=null,
com.birdlife.simulator.quest, BIRDLIFE_QUEST only, ARM64/IL2CPP, Vulkan, APK output.
OpenXR policy/startup validated. Used the running Editor's BuildPipeline.BuildPlayer with
BuildPlayerWithProfileOptions and DetailedBuildReport; no second/batch Editor.

**QuestStandalone: Succeeded**, 0 errors, 6 warnings, BuildReport duration 00:26:04.1559250.
APK: `<REPO_ROOT>\BirdLifeSimulator\Builds\QuestStandalone\QuestStandalone.apk`
(47,231,216 bytes). BuildReport totalSize includes other output/debug files and is not APK size.
APK SHA256: C30F0ED1E514CAA9A81D0A293FFCBCEB5722C84D66A3B05FD96DEFAB31AFA408.
Ignored sibling files `QuestStandalone.build-result.txt` and `QuestStandalone.manifest.txt`
preserve report messages and aapt manifest dump. MCP hit its 300-second tool timeout during
shaders, but build continued; no duplicate build was started. The result file and APK confirmed
completion afterward.

Bundled SDK aapt inspection: application ID com.birdlife.simulator.quest, min/target SDK 29/36,
arm64-v8a, OpenXR permissions/runtime queries, required android.hardware.vr.headtracking,
com.oculus.intent.category.VR and supported Quest device metadata. Warnings: optional
PoseControl/StickControl migration, Quest latency preference, thin LTO, diagnostics debug symbols,
and significant SSAO overhead on device. No settings changed to silence these; SSAO/performance
must be evaluated on hardware, with no performance/comfort pass claimed.

Simulator checks: instantiated legacy simulator has EditorOnly override; auto-instantiation
settings false and EditorOnly=true. Packed-assets report contained simulator source-script
metadata and the disabled settings resource, but no simulator sample prefab/input asset.
This is build/source inspection, not physical runtime verification or a claim that simulator
source metadata is absent from every build artifact.

Resolved packages remain OpenXR 1.16.1, XRI 3.3.2, Input System 1.20.0, URP 17.3.0.
Git check-ignore confirms both APK destinations and Quest evidence files are ignored.

**AndroidTouch: pending**. Profile switched; transient XR startup mismatch detected before
its recompilation settled. ApplyActive correctly refused unstable Edit Mode; no touch build
started with mismatched XR. During domain reload Pipeline timed out/disconnected with Auto
Start still disabled; user asked to start server after compilation. T019 remains unchecked
until touch build and final isolation checks succeed. No device test, staging, commit or push.

### T019 completed — 2026-10-24

After user restarted Pipeline, AndroidTouch preflight passed: correct project, stable Edit Mode,
clean FoundationSmoke as sole enabled scene, ValidateActive=null, zero loaders/XR startup/features,
com.birdlife.simulator.touch, BIRDLIFE_TOUCH only, ARM64/IL2CPP and matching APK output.
Used the same running-Editor profile-aware BuildPipeline API and DetailedBuildReport.

**AndroidTouch: Succeeded**, 0 errors, 1 warning, duration 00:08:08.2960941.
APK: `<REPO_ROOT>\BirdLifeSimulator\Builds\AndroidTouch\AndroidTouch.apk`
(42,110,316 bytes); SHA256 F630E5F7FEF6064737112220F41975FE5BD4D8A0D2FC6042E9BD4D9E6E4DCFB4.
The warning concerns Diagnostics Data/debug symbols, not a compilation failure. MCP's 300-second
call timeout again did not stop the build; no duplicate build was launched. Final BuildReport,
APK and ignored AndroidTouch.build-result.txt confirmed success.

Bundled aapt inspection and ignored AndroidTouch.manifest.txt: touch application ID,
min/target SDK 25/36 and arm64-v8a; no OpenXR permissions/runtime queries, Oculus VR category,
Quest metadata or required VR headtracking found. Contrast with the Quest manifest above
establishes artifact-level platform requirement separation, not just different profile names.
Both versionCode=1/versionName=0.1.0. Quest artifact is reused unchanged, not rebuilt.

Latest packed-assets inspection: no simulator sample prefab/input assets; only the disabled
XRDeviceSimulatorSettings resource among non-script simulator paths. Legacy instance remains
EditorOnly, auto-instantiation=false and EditorOnly=true. Simulator source-script metadata may
remain in build reports; these checks do not assert absence of every simulator-related byte.
No on-device runtime test is inferred from source stripping or a successful build.

Final MCP: BuildReport Succeeded, totalErrors=0, totalWarnings=1; ValidateActive=null and
scene dirty=false, AndroidTouch left active. Both APKs and build evidence are Git-ignored.
Resolved Unity/package versions unchanged from preflight; no package installation or upgrade.
T019 checked. T020 onward not executed. Android touch, Quest tracking/input, measured
performance and comfort tests remain pending/unconfirmed hardware. Quest SSAO warning remains
an explicit device-performance concern. No staging, commit or push.

## Current handoff: T020-T025 — 2026-10-25

This section supersedes older pending/not-started status statements above; earlier entries
remain historical evidence. No implementation changes, builds, installs or device tests were
performed during this documentation/file-audit stage.

### User-reported device evidence

| Check | Status and evidence | Limits |
| --- | --- | --- |
| Quest controller rendering/tracking | Passed, user-reported: controllers visible and follow movement on Meta Quest 2 | Quest described as updated; exact OS/build and refresh setting not supplied |
| Quest shared action | Passed, user-reported: trigger changes cube color | No new measured callback count or independent operator observation claimed |
| AndroidTouch functionality | Passed, user-reported on Android 15 | Android model and detailed step/timing log not supplied; do not expand this report into a performance pass |
| Sustained performance/frame timing | Pending, not measured | No verified fps, frame-time distribution, thermal behavior or timed observation |
| Quest comfort | Pending, not reported | No duration/comfort protocol result or absence-of-discomfort claim |

User is the reporting operator; exact execution time and APK hash installed on each device
were not captured. APK identities from T019 remain available, but installed hash equality was
not independently verified. No redeployment/retest was requested here. Proposed Android 60 fps
and Quest 72 Hz (approximately 16.7/13.9 ms) plus five-minute warm-up are planning candidates,
not measured or frozen device targets. Confirm refresh settings, observation duration and
actual device identities before closing T020/T021.

### Task disposition

| Task | Outcome |
| --- | --- |
| T020 | Partial: hardware access, Quest 2 and Android 15 reported; exact metadata and frozen measurement conditions pending. Unchecked |
| T021 | Partial: functional device reports recorded above; required performance/comfort evidence pending. Unchecked |
| T022 | Complete: README now describes installed versions, MCP, Editor/simulator test, profiles, outputs and truthful limits. Checked |
| T023 | AI and third-party records authored and locally sourced; human review of assistance/attribution not yet reported. Unchecked |
| T024 | Not executed: earlier input/profile checks predate this README and do not prove reproduction from it. Unchecked |
| T025 | File/GUID audit and requirement mapping complete; final human review and dependencies remain open. Unchecked |

### Git audit and commit selection

At resumption: 667 untracked files plus 11 modified tracked files (678 status entries).
The successful interrupted changes were the scoped .utmp ignore and removal of the temporary
constitution sync-review comment; README/AI/continuation updates had not yet been completed.
This run reused those edits. The ignore removes 101 generated CMake/GameActivity files from
the eligible list without deleting them. No additional blanket sample or asset ignores added.

| Untracked group at audit | Files | Recommendation |
| --- | --- | --- |
| XRI samples, including metadata | 563: Starter Assets 320, legacy simulator 111, modern simulator 129, parent metadata 3 | Preserve all for this handoff; Starter/legacy dependencies required. Modern simulator/demo pruning needs separate Editor review, not .gitignore |
| Other Unity project files | 48 | Include authored Foundation, smoke scene, build profiles, XR/XRI settings and metadata; review local Pipeline and other settings intentionally |
| .agents / .specify | 25 / 20 | Versioned guidance, workflows, constitution/templates and installation metadata; not conversation transcripts. Existing .specify ignore keeps local feature pointer/adapter state out |
| AGENTS / specs / docs | 1 / 8 / 2 before new records | Include project rules, specs, validation/continuation and new AI/attribution documents |

Sample tree sizes: Starter Assets 8.97 MiB, legacy simulator 1.06 MiB, modern simulator
1.17 MiB. No untracked file exceeded 5 MiB; largest was Concrete_Albedo.tif, 2,220,452 bytes.
All untracked paths/sizes were enumerated. Filename audit found no obvious personal/chat/
transcript files, secret-key filenames, generated logs, binaries or build outputs after ignore.
This was not a claim of exhaustive credential/content scanning. Documentation records technical
evidence and material AI assistance, not a raw chat export. No files were deleted.

Asset audit from the interrupted run is reused: zero missing .meta, zero orphan .meta,
zero duplicate Assets GUIDs and zero changed tracked .meta files. This run changed no Assets.
Build success provides additional prior reference evidence; no fresh exhaustive serialized-reference
or package-license audit was performed. Git check-ignore confirms .utmp, Library, Temp, Logs,
Obj, UserSettings and both APK paths ignored, while source/.meta/Packages/ProjectSettings stay
eligible; no generated-cache paths are already tracked.

Review before selecting a commit (preserved, not reverted): UnityConnectSettings enabled flag;
QualitySettings replacement of PC quality by Meta Quest and changed platform defaults;
Mobile_RPAsset/DefaultVolumeProfile/URP global serialization and prefilter changes;
Pipeline Auto Start=false. Successful mobile builds do not establish future Windows quality
configuration or explain authorship of all Editor serialization changes. Do not hide these
settings in .gitignore. Native profiles and package changes are required project configuration.

### Requirement coverage

| Requirements | Evidence / remaining limit |
| --- | --- |
| FR-001 | T006/T007 MCP identity/hierarchy/object round trip and user visual confirmation; initial dirty caveat retained, later scene save explicitly authorized |
| FR-002-FR-005 | T008-T014 scene/shared action, Editor modes/simulator/input isolation; user functional device reports above |
| FR-006-FR-007 | T016-T019 native profiles, effective XR checks, successful distinct APKs and manifests |
| FR-008 | Editor/build/device outcomes separated here and in earlier evidence |
| FR-009 | Partial: basic touch/tracking/trigger reported; performance/comfort pending |
| FR-010 | Headset-free path retained; Windows player and its quality configuration untested |
| FR-011 | README/continuation updated; human reproduction T024 pending |
| FR-012 | AI/source/license records written; human review pending |
| FR-013 | Earlier installed-version/build evidence and meta/GUID audit; flagged setting changes require review |
| FR-014 | Foundation only; no flight, food, refuge, bird model, forest, kiosk or inter-app implementation added |

Milestone is functionally exercised but not fully device/performance/comfort-validated or
independently reproduced. Commit candidates are identified, not staged or globally approved.
No staging, commit or push performed. Remaining actions are in manual-setup.md.

Final documentation checks: local README/record links resolve; git diff --check reports no
whitespace errors. Current list is 669 untracked plus 12 modified tracked files (681 entries),
including the two new provenance records and README update; index contains zero staged files.

### AI disclosure and identified follow-up hardware — 2026-10-25

Student requested explicit academic disclosure. README now links a file/component-level AI
register covering the five Foundation scripts, input asset, scene/prefab wiring, package/build
configuration and documentation. It separates generated/edited assistance from student-supplied
geometry, manual configuration, scope and tests. No line-level authorship percentage or full
human review is inferred. T023 remains pending that review.

Student nominated Samsung Galaxy S24 Ultra and Meta Quest 2 for detailed testing, and described
the initial APK test as apparently working with visible color changes. This confirms only the
reported basic interaction; it adds no frame-time/comfort evidence. Android 15 was reported
earlier; exact OS/build on the nominated phone and exact Quest firmware still require capture
for the measured run. No new tests, builds or implementation changes were performed here.

Official manufacturer references checked on 2026-10-25:

- [Samsung Galaxy S24 Ultra specifications](https://www.samsung.com/ca/business/smartphones/galaxy-s/galaxy-s24-ultra-titanium-black-1tb-sm-s928wzknxac/):
  6.8-inch display, 3120 x 1440 panel resolution, maximum display refresh 120 Hz.
  This reference is a specific regional/storage variant, not confirmation of the student's
  unit's storage, current display resolution, refresh setting or achieved app fps.
- [Meta display refresh documentation](https://developers.meta.com/vr/documentation/unity/unity-set-disp-freq/):
  Quest 2 supports a 72 Hz application target; documented default app refresh is 72 Hz.
  Actual runtime refresh and sustained delivery must still be observed. At 72 Hz the nominal
  frame budget is about 13.9 ms, not a measured result for this project.

Existing 60 fps Android / 72 Hz Quest candidates are compatible with those documented display
capabilities, but online hardware specifications do not demonstrate this app's performance.
A proposed closure run is five minutes warm-up plus five minutes observation per device,
recording measured frame timing, input/tracking and Quest comfort. Targets/duration remain
proposed until confirmed for the run. T020/T021/T024/T025 checkboxes are unchanged.

### Final checkpoint documentation/settings clarification — 2026-10-25

Generalized the illustrative MCP project path in the Spec Kit quickstart. README and the
current setup guide contain no literal user checkout path. Absolute paths in earlier
validation entries are retained as historical executable/project/artifact evidence; they
are not paths another contributor should copy.

Read-only settings review: UnityConnectSettings top-level m_Enabled=1; Analytics, Purchasing,
Ads, performance reporting and cloud diagnostics reporting are disabled; engine diagnostics
is enabled. No com.unity.services.* package appears in the resolved lock and no service
initialization/API use was found in Assets/Foundation. The current smoke/input/XR foundation
and local APK builds have no demonstrated dependency on activating Unity Services. This
does not prove absence of every Editor/tooling network request. No flags were changed.

Preserving the current Quest-oriented Quality/URP configuration is reasonable for this
checkpoint: it is the configuration used for successful APKs and user-reported functional
tests. It is not established as performance-optimal or fully validated. Former PC quality
replacement, platform-default changes and URP/volume serialization/prefilter differences
remain documented; Windows player rendering, quality and performance are untested. Pipeline
Auto Start=false and all imported XRI samples/.meta files are retained unchanged.

Student confirmed reading/validating AI usage and foundation records. T023 AI/foundation
review is now recorded as confirmed, while separate asset attribution/license review remains
unconfirmed. T020/T021 retain pending hardware conditions/performance/comfort requirements;
T022 remains complete, T024 reproduction unexecuted and T025 final acceptance incomplete.
No unchecked acceptance criterion is promoted to passed by this documentation change.
No implementation, scene, package or project-setting changes, builds, staging, commit or push.

### Repository hygiene/privacy audit — 2026-10-25

Inspected all 739 tracked paths, file sizes/grouping, current ignore rules and relevant
documentation/settings. No tracked cache/build/log/private-key/credential-named file or file
over 5 MiB was identified. Unity source assets, metadata, packages/settings, AGENTS, Spec Kit
documents and AI/source records remain tracked. No files were deleted or untracked.

Added repository rules for Gitleaks JSON/SARIF/CSV output, local dotenv files, credential-store
locations and common private-key/signing formats. Shared scanner configuration remains eligible
for tracking. Example dotenv files are eligible only for placeholder content, never real secrets.
Ignore rules do not sanitize files already tracked or secrets embedded in ordinary source/docs.

Personal home/checkout prefixes in this record were normalized to <USER_HOME>/<REPO_ROOT>;
executable versions, relative artifact paths, hashes and outcomes were preserved. Current
public setup instructions already use configurable examples. No Git history was rewritten:
older commits still contain earlier documentation. Changing repository visibility does not
erase previously published copies. A username/path alone is not an API credential.

User reports Gitleaks scanned all four commits with one Atlassian-token finding. This audit
does not claim a new full-history Gitleaks run. The earlier verified false positive is an
example jiraProjectConfigId in the official Unity CLI skill (line 496, foundation commit
7ddb8edf92a5c6e05db97c212907f085fe6a194c); whole-document upstream comparison matched ignoring
line endings. No detected value is reproduced. Source/licensing and narrowly scoped exception
guidance are in third-party-assets.md; no scanner suppression was added.

Unity CLI guidance is 15 files/about 0.56 MiB. Recommendation: retain the documented snapshot
for reproducibility, with official upstream attribution/license reference. Installation-only
is an optional future maintenance decision, requiring approval before untracking. No currently
tracked file requires automatic untracking; optional XRI/demo cleanup remains a separate
dependency-checked Editor task. Local reports/credentials/cache/build outputs should remain
excluded. All unfinished acceptance checks retain their previous status.
