# Working Guidelines for Bird_Simulator

- The repository is `Bird_Simulator`; the Unity project is located in `BirdLifeSimulator/`.
- Before making changes, check the Unity version in `BirdLifeSimulator/ProjectSettings/ProjectVersion.txt` and the packages in `BirdLifeSimulator/Packages/manifest.json` and `BirdLifeSimulator/Packages/packages-lock.json`.
- Work step by step: perform only the requested stage and report its verification results.
- Prototype scope: one blackbird, one forest level, flight, collecting three portions of food, and returning to the refuge.
- Target platforms are Android with touch controls and Meta Quest standalone. Keep gameplay logic shared and controls separate for each platform. Support keyboard/mouse testing and preserve the option of a future Windows build.
- Each task must have a clear outcome and verification criteria. Explicitly distinguish checks that were performed from checks that require manual testing.
- Preserve `.meta` files and Unity references. Prefer Editor/MCP tools for scene and prefab changes when available.
- Do not add features, packages, or updates that are unnecessary for the requested stage.
- Commit or push only when the user explicitly requests it.
- Communicate with the user in Romanian and briefly explain the changes.
