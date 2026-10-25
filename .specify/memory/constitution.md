# Bird_Simulator Constitution

## Core Principles

### I. Manageable Initial Scope

The initial prototype MUST remain feasible for a solo undergraduate project: one blackbird,
one forest level, flight, collecting three food portions, and returning to a refuge.
Future milestones MAY extend this scope through new specifications; this boundary is not a
permanent product limit. Features outside the current milestone MUST NOT be implemented early.

### II. Shared Gameplay, Separate Platform Configuration

Gameplay logic MUST be shared across Android touch and Meta Quest standalone. Device input
and camera/XR configuration MUST be isolated from gameplay rules. Keyboard/mouse testing MUST
remain supported, and platform assumptions MUST NOT prevent a future Windows build.

### III. Editor VR Testing and Device Validation

VR mode MUST support testing in the Unity Editor with an XR simulator. Simulator checks MUST
NOT be treated as device validation. Touch interaction, tracking, performance, and VR comfort
MUST be tested on the relevant target devices before the affected milestone is accepted.

### IV. Small, Verifiable Increments

Each increment MUST define a clear outcome and explicit acceptance criteria before implementation.
Reports MUST separate completed checks and their results from pending manual checks.
Automated tests MUST be used where they meaningfully verify behavior; tests that merely mirror
implementation or add no useful assurance are not required.

### V. Unity Asset and Dependency Integrity

Unity asset GUIDs, `.meta` files, and serialized references MUST be preserved. Before changes,
inspect the Unity version, package manifest, and package lock file. Dependency changes MUST be
necessary for the requested stage and favor stable versions compatible with the installed stack.
Prefer available Editor/MCP tools for scene and prefab changes to preserve Unity references.

### VI. Accurate Documentation and Attribution

Documentation MUST describe implemented behavior and clearly label planned work. Material AI
assistance MUST be recorded with its contribution and human verification. Third-party assets
MUST have their sources, applicable licenses, and required attribution recorded before inclusion.

### VII. Simple Architecture and Early Experiments

Favor simple architecture and a working prototype. Uncertain flight and VR comfort mechanics
MUST be prototyped and evaluated before their design is adopted. Keep platform boundaries
extensible, but introduce abstractions only for demonstrated current needs; future features
MUST NOT be implemented merely to prepare for possible later milestones.

## Project Boundaries

The repository is `Bird_Simulator`; the Unity project is in `BirdLifeSimulator/`.
`AGENTS.md` provides operational guidance and requires user-facing communication in Romanian.
This constitution is in English. New specifications MAY define later milestones without
changing the historical scope of the initial prototype. Changes to the governing principles
require the amendment procedure below.

## Development Workflow

Perform only the stage requested by the user. Each specification, plan, and task review MUST
check these principles, identify acceptance criteria, and identify required manual/device checks.
Before modifying the project, inspect `ProjectSettings/ProjectVersion.txt`,
`Packages/manifest.json`, and `Packages/packages-lock.json` under `BirdLifeSimulator/`.
Do not add unrelated features, packages, or upgrades. At completion, briefly report changed files,
executed verification, and pending checks; do not claim unperformed testing has passed.
Commit or push only when the user explicitly requests it.

## Governance

This constitution governs project specifications, plans, and implementation alongside the
operational rules in `AGENTS.md`. Review every increment for compliance; document and resolve
conflicts before proceeding with affected work. Resolve routine implementation choices within the current authorized scope. Ask the user when a decision changes the agreed scope, governing principles, or introduces a material unresolved trade-off.

Amendments MUST record the rationale, affected principles, and impact on existing specifications
and plans, receive user approval, and update the version and amendment date. Use semantic
versioning: MAJOR for incompatible principle removals or redefinitions, MINOR for added or
materially expanded principles or sections, and PATCH for clarifications without semantic changes.
The original ratification date MUST remain unchanged in subsequent amendments.

**Version**: 1.0.0 | **Ratified**: 2026-10-24 | **Last Amended**: 2026-10-24
