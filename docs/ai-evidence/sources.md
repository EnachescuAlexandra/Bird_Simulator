# AI evidence: sources and limits

Reviewed for the 2026-10-30 documentation audit. Five original Codex JSONL session logs were found in the student's private Codex session storage; their session metadata identifies this project. They are not committed or linked by private absolute path. Public aliases below omit session UUIDs; the exact filename mapping is retained privately in `local-notes/log-sources.json`.

| Alias | Original filename prefix (session identifier omitted) | Material inspected |
| --- | --- | --- |
| S1 | rollout-2026-10-24T13-48-42 | Session metadata and message inventory; foundation/repository workflow context |
| S2 | rollout-2026-10-24T15-13-01 | Session metadata and message inventory; foundation context |
| S3 | rollout-2026-10-25T08-35-15 | Session metadata and message inventory; documentation/planning review context |
| S4 | rollout-2026-10-30T13-41-41 | Short session metadata/message inventory; no published excerpt |
| S5 | rollout-2026-10-30T13-45-15 | Selected original user messages and relevant user-facing assistant messages covering feature 002 |

Filename prefixes are locators, not claims about UTC conversion. Excerpt timestamps are copied from the JSONL records and explicitly labeled UTC. S5 was active during review; line numbers locate existing messages in the inspected snapshot, not a frozen complete export. This is a targeted review of messages, not a claim that every tool payload, reasoning record or historical artifact was reviewed.

## Published evidence

- [Selected excerpts](selected-excerpts.md): eight verbatim slices from S5 user messages, with timestamp/line locators and separately labeled editorial result summaries. The source slices were checked against original message text before publication.
- [AI assistance disclosure](../ai-usage.md): activities, responsibility split and design/debugging examples.
- [Validation record](../foundation-validation.md): existing Editor evidence, student device reports and explicit pending checks.
- Git commits available in the repository: `82f5b77` (initial flight plan), `26a3a66` (Quest-first planning), `c48c7a9` (static graybox), `b31efbc` (shared motor/Quest input checkpoint), `50cca6b` (weekly milestone/dashboard). Inspect with `git show <hash>`; commit history demonstrates changes, not independent device testing.

## Privacy and authenticity

Only the selected text is public. IDE context, workstation paths, session/account identifiers, tool transport IDs, device identifiers, raw console/build output, authentication data and unrelated conversation content are excluded. No hidden reasoning is published. Full raw logs and the local review catalog remain private; absence of a public raw transcript must not be described as absence of the original source.

The student can privately locate originals for a supervised demonstration if appropriate, after another privacy check. Do not upload entire session directories. The excerpts are not a complete conversation and must not be used to imply that an instruction was automatically satisfied. Result summaries are supported by code/history and reported tests, with remaining checks recorded separately.

No original log for any brainstorming before the available sessions was established. No such conversation is invented here. No new hardware video, screenshots or timed test report was created for this audit. Existing local screenshots and build artifacts are not being republished as physical-test evidence.
