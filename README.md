# Modular RPG

A modular, package-based RPG development framework for Unity 6.

Modular RPG is designed as a collection of independent systems that can be installed and used separately or combined into a complete RPG framework. Each system focuses on a specific area of RPG development, such as inventory, quests, dialogue, NPCs, combat, or character progression.

All official modules can integrate through the shared Core package and RPG GameManager while remaining loosely coupled and independently replaceable.

## Project Status

Modular RPG is currently in early development.

Phase 1 Core Foundation implementation is available in the embedded Core package. The first development phase established:

* Core package architecture
* RPG GameManager
* System registration
* Module lifecycle management
* Shared identifiers
* Event communication
* Logging and diagnostics
* Configuration foundations
* Package validation
* Automated testing foundations

Gameplay modules will be developed after the Core package continues through Unity validation and stabilization.

## Project Goals

The primary goal of Modular RPG is to provide reusable RPG systems without forcing developers to adopt an entire game framework.

The project is being designed around the following principles:

* Every gameplay system should be available as an independent Unity package.
* Modules should depend on Core rather than directly depending on one another.
* Developers should be able to replace an official module with a custom implementation.
* Systems should communicate through public contracts and events.
* Runtime systems should remain separate from editor tooling.
* Packages should be suitable for both small prototypes and larger production projects.
* The framework should not require a specific rendering pipeline, input system, camera system, or visual style.
* Documentation and tests should be treated as core project features.

## Modular Architecture

Modular RPG is not intended to be one large package.

Instead, it is a family of focused Unity packages that share a common architectural foundation.

A developer may use:

* Inventory without Quests
* Dialogue without Combat
* NPCs without Crafting
* Stats with a custom combat system
* Quests with a custom inventory system
* The complete package bundle

The Core package provides the common infrastructure that allows compatible modules to discover, reference, and communicate with one another.

## Core Package

The Core package is the only foundational package required by official Modular RPG modules.

Core is responsible for shared framework infrastructure, including:

* RPG GameManager
* System and service registration
* Module lifecycle coordination
* Event communication
* Stable identifiers
* Shared configuration
* Logging and diagnostics
* Save-system contracts
* Runtime validation
* Common interfaces and data contracts

Core does not contain gameplay-specific functionality.

It does not manage inventory, quests, combat, dialogue, NPC behavior, crafting, equipment, or character statistics.

## RPG GameManager

The RPG GameManager acts as the central runtime integration point for installed RPG systems.

Its responsibilities include:

* Registering available systems
* Providing controlled access to registered systems
* Initializing systems in the correct order
* Coordinating runtime lifecycle events
* Managing system shutdown
* Exposing shared Core services
* Reporting missing or invalid module configuration
* Supporting future save and load coordination

The RPG GameManager is not intended to contain gameplay logic.

Each gameplay module remains responsible for its own data, behavior, validation, and runtime state.

## Planned Modules

### Core

Provides shared architecture, lifecycle management, communication, identifiers, diagnostics, and the RPG GameManager.

### Stats

Provides attributes, resources, derived values, modifiers, regeneration, temporary effects, and progression-compatible stat definitions.

### Items

Provides item definitions, item categories, metadata, rarity, item properties, and shared item contracts.

### Inventory

Provides containers, item storage, stack management, transfers, restrictions, capacity rules, and inventory events.

### Equipment

Provides equipment slots, loadouts, item requirements, stat integration, and equipment change events.

### Characters

Provides shared character identities, runtime character data, player characters, companions, and non-player character foundations.

### NPCs

Provides NPC definitions, roles, schedules, interactions, relationships, and behavior integration points.

### Dialogue

Provides conversations, branching responses, conditions, actions, speaker data, and dialogue presentation contracts.

### Quests

Provides quest definitions, objectives, progression states, requirements, rewards, tracking, and journal integration.

### Factions

Provides factions, membership, relationships, reputation, hostility, and faction-based conditions.

### Combat

Provides combat participants, damage processing, targeting contracts, combat state, encounters, and configurable combat rules.

### Abilities

Provides active abilities, passive abilities, costs, cooldowns, requirements, effects, and ability progression.

### Status Effects

Provides buffs, debuffs, conditions, durations, stacking rules, periodic effects, and removal conditions.

### Loot

Provides loot tables, weighted rewards, enemy drops, containers, currency rewards, and randomized item generation.

### Economy

Provides currencies, prices, transactions, value calculation, and economy-related contracts.

### Vendors

Provides vendor inventories, buying, selling, stock rules, pricing modifiers, and restocking.

### Crafting

Provides recipes, ingredients, crafting stations, production requirements, upgrades, and crafting outcomes.

### World Interaction

Provides interactable objects, pickups, containers, doors, switches, triggers, and shared interaction contracts.

### Spawning and Encounters

Provides spawn definitions, encounter groups, respawn rules, spawn conditions, and world population management.

### Journal

Provides quest logs, lore entries, discovered information, bestiary entries, and player-facing records.

### Save System

Provides modular state collection, serialization, version handling, save slots, loading, and module-specific save providers.

### User Interface

Provides optional reference interfaces for supported systems while allowing developers to replace the complete UI layer.

### Localization

Provides text keys, localized content references, language selection, and integration points for text-heavy RPG systems.

## Package Independence

Each module should remain usable without installing unrelated modules.

For example, the Quest package should not require the Inventory package simply because a quest may reward an item.

Instead, optional integrations should be supplied through adapters, conditions, rewards, events, or extension packages.

This approach prevents circular dependencies and allows developers to choose only the systems required by their game.

## Package Categories

Modular RPG packages are divided into three general categories.

### Foundation Packages

Foundation packages provide shared architecture required by other systems.

The primary foundation package is Core.

### Gameplay Packages

Gameplay packages provide individual RPG features such as inventory, quests, dialogue, stats, combat, or crafting.

### Integration Packages

Integration packages connect otherwise independent systems.

Examples may include:

* Inventory and Quests integration
* Stats and Equipment integration
* Dialogue and Factions integration
* Combat and Abilities integration
* Quests and World Interaction integration

Integration packages prevent optional relationships from becoming mandatory dependencies.

## Unity Compatibility

Modular RPG is being developed for Unity 6.

The framework is intended to remain independent of:

* Universal Render Pipeline
* High Definition Render Pipeline
* Built-in Render Pipeline
* Unity Input System
* Legacy Input Manager
* Cinemachine
* A specific camera controller
* A specific character controller
* A specific networking solution
* A specific user interface framework

Individual samples or optional integrations may use additional Unity packages, but the Core runtime should remain as dependency-light as possible.

## Repository Purpose

This repository serves as the development and integration environment for the Modular RPG package family.

It contains:

* The Unity 6 development project
* Embedded package source
* Integration tests
* Example scenes
* Documentation
* Development planning
* Package release preparation

Unity-generated folders are not stored in source control.

The repository should contain the Unity project’s Assets, Packages, and ProjectSettings directories, along with the project documentation and package source.

## Installation

During early development, the Core package is maintained as an embedded package inside the Unity project.

Future installation methods are expected to include:

* Unity Package Manager installation from a Git URL
* Local package installation
* Downloadable package releases
* Individual module installation
* Complete framework bundle installation

Installation instructions will be expanded when the first public package version is released.

## Getting Started

The project is not yet ready for production use.

During the Core development phase, contributors and testers should:

1. Clone the repository.
2. Open the included project using the supported Unity 6 version.
3. Allow Unity to restore packages and regenerate local project data.
4. Confirm that the Core runtime and editor assemblies compile.
5. Open the Core sample or test scene.
6. Review the current development milestone before making changes.

Additional setup instructions will be added as the project matures.

## Development Approach

Each module will be developed through a consistent sequence:

1. Define the module’s purpose and boundaries.
2. Identify its responsibilities and non-responsibilities.
3. Define public contracts.
4. Define optional integration points.
5. Establish runtime data and state ownership.
6. Create editor authoring tools.
7. Add validation and diagnostics.
8. Add runtime and editor tests.
9. Create samples.
10. Complete package documentation.
11. Test the module independently.
12. Test the module as part of the full bundle.
13. Prepare the module for release.

A module is not considered complete simply because its primary runtime feature works.

Documentation, tests, samples, package metadata, validation, and upgrade planning are part of the definition of done.

## Development Roadmap

### Phase 1: Core Foundation

* Repository and Unity project setup
* Core package structure
* Assembly definitions
* Package metadata
* Naming and namespace conventions
* RPG GameManager
* System registry
* Module lifecycle
* Event communication
* Stable identifiers
* Logging and diagnostics
* Core configuration
* Validation tools
* Runtime tests
* Editor tests
* Core sample scene
* Core documentation

### Phase 2: Stats and Character Foundations

* Character identity
* Attribute definitions
* Resource values
* Derived statistics
* Stat modifiers
* Character runtime state
* Progression contracts

### Phase 3: Items and Inventory

* Item definitions
* Item instances
* Inventory containers
* Item stacks
* Transfers
* Capacity rules
* Inventory events
* Inventory editor tools

### Phase 4: Equipment

* Equipment slots
* Loadouts
* Equip and unequip rules
* Requirements
* Stat integration
* Equipment events

### Phase 5: NPCs and Interaction

* NPC definitions
* Character roles
* Interaction contracts
* Relationships
* Schedules
* World interactables

### Phase 6: Dialogue and Quests

* Conversation definitions
* Dialogue conditions
* Dialogue actions
* Quest definitions
* Quest objectives
* Quest rewards
* Journal integration

### Phase 7: Combat and Abilities

* Combat participants
* Damage and healing
* Targeting
* Abilities
* Costs
* Cooldowns
* Status effects
* Encounter state

### Phase 8: Economy and Crafting

* Currency
* Vendors
* Prices
* Loot tables
* Recipes
* Crafting stations
* Item upgrades

### Phase 9: Save System

* Save providers
* Module state collection
* Serialization
* Save slots
* Versioning
* Migration support
* Load coordination

### Phase 10: User Interface and Tools

* Reference runtime UI
* Custom inspectors
* Editor windows
* Database tools
* Validation dashboards
* Project setup utilities

### Phase 11: Packaging and Release

* Package separation
* Independent package testing
* Bundle testing
* Samples
* Documentation review
* Versioning
* Release notes
* Distribution preparation

## Testing Strategy

Each package should include tests appropriate to its responsibilities.

Testing will cover:

* Core lifecycle behavior
* Service registration
* Duplicate registration handling
* Missing system behavior
* Initialization order
* Shutdown behavior
* Event subscription and removal
* Identifier stability
* Configuration validation
* Package independence
* Optional module integration
* Save compatibility
* Editor tooling
* Upgrade and migration behavior

The complete framework bundle will also be tested as an integrated system.

## Versioning

Modular RPG packages will follow semantic versioning.

Version numbers will use the following format:

**Major.Minor.Patch**

* Major versions may introduce breaking API or data changes.
* Minor versions may add backward-compatible features.
* Patch versions may include backward-compatible fixes and improvements.

Packages within the framework may have independent version numbers.

Compatibility requirements between modules will be documented in each package.

## Documentation

Project documentation will be divided by purpose.

### README

Introduces the project, goals, packages, installation approach, and roadmap.

### Development Guide

Defines the step-by-step implementation plan and development milestones.

### Architecture Guide

Defines package boundaries, dependencies, lifecycle behavior, communication patterns, and system ownership.

### Package Documentation

Each package will include its own installation, setup, concepts, authoring workflow, integration points, samples, and troubleshooting information.

### API Documentation

Public contracts, events, extension points, and compatibility expectations will be documented before stable releases.

## Contributing

The contribution process will be formalized after the Core architecture is established.

Contributions should follow these general principles:

* Preserve package independence.
* Avoid unnecessary dependencies.
* Do not place gameplay logic in Core.
* Keep runtime code separate from editor code.
* Document public behavior.
* Add tests for new functionality.
* Maintain backward compatibility whenever practical.
* Use clear and consistent naming.
* Avoid introducing integrations directly into otherwise independent modules.
* Update the changelog when behavior changes.

Before proposing a major system or architectural change, contributors should review the project’s development and architecture documentation.

## Planned Distribution

The long-term goal is to make modules available:

* Individually
* As related module collections
* As a complete Modular RPG bundle

A developer should never be required to install the complete bundle to use a single system.

## License

A license has not yet been finalized.

Until a license is added, the repository should be treated as source-available for development and evaluation only. No permission to redistribute, sublicense, sell, or incorporate the project into another distributed product should be assumed.

The final license will be selected before the first public release.

## Current Milestone

The current milestone is:

**Core Package and RPG GameManager Foundation**

This milestone is complete when:

* Unity recognizes the Core package.
* Runtime and editor assemblies compile independently.
* The Core package has no dependency on gameplay modules.
* RPG systems can be registered and discovered.
* Systems have a defined lifecycle.
* Initialization order is deterministic.
* Shutdown behavior is reliable.
* Shared event communication is available.
* Stable identifiers are supported.
* Logging and validation are available.
* Core runtime and editor tests pass.
* A minimal Core sample demonstrates correct setup.
* Core documentation accurately describes the supported workflow.

## Long-Term Vision

Modular RPG aims to become a professional collection of reusable Unity RPG systems rather than a rigid, all-or-nothing game template.

Developers should be able to begin with one module, add more as their game grows, replace systems when requirements change, and retain control over the architecture of their own project.

The framework should provide structure without taking ownership of the game.
