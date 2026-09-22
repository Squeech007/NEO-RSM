# RSM Integration Layer

This directory contains the integration layer maintained on top of upstream
Reedit Ship Management.

## Principles

- Keep the RSM base as close to upstream as practical.
- Do not duplicate the RSM source here.
- Do not delete existing RSM functionality merely to add integrations.
- Keep our changes in small, logically separated Git commits.
- `integration/patches/` may contain exported patch files for portability.
- `integration/notes/` records reintegration decisions and conflict handling.
- `integration/tools/` contains helper scripts used by the integration workflow.

## External Controllers

RSM may communicate with external controllers through its existing controller
mechanisms. FCM is intended to be integrated as another controller alongside
existing NavOS/EFC support.

## Planned Integration Areas

1. FCM interface
2. Flight telemetry
3. HUD presentation
4. Additional system telemetry
5. AutoStance integration
6. Other Cortex-derived functionality only where deliberately selected

## Update Strategy

Upstream RSM updates are optional.

When desired:

1. Update/rebase the RSM integration branch onto the chosen upstream commit.
2. Reapply/reconcile the integration commits.
3. Resolve only the changes that actually conflict.
4. Build and test.
5. Keep the resulting integration commits separate and understandable.

The deployed RSM version is therefore always a deliberate combination of:
- an upstream RSM baseline
- our integration commits
