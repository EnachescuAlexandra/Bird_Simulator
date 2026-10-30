# Foundation continuation

Updated: 2026-10-25. Perform only the requested follow-up; no automatic builds or commits.

## Verified state

- Unity 6000.3.25f1; CLI 1.0.0-beta.14; Pipeline 0.9.1-exp.1; OpenXR 1.16.1;
  XRI 3.3.2; Input System 1.20.0; URP 17.3.0. T001-T019 complete.
- Shared FoundationSmoke scene and exclusive mode/input/camera behavior verified. Quest is
  primary. Quest player starts VR; touch player and Editor default non-VR.
- Both APKs succeeded, with separate IDs and inspected manifests. Outputs under
  BirdLifeSimulator/Builds/{QuestStandalone,AndroidTouch}/ are ignored. Reports/hashes are
  in foundation-validation.md. Do not repeat builds without a change or explicit need.
- User reports Quest 2 controllers visible/tracked and trigger toggling the cube;
  AndroidTouch works on an Android 15 device. Exact Quest OS build is unrecorded; the original Android report did not identify its model.
- Detailed follow-up testing is now planned on Samsung Galaxy S24 Ultra and Meta Quest 2.
  The earlier generic Android 15 functional report is retained; a new measured S24 Ultra run
  has not been reported. Manufacturer specifications are linked in validation, not treated
  as actual app frame-rate measurements.
- No performance/comfort measurements or fresh README-based reproduction have passed.

## Remaining work and expected evidence

| Task | State | Next concrete evidence needed |
| --- | --- | --- |
| T020 | Partial | Exact device identities/OS, actual refresh setting, agreed frame-time target and observation duration. Android 60 fps / Quest 72 Hz and five-minute warm-up remain provisional |
| T021 | Partial | Keep the reported functional passes; separately measure sustained frame timing/thermal behavior and record Quest comfort observations. Stop if discomfort occurs |
| T022 | Complete | README now describes implemented setup and limitations |
| T023 | AI/foundation review confirmed by student; attribution review pending | Confirm/correct third-party-assets.md source/license record; no unreported review is assumed |
| T024 | Not executed | Follow README for one headset-free test and both profile selections; report actual results, not earlier tests performed before this README existed |
| T025 | File audit complete; final acceptance pending | Review flagged settings and remaining acceptance gaps below; dependencies T020/T021/T024 not fully passed |

Practical closure checklist (proposed test protocol, not an executed result):

1. T020: record exact S24 Ultra OS/build, Quest OS/build and actual refresh setting while
   the app runs. Confirm the targets and observation duration; current candidates remain
   Android 60 fps / Quest 72 Hz, five-minute warm-up followed by five-minute observation.
2. T021: on each device record launch, input/color toggle, sustained measured fps/frame
   timing and any stutter or thermal deterioration. On Quest also record controller/head
   tracking and comfort over the stated duration; stop if discomfort occurs. A color toggle
   alone passes the basic interaction check, not these other criteria.
3. T023: AI/foundation records have now been read and validated by the student. The
   separate third-party source/license review remains unconfirmed; record its actual result.
4. T024: reproduce one Editor smoke check and both profile selections from the README's
   linked guide. T025: review the flagged Git settings and remaining limitations afterward.

Do not mark a measured requirement passed from a functional report. Hardware access is
confirmed, but exact metadata gaps do not invalidate the reported basic functionality.

## Commands and recovery

### Installed tools and package versions

| Component | Verified version |
| --- | --- |
| Unity / Android Build Support | 6000.3.25f1 |
| CLI / Pipeline | 1.0.0-beta.14 / 0.9.1-exp.1 |
| Input System / URP | 1.20.0 / 17.3.0 |
| OpenXR / XRI | 1.16.1 / 3.3.2 |
| Bundled OpenJDK / NDK | 17.0.18+8 / 27.2.12479018 |
| SDK Build Tools / target SDK | 36.0.0 / 36 |

Use this Editor's installed Android module/bundled tools and the project's manifest/lock.
No reinstall or upgrade is required to reproduce recorded checks.

### Reproduce the Editor smoke test

1. Open `BirdLifeSimulator/` and `Assets/Scenes/FoundationSmoke.unity`.
2. Play defaults to AndroidTouch/non-VR, even with the Quest profile selected. Focus
   Game view. Space or the on-screen button toggles SmokeTarget cyan/yellow once per
   activation. Stop/Play resets cyan.
3. In Play, select the runtime `FoundationMode` object and change Selected Mode to
   QuestStandalone. VRRig replaces non-VR camera/input. For the legacy simulator, hold
   right mouse and move the mouse for head pose; hold Space for the right controller
   and click left mouse for trigger. Left Ctrl overrides rotation; WASD/QE position the
   selected simulated device. These are simulator bindings, not flight controls.
4. Return to AndroidTouch: only selected input should respond, with one active Camera
   and AudioListener in either mode. Stop Play. Report observations for T024; the new
   guide itself has not yet received a fresh human reproduction pass.

Both adapters use `Assets/Foundation/Runtime/SmokeTarget.cs`. The existing rig is
`Assets/Foundation/Prefabs/VRRig.prefab`; its simulator is Editor-only. All imported
Starter Assets/simulator samples and their metadata are preserved; the modern simulator
is not the instantiated legacy simulator.

### Optional Codex MCP setup

Official local guidance is `.agents/skills/unity-cli/`, installed using
`unity skill install codex --local`. The full optional Unity plugin was not installed.
Merge only a minimal project-bound entry into the user's Codex configuration; preserve
other entries and permission policies. This is a configurable Windows example, not a
literal path to copy:

```toml
[mcp_servers.unity]
command = 'C:\path\to\unity.exe'
args = ['mcp', '--project-path', 'C:\path\to\Bird_Simulator\BirdLifeSimulator']
```

Reload the VS Code window after configuration changes. T006/T007 recorded project identity,
hierarchy read and a unique temporary-object create/read/delete/absence check; see validation
for the original dirty-state caveat and later user-authorized save. Do not repeat edits merely
to reconfirm completed setup. Historical paths in validation use privacy-normalized placeholders; new checkouts must substitute their own project and executable locations.

### Profile selection and build outputs

| Profile | Player startup / XR policy | Application ID | Output relative to BirdLifeSimulator/ |
| --- | --- | --- | --- |
| AndroidTouch | non-VR; no XR loader/startup/features | com.birdlife.simulator.touch | Builds/AndroidTouch/AndroidTouch.apk |
| QuestStandalone | VR; OpenXR, Meta Quest Support and Oculus Touch | com.birdlife.simulator.quest | Builds/QuestStandalone/QuestStandalone.apk |

Native profiles in `Assets/Settings/Build Profiles/` both include FoundationSmoke and
ARM64/IL2CPP, target SDK 36, minimum SDK 25 touch / 29 Quest. When a build is requested,
use the selected profile and its exact output path. Builds are Git-ignored. T019 already
produced both APKs; sizes, errors/warnings, SHA256 and manifest inspection are retained in
[validation](foundation-validation.md#t019-build-execution--2026-10-24) and its following
completion section. No Windows player build was executed. No build is authorized by
this README/documentation refactor.

Quest SSAO device overhead, optional OpenXR control/latency/LTO recommendations and
diagnostics debug-symbol warnings remain recorded, not silently fixed. Frame timing,
refresh settings, timed warm-up/observation, thermal behavior and comfort remain pending;
proposed 60 fps Android / 72 Hz Quest and five-minute warm-up are not measurements.
The digital-panel OS, launcher and integration mechanism remain open assumptions, with no
kiosk, embedded-app or inter-app implementation.

Open Assets/Scenes/FoundationSmoke.unity. Stop Play before selecting
BirdLife > Select AndroidTouch or Select QuestStandalone; wait for import/compilation.
If synchronization was skipped while busy, select the same menu item in stable Edit Mode.
Touch must have no XR loader/startup/features; Quest requires OpenXR with Meta Quest Support
and Oculus Touch Controller Profile. The prebuild validator rejects mismatches.

Pipeline Auto Start remains false. After reload/disconnection, in stable Edit Mode use
Window > Pipeline > Start Server. Existing minimal Codex MCP entry targets this project's
BirdLifeSimulator directory; do not overwrite other entries or policies. Long build MCP calls
previously timed out while builds continued: inspect *.build-result.txt/APK/progress before retry.
Never start a second Editor on a locked project or clear caches as an automatic workaround.

The isolation file ProjectSettings/Packages/com.unity.pipeline/EditorPipelineConfig.json
originally did not exist. Exact restoration, only if explicitly requested: close Unity and
remove just that file if it still contains only the test setting; preserve later edits first.
No restoration or deletion has been performed.

## Git handoff

Include authored Foundation/scene/profile/XR assets and all required .meta files, package
manifest/lock, relevant project settings, documentation, AGENTS and Spec Kit/Unity guidance.
Keep all imported samples eligible for Git; modern simulator/demo pruning needs a separate
Editor dependency review. .utmp, Library, Temp, Logs, Obj, UserSettings, builds and IDE files
are ignored. The temporary constitution sync-review comment has already been removed.

Checkpoint settings review (2026-10-25): UnityConnectSettings has its top-level m_Enabled=1,
but Analytics, Purchasing, Ads and cloud diagnostics reporting are disabled in that file.
Engine diagnostics remains enabled. No com.unity.services.* package or Foundation-script
service initialization was found; the smoke behavior, input, XR and local builds do not
require Unity Services activation. The top-level flag alone does not establish active
analytics collection or prove that all tooling is offline. No setting was disabled.

The current Quest-oriented Quality/URP settings can be preserved as the exact checkpoint
configuration that produced the two successful APKs and reported basic device behavior.
This is not a performance/comfort certification. QualitySettings replaced the former PC
entry with Meta Quest and changed platform defaults; URP/volume serialization/prefilter
changes are retained. Windows player quality, rendering and performance are untested and
need explicit platform/quality review before a future Windows build. Do not revert these
files independently of profiles merely to reduce the diff. Samples and their metadata remain.

Pipeline Auto Start=false is intentionally retained; manual server start after reload remains
documented. Public setup examples use configurable paths; privacy-normalized tested paths in the
validation history retain provenance without the local account name. Nothing has been staged, committed or pushed.

## Weekly FlightPrototype handoff — 2026-10-30

Open Assets/Scenes/FlightPrototype.unity. StaticObservation/FoundationMode selects Android Touch or Quest Standalone. Enable KeyboardFlightInput for Editor keyboard debugging; touch ownership/UI then becomes inactive. The [feature-002 quickstart](../specs/002-flight-system/quickstart.md) records simulator bindings and existing profile/output selection. The [dashboard](dashboard.html) summarizes delivered work and follow-up validation. Student reports both APKs launch and core controls work; edge/comfort/multi-touch checks remain T021. Restart viewpoint offsets and lag are deferred under T020. No setup or XR policy was changed in this closure.