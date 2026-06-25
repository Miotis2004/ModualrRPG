# Modular RPG Core

Core is the foundational package for Modular RPG. It owns shared runtime infrastructure only: lifecycle orchestration, service/system registration, events, identifiers, configuration, diagnostics, validation, and save contracts. Gameplay systems such as inventory, quests, combat, stats, dialogue, and NPC behavior belong in separate packages.

## Installation

The package is embedded at `Packages/com.modularrpg.core` and is recognized by Unity through `package.json`. Official modules should depend on this package instead of depending on each other.

## Runtime Setup

1. Add `RPGGameManager` to a scene GameObject.
2. Optionally create an `RPGCoreConfiguration` asset from **Assets > Create > Modular RPG > Core Configuration** and assign it to the manager.
3. Register systems before initialization, or disable automatic initialization and call `InitializeSystems` manually after registration.

## Public Contracts

- `IRPGSystem` identifies a system and exposes deterministic initialization ordering.
- `IRPGInitializable`, `IRPGStartable`, and `IRPGShutdown` opt a system into lifecycle stages.
- `IRPGServiceProvider` provides controlled access to registered services.
- `IRPGEventBus` supports decoupled publish/subscribe messaging.
- `IRPGSaveProvider` and `IRPGSaveData` define future save-system integration points.
- `IRPGConfigurationProvider` lets modules contribute validation-ready configuration providers.

## Authoring Workflow

Register Core-compatible systems with `RPGGameManager.RegisterSystem`. Systems should communicate through contracts, services, and `IRPGEventBus` rather than directly referencing unrelated gameplay packages.

## Validation

Use **Tools > Modular RPG > Core Validation** to inspect open scenes for GameManager and registry issues. Runtime validation helpers are also available through `RPGCoreValidator`.
