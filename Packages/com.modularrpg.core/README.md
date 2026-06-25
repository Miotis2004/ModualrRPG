# Modular RPG Core Package

This embedded Unity package contains the shared Core foundation for Modular RPG.

## Package Layout

- `Runtime/` contains player-safe runtime contracts, services, lifecycle orchestration, validation helpers, identifiers, diagnostics, and configuration foundations.
- `Editor/` contains Unity Editor-only tooling for Core workflows and validation windows.
- `Tests/Runtime/` contains runtime test coverage for Core behavior.
- `Tests/Editor/` contains editor-only test coverage for validation and tooling.
- `Documentation~/` contains package documentation that Unity Package Manager can display.
- `Samples~/` contains importable sample content for a minimal Core setup.

## Boundary Rules

Runtime code must not reference editor-only APIs or assemblies. Editor code may reference the Runtime assembly when it needs to inspect or assist Core runtime objects.

The Core package must stay independent from gameplay modules. It may define shared infrastructure contracts, but it must not depend on inventory, stats, quests, combat, dialogue, NPC, equipment, crafting, economy, or other gameplay packages.
