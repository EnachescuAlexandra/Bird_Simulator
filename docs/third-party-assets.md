# Third-party sources and licenses

Updated 2026-10-25. No external bird model or forest environment has been introduced.

| Material | Author / source | Version and license evidence | Use |
| --- | --- | --- | --- |
| XRI Starter Assets | Unity Technologies, XR Interaction Toolkit package samples | 3.3.2; installed package `LICENSE.md`: Unity Companion License for Unity-dependent projects | Source XR Origin prefab, input actions and referenced visuals/UI |
| Legacy XR Device Simulator | Same package/author | 3.3.2, same package license | Instantiated Editor-only simulator |
| XR Interaction Simulator | Same package/author | 3.3.2, same package license | Imported during setup, retained; not the active legacy simulator |
| URP/template materials and scene primitives | Unity Technologies / existing project template | URP 17.3.0 and template package notices; preserve original notices/metadata | Existing rendering and minimal geometry |

Sources: [XRI documentation](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.3/manual/index.html),
[Unity Companion License](https://unity.com/legal/licenses/unity-companion-license).
The installed XRI package `LICENSE.md` identifies Unity Technologies copyright 2026 and
the Companion License; preserve that notice and any package-specific third-party notices.
Package dependencies/versions remain recorded in manifest and lock files; this table does
not relicense those dependencies or replace their licenses.

Keep sample files with their `.meta` GUIDs and original attribution. Do not ignore or manually
remove sample subtrees to reduce the Git count: references can cross sample folders. The
modern simulator and demo art can be assessed for removal in a separately authorized Editor
cleanup, with dependency checks. No such removal occurred here. Human attribution review
remains pending; no legal clearance or exhaustive license review is asserted.

## Official Unity CLI guidance

The tracked `.agents/skills/unity-cli/` directory contains 15 documentation files, about
0.56 MiB, installed from Unity CLI 1.0.0-beta.14 (build provenance in validation). Retaining
this snapshot makes the guidance used during foundation work reproducible; it is not a
runtime package, credential store or build requirement. Do not install another copy merely
to resolve a scanner finding.

Upstream: [Unity-Technologies/skills](https://github.com/Unity-Technologies/skills).
Upstream attribution: **Unity Skills © 2026 Unity Technologies**, licensed under the
[Unity Companion License for Unity-dependent projects](https://github.com/Unity-Technologies/skills/blob/main/LICENSE.md).
Preserve this attribution and upstream license reference; the project does not relicense
the guidance. Human source/license review remains distinct from AI-record review.

Recommendation: keep the current snapshot for this checkpoint. A separately approved future
switch to installation-only should record the exact CLI/skill source version, preserve these
provenance notes and verify installation on a clean checkout before untracking it. No skill
files were removed, ignored or untracked by this audit.

Confirmed scanner false positive: `references/collaboration.md`, line 496 at foundation
commit `7ddb8edf92a5c6e05db97c212907f085fe6a194c`, is `jiraProjectConfigId` in a JSON example,
not an authentication field. Prior read-only comparison matched the complete official
[upstream file](https://github.com/Unity-Technologies/skills/blob/main/skills/unity-cli/references/collaboration.md),
ignoring line endings. The detected value is intentionally omitted here. Do not suppress the
entire skill directory or Atlassian rule; any future scanner exception must be specific to
this documented finding. No exception/configuration change was made during this audit.

## Feature 002 procedural placeholders — 2026-10-30

Woodland graybox and MockBlackbird are project-authored Unity primitive/ProBuilder 6.1.4 geometry and plain project materials, authored with Codex/Editor assistance. No external bird, vegetation, texture, sound or font was imported for this milestone. Existing Unity/XRI/Input System/ProBuilder package and sample provenance continues to apply. The dashboard uses system fonts and inline CSS/JavaScript with no external assets/dependencies. This provenance note does not assert independent license clearance.