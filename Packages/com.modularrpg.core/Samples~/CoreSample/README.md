# Core Sample

Create an empty scene, add an `RPGGameManager` component to a GameObject, then add `MockCoreSystemInstaller` to another GameObject. The installer registers a mock system that participates in initialize, start, and shutdown lifecycle calls.

This sample intentionally contains no gameplay logic. It demonstrates only Core package registration and lifecycle integration.

The included sample scene disables automatic initialization on Awake so all installer components can register systems before `RPGGameManager.StartSystems` runs during `Start`.
