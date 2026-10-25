# Feature Specification: Technical Foundation

**Feature Branch**: Not created; current branch is `main`.

**Created**: 2026-10-24

**Status**: Draft; requirements reviewed, implementation not started.

**Input**: Establish the BirdLife Simulator technical foundation: verified Unity MCP access,
separate touch and standalone VR modes, headset-free testing, minimal smoke scene, separate
build configurations, and accurate setup and AI-assistance documentation.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Inspect and Edit the Open Project (Priority: P1)

As the solo developer, I want Codex to inspect and make a reversible edit in the correct open
Unity project, so subsequent work can use verified Editor access.

**Why this priority**: Reliable project access prevents work against the wrong Editor instance.

**Independent Test**: Connect to the open project, read its hierarchy, and complete one reversible
object edit without implementing runtime functionality.

**Acceptance Scenarios**:

1. **Given** the open `BirdLifeSimulator/` project, **When** Codex reads its hierarchy through MCP,
   **Then** project identity, active scene, and object names match what the developer sees.
2. **Given** that verified connection, **When** Codex creates one uniquely named temporary object,
   reads it back, removes it, and rereads the hierarchy, **Then** its presence and subsequent
   absence are evidenced and pre-existing objects and references remain unchanged.
3. **Given** no connection or an ambiguous project identity, **When** verification is attempted,
   **Then** the result is blocked with a clear reason, rather than reported as passed.

### User Story 2 - Exercise Both Modes Without a Headset (Priority: P1)

As the developer, I want a minimal shared scene that I can inspect and interact with in non-VR
and simulated VR modes without owning or connecting a headset.

**Why this priority**: Daily verification must be possible independently of device availability.

**Independent Test**: Use the smoke scene in each Editor mode and observe rendering, camera
behavior, and one visible input response. This test does not depend on MCP availability.

**Acceptance Scenarios**:

1. **Given** non-VR mode and no headset, **When** Play mode starts, **Then** simple geometry is
   visible from the intended camera and keyboard/mouse can trigger a visible test response.
2. **Given** VR mode and an XR simulator, **When** simulated head pose and controller input change,
   **Then** the view responds to pose and the same test action produces the same visible response.
3. **Given** either mode, **When** the developer switches to the other mode and back, **Then**
   only the intended input/camera setup is active, without duplicated listeners or cameras.

### User Story 3 - Select, Build, and Test Each Target (Priority: P2)

As the developer, I want distinct Android touch and Quest standalone configurations so that
selecting a target reliably selects the matching input and camera behavior.

**Why this priority**: Both targets use Android, but must not accidentally share incompatible
runtime settings.

**Independent Test**: Follow the documented selection procedure and build each target; assess
device execution separately when the relevant device is available.

**Acceptance Scenarios**:

1. **Given** the documented setup, **When** Android touch is selected and built, **Then** a distinct
   artifact is produced with the smoke scene and non-VR configuration.
2. **Given** the same project, **When** Quest standalone is selected and built, **Then** a distinct
   artifact is produced with the smoke scene and VR configuration, without modifying gameplay rules.
3. **Given** an available touch device, **When** its artifact is launched, **Then** geometry renders,
   the non-VR camera works, and physical touch produces the shared visible input response.
4. **Given** an available Quest, **When** its artifact runs standalone, **Then** geometry renders,
   head tracking controls the view, controller input produces the shared response, and initial
   performance/comfort observations are recorded.
5. **Given** unavailable hardware, **When** results are reported, **Then** the relevant device
   tests remain pending with the reason, even if Editor checks and builds passed.

### User Story 4 - Reproduce and Audit the Foundation (Priority: P2)

As the developer or assessor, I want reproducible instructions and an honest work record so
I can repeat checks and understand material AI and third-party contributions.

**Why this priority**: A foundation is useful only if its state and evidence are reproducible.

**Independent Test**: Review the completed-work documentation against recorded evidence and use
it to repeat one Editor check and select both build targets.

**Acceptance Scenarios**:

1. **Given** the completed milestone, **When** a reader follows the README, **Then** prerequisites,
   mode selection, MCP verification, simulator use, build selection, and device tests are clear.
2. **Given** the AI usage record, **When** compared with actual changes and checks, **Then** material
   assistance, affected artifacts, human review, and unverified work are accurately distinguished.

### Edge Cases

- Multiple Editors or a disconnected MCP connection: verify project identity before mutation;
  interrupted cleanup must be reported, with a procedure to remove only the temporary object.
- Missing simulator or build prerequisites: report the affected check as blocked, preserving
  unrelated verification; do not silently substitute a different test mode.
- Switching between two Android-based targets must not leave stale XR, camera, or input state.
- Missing headset must not prevent keyboard/mouse or simulator testing.
- An artifact that builds but fails to launch is a successful build and a failed device check.
- Input events must cause one response per action, including after repeated mode switches.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Verify Codex read/write access through MCP using the project identity, hierarchy
  read, and temporary-object round trip in Story 1. Capture evidence and confirm cleanup.
- **FR-002**: Provide separately selectable Android touch and Quest standalone configurations,
  including isolated input and camera/XR setup. Prove switching through Story 2 and Story 3.
- **FR-003**: Keep common scene behavior and test-action semantics shared; target selection MUST
  NOT require editing or duplicating gameplay rules. No gameplay mechanics are required here.
- **FR-004**: Support non-VR keyboard/mouse Editor testing without a headset and VR Editor testing
  with simulated head pose and controller input, with the visible responses in Story 2.
- **FR-005**: Provide one minimal smoke-test scene with a ground surface, simple distinguishable
  geometry, lighting, and a visible input-response target; each supported mode must render it.
  A test action such as changing a target's appearance is sufficient; locomotion is excluded.
- **FR-006**: Provide two clearly named build configurations and distinct output locations, with
  a documented selection procedure identifying target, mode, included scene, and expected artifact.
- **FR-007**: Produce and record a successful build for each target independently of Editor and
  device results. Record failures or missing prerequisites instead of implying build success.
- **FR-008**: Maintain a verification matrix separating Editor checks, build results, and actual
  device tests. Each entry records status (passed, failed, blocked, or pending), date, mode,
  environment/device identity, evidence, and any reason it was not executed.
- **FR-009**: Require device evidence for touch, tracking, target performance, and initial VR
  comfort observations; simulator results cannot satisfy these checks. Unavailable devices
  remain pending, and the milestone cannot be called fully device-validated.
- **FR-010**: Preserve a future Windows build path through headset-free operation and separable
  device configuration. Delivering or testing a Windows player build is outside this milestone.
- **FR-011**: Update the README with only completed setup and truthful verification results,
  separately labeling pending work, known limitations, and reproducible procedures from Story 4.
- **FR-012**: Maintain an AI usage record describing material assistance, affected artifacts,
  review and verification, and limitations. Record third-party sources/licenses if introduced.
- **FR-013**: Preserve existing GUIDs, `.meta` files, and references. Introduce dependencies only
  when needed, after inspecting installed versions and documenting compatibility rationale.
- **FR-014**: Exclude flight, food collection, refuge logic, final bird models, forest environment,
  unnecessary frameworks/services, and speculative future features. Build only this foundation.

### Key Entities *(include if feature involves data)*

- **Mode configuration**: Named touch or standalone VR setup, its input/camera selection and build identity.
- **Smoke-test scene**: Shared geometry and observable test interaction used across modes.
- **Verification record**: A check, environment, outcome, evidence, and unresolved limitation.
- **AI usage entry**: Material contribution, affected work, human review, and verification status.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: One complete inspect/create/read/remove/read sequence demonstrates reversible access;
  zero temporary objects or unintended scene changes remain (FR-001).
- **SC-002**: Both headset-free test modes display the scene and produce the same visible action
  result; switching touch-to-VR-to-touch produces zero duplicate responses (FR-002 to FR-005).
- **SC-003**: Two independently identified build artifacts are produced successfully using their
  documented selections; neither result is used as proof of device execution (FR-006 to FR-008).
- **SC-004**: Every required check has an explicit outcome and evidence or a non-execution reason;
  zero unavailable-device checks are marked passed (FR-008, FR-009).
- **SC-005**: Following the README, a reviewer can repeat an Editor smoke check and select both
  targets without undocumented source edits; AI records distinguish completed and unverified work
  (FR-010 to FR-012).
- **SC-006**: Review finds zero missing existing asset metadata, unintended reference changes,
  or out-of-scope features introduced by this milestone (FR-013, FR-014).

## Assumptions

### Inspected Baseline (2026-10-24)

- Unity version: `6000.3.25f1`, from `ProjectSettings/ProjectVersion.txt`.
- Manifest and lock file contain Input System `1.20.0` and URP `17.3.0`.
  The built-in XR module is present; this does not establish an operational XR setup.
- `EditorBuildSettings.asset` enables `Assets/Scenes/SampleScene.unity` and references an input
  action asset. The serialized scene names include Main Camera, Directional Light, and Global Volume.
- No separate touch/Quest build configuration, XR simulator, or Unity MCP integration was found
  in inspected project files; no Unity MCP tool is exposed to this session. This is a static
  inspection, not proof about the currently open Editor or external tools installed elsewhere.
- The README currently contains only the repository title. No runtime, build, MCP, or device
  acceptance tests have been executed during specification work.

### Dependencies and Open Planning Decisions

- Planning must choose and verify a Unity MCP integration, its compatibility and connection
  procedure; no plugin or version is selected by this specification.
- Planning must choose a compatible XR runtime/provider, simulator and input/camera approach
  based on the installed Unity stack. No new package version is assumed.
- Identify available Android/Quest hardware, build toolchains and Editor modules, supported
  device/OS baselines, and test operators before scheduling builds and device tests.
- Define device-specific performance targets and observation duration before performance testing;
  a recorded initial comfort observation is not comprehensive comfort validation.
- Choose the simplest reproducible build-configuration mechanism and minimal test-action mapping
  during planning. These decisions do not change the required outcomes.
- A developer performs the smoke checks. Distribution, store publication, production signing,
  a Windows artifact, and full gameplay remain outside this milestone.
- Later specifications may extend the initial prototype scope under the constitution; this
  milestone must not implement those future features in advance.
