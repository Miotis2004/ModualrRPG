# Modular RPG Core Architecture

## Package Boundaries

Core contains no gameplay-specific behavior. It may define infrastructure contracts used by gameplay packages, but it must not own inventory, quests, combat, dialogue, NPC behavior, crafting, equipment, economy, or stat rules.

## Dependencies

The Runtime assembly has no references to gameplay modules. The Editor assembly references Runtime and is editor-only. Future modules should reference Runtime contracts and expose optional integrations through adapters or extension packages.

## Lifecycle

`RPGGameManager` coordinates registered systems in ascending `InitializationOrder` for initialization and start. Shutdown runs in reverse order so dependencies unwind predictably.

## Communication

Systems should use public interfaces, registered services, and `IRPGEventBus` events for cross-system communication. This keeps packages replaceable and prevents circular dependencies.

## Ownership

Each module owns its own runtime state and data validation. Core owns only shared orchestration, diagnostics, identifiers, and contracts.

## Configuration

`RPGCoreConfiguration` stores Core settings. `RPGConfigurationHub` allows future modules to contribute configuration providers without coupling Core to gameplay modules.
