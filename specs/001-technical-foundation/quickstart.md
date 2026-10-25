# Quickstart Validation Guide

This is a future execution guide, not evidence of completed setup. Request one implementation
stage at a time. See [plan](plan.md), [research](research.md) and [contracts](contracts/foundation.md).

## 1. Establish MCP first

User: confirm Windows 10 21H1+ and a valid Unity license; open the existing project using
Unity 6000.3.25f1. Confirm Codex VS Code extension sign-in. Install the selected CLI only during
the authorized setup stage, following research.md. Interactive sign-in and modal prompts are manual.

Future PowerShell sequence, from `BirdLifeSimulator/` (none executed during planning):

```powershell
unity --version
unity auth login
unity pipeline list-versions
# Inspect package compatibility and select an exact published version before installation.
unity pipeline install --package-version <selected-version> --project-path .
unity pipeline list
unity mcp configure codex --dry-run
```

The selected Pipeline version is a recorded prerequisite, not a literal placeholder to paste.
Refocus Unity and wait for compilation. Inspect installed command help before relying on flags.
Review the dry-run delta; prefer the minimal entry below if the helper changes sandbox policy.
Use the absolute CLI path resolved on this machine, and merge only this entry into Codex's
configuration (do not replace the file or expose other servers' credentials):

```toml
[mcp_servers.unity]
command = 'C:\absolute\path\to\unity.exe'
args = ['mcp', '--project-path', 'C:\path\to\Bird_Simulator\BirdLifeSimulator']
```

Both executable and project paths above are illustrative; replace them with absolute paths
for the current checkout. Historical tested paths remain in docs/foundation-validation.md.
The user applies host settings and reloads the
extension. No `vscode` Copilot configuration is required for Codex. After reload, Codex discovers
the MCP tools and performs the hierarchy/object round trip in contracts/foundation.md through
MCP. Compare baseline and final scene/dirty state; pass only after cleanup. Missing tools or
wrong project is blocked. Do not proceed to scene work until this gate passes.

## 2. Non-VR smoke check

After stage 2 exists, open FoundationSmoke, select non-VR mode and enter Play without a headset.
Confirm ground/primitives/target render, one camera/listener is active, and Space or mouse click
changes target color once. Stop/restart Play and repeat. A laptop touch check is supplementary.

## 3. Simulated VR check

After authorized package resolution, confirm exact versions and no compile/validation errors.
Select VR Editor mode, enable the imported XRI simulator and focus Game view. Exercise simulated
head pose and controller select using the sample's displayed bindings; record actual bindings in
README after setup. Confirm view movement and one shared target response. Repeat non-VR -> VR ->
non-VR; inspect inactive rigs/listeners and duplicate responses. No headset is required for this check.

## 4. Build checks

User confirms Android Build Support with matching SDK/NDK/OpenJDK for this Editor. In File >
Build Profiles activate AndroidTouch, run mode/XR preflight, then build an APK into
`Builds/AndroidTouch/` under the project. Repeat with QuestStandalone into `Builds/QuestStandalone/`.
Use the documented selector if a shared-settings guard is needed. Capture build reports,
application IDs, included scene and effective XR settings/manifests. Repeat selection after
Editor restart. Confirm touch is non-VR and Quest contains intended XR support; simulator absent.
Successful builds do not prove either APK launches. Do not start a second batch Editor build
against a project already locked by the open Editor.

## 5. Physical devices

Availability is unconfirmed. User enables developer/USB debugging, approves trust and supplies
physical interactions. On Android: launch, render, camera and real touch response. On Quest:
standalone launch, tracked camera, controller response, frame timing and initial comfort notes.
Confirm hardware-specific budgets before measuring; proposed baseline is Android 60 fps and
Quest 72 Hz, five minutes after warm-up. Record actual refresh settings, temperatures/conditions,
frame timings and observation duration; do not infer sustained performance from a screenshot.
Stop on discomfort and record limitations. This stationary smoke scene does not validate future
flight comfort. Unavailable hardware remains pending, never passed by laptop or simulator tests.

## 6. Evidence and documentation

Initial acceptance matrix (all unexecuted):

| Check | Category | Initial status |
| --- | --- | --- |
| MCP hierarchy and reversible edit | Editor/MCP | Pending setup |
| Keyboard/mouse rendering and action | Editor | Pending implementation |
| Simulated VR pose and controller action | Editor | Pending implementation |
| Mode/profile switching and restart | Editor | Pending implementation |
| AndroidTouch APK | Build | Pending toolchain confirmation |
| QuestStandalone APK | Build | Pending toolchain confirmation |
| Real Android touch/rendering/performance | Device | Pending; hardware unconfirmed |
| Quest tracking/input/performance/comfort | Device | Pending; hardware unconfirmed |

During implementation record outcomes using data-model.md, update README only for completed work,
and add AI contribution/source-license records. Human visual checks remain explicit even when
Codex captures console or test output. Useful automated checks cover one-action/one-response,
mode exclusivity and invalid build-state rejection; do not write tests merely mirroring settings.
