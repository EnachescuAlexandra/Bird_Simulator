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
