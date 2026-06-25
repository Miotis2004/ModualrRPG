# Phase 1: Core Foundation - Development Plan

This document outlines the detailed step-by-step development plan for Phase 1 of the Modular RPG framework. The goal of this phase is to establish the Core package and RPG GameManager foundation without any gameplay-specific logic.

## 1. Repository and Unity Project Setup
*   **Actionable Tasks:**
    *   Initialize the Unity 6 project architecture.
    *   Establish standard directory structures for Assets, Packages, and ProjectSettings.
    *   Set up source control exclusions (e.g., `.gitignore`) to ensure Unity-generated folders are not tracked.
*   **Constraints:**
    *   Keep the project independent of specific render pipelines (URP, HDRP, Built-in), input systems, or camera controllers.

## 2. Core Package Structure
*   **Status:** Completed and verified.
*   **Actionable Tasks:**
    *   Define the Core package as an embedded Unity package inside the project.
    *   Create distinct directories for Runtime code and Editor code.
*   **Verification:**
    *   The Core package is embedded at `Packages/com.modularrpg.core` and includes its Unity package manifest at `Packages/com.modularrpg.core/package.json`.
    *   Runtime code is isolated under `Packages/com.modularrpg.core/Runtime`.
    *   Editor code is isolated under `Packages/com.modularrpg.core/Editor`.
    *   Runtime tests and editor tests are separated under `Packages/com.modularrpg.core/Tests/Runtime` and `Packages/com.modularrpg.core/Tests/Editor`.
    *   The Core package manifest declares an empty dependency map, keeping Core free of gameplay-module dependencies.
*   **Constraints:**
    *   **Crucial:** Keep runtime code strictly separate from editor code.
    *   **Crucial:** The Core package must have zero dependencies on gameplay modules (e.g., inventory, stats).

## 3. Package Metadata & Assembly Definitions
*   **Status:** Completed and verified.
*   **Actionable Tasks:**
    *   Create `package.json` with appropriate semantic versioning and metadata to ensure Unity recognizes the Core package.
    *   Create Assembly Definition (`.asmdef`) files for the Runtime assembly.
    *   Create Assembly Definition (`.asmdef`) files for the Editor assembly.
*   **Verification:**
    *   `Packages/com.modularrpg.core/package.json` declares the embedded Core package as `com.modularrpg.core` at semantic version `0.1.0` with Unity 6 compatibility metadata, package type, license, author, repository, keywords, samples, and an intentionally empty dependency map.
    *   `Packages/com.modularrpg.core/Runtime/ModularRPG.Core.asmdef` defines the `ModularRPG.Core` runtime assembly with no references to editor or gameplay assemblies.
    *   `Packages/com.modularrpg.core/Editor/ModularRPG.Core.Editor.asmdef` defines the `ModularRPG.Core.Editor` editor-only assembly and references `ModularRPG.Core`.
    *   Runtime tests and editor tests have separate test assemblies that reference only the assemblies they need.
*   **Constraints:**
    *   The Editor assembly must reference the Runtime assembly, but not vice versa.
    *   Ensure both runtime and editor assemblies compile independently.

## 4. Naming and Namespace Conventions
*   **Actionable Tasks:**
    *   Define clear and consistent naming conventions for classes, interfaces, and assets.
    *   Establish namespace guidelines (e.g., `ModularRPG.Core`, `ModularRPG.Core.Editor`).

## 5. Common Interfaces and Data Contracts
*   **Actionable Tasks:**
    *   Define the module’s public contracts, boundaries, responsibilities, and non-responsibilities.
    *   Define base interfaces for shared framework infrastructure (e.g., generic system interfaces).
    *   Define Save-system contracts that future gameplay modules will implement.
    *   Establish runtime data and state ownership principles.

## 6. RPG GameManager
*   **Actionable Tasks:**
    *   Create the central `RPGGameManager` acting as the runtime integration point.
    *   Implement logic to expose shared Core services.
    *   Implement initialization logic to ensure initialization order is deterministic.
    *   Implement shutdown logic to ensure reliable shutdown behavior.
*   **Constraints:**
    *   The `RPGGameManager` must contain absolutely no gameplay logic.

## 7. System Registry
*   **Actionable Tasks:**
    *   Develop a registry to allow RPG systems and services to be registered and discovered.
    *   Provide controlled access to these registered systems.
    *   Handle duplicate registrations gracefully or with clear diagnostic errors.
    *   Define and handle missing system behavior.

## 8. Module Lifecycle
*   **Actionable Tasks:**
    *   Define lifecycle interfaces (e.g., `IInitialize`, `IStart`, `IShutdown`).
    *   Implement coordination of runtime lifecycle events across all registered systems through the `RPGGameManager`.

## 9. Event Communication
*   **Actionable Tasks:**
    *   Establish a shared, decoupled event communication system.
    *   Provide support for subscribing to and removing event listeners safely.
    *   Define public contracts and shared event data structures for cross-system communication.

## 10. Stable Identifiers
*   **Actionable Tasks:**
    *   Implement a robust system for stable identifiers (e.g., GUIDs, ScriptableObject-based IDs) to uniquely identify resources and instances.
    *   Ensure identifier stability is maintained across serialization and editor workflows.

## 11. Core Configuration
*   **Actionable Tasks:**
    *   Create shared configuration foundations (e.g., centralized ScriptableObjects or settings providers).
    *   Implement a configuration hub that modules can plug into.

## 12. Logging and Diagnostics
*   **Actionable Tasks:**
    *   Develop a centralized logging and diagnostics service.
    *   Implement categorized logging (e.g., Initialization, Errors, Lifecycle) to aid debugging.
    *   Implement reporting for missing or invalid module configurations.

## 13. Validation Tools
*   **Actionable Tasks:**
    *   Create runtime and editor validation utilities.
    *   Implement tools to check system registration integrity, initialization order, and configuration correctness.
    *   Provide clear editor feedback (dashboards, warnings) for validation errors.
*   **Constraints:**
    *   Validation tools must be editor-only where applicable, maintaining the runtime/editor separation.

## 14. Runtime and Editor Tests
*   **Actionable Tasks:**
    *   Write tests for Core lifecycle behavior, service registration, duplicate/missing systems, initialization order, and shutdown behavior.
    *   Write tests for event subscription/removal, identifier stability, and configuration validation.
*   **Constraints:**
    *   All runtime and editor tests must pass.
    *   Tests must verify that package independence is maintained.

## 15. Core Sample Scene
*   **Actionable Tasks:**
    *   Create a minimal Core sample scene demonstrating the correct setup of the `RPGGameManager` and system registration.
    *   Include sample scripts illustrating how to integrate a mock system using the Core lifecycle.

## 16. Core Documentation
*   **Actionable Tasks:**
    *   Write Package Documentation for Core, covering installation, setup, concepts, and the authoring workflow.
    *   Document public behavior, contracts, and APIs.
    *   Create an Architecture Guide defining package boundaries, dependencies, lifecycle behavior, communication patterns, and system ownership.
    *   Ensure documentation accurately describes the supported workflow before the milestone is considered complete.

## Completion Notes

Phase 1 is implemented in `Packages/com.modularrpg.core` with runtime contracts, `RPGGameManager`, registry, lifecycle orchestration, event bus, identifiers, configuration hub, diagnostics, validation helpers, editor validation UI, runtime/editor test coverage, package documentation, architecture documentation, and a Core sample scene.
