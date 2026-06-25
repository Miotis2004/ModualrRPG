# Core Editor

Editor code in this directory is compiled only for the Unity Editor.

Editor tooling may reference the Core Runtime assembly to inspect scenes, validate setup, and provide authoring utilities. Runtime code must never reference this Editor assembly.

Keep editor workflows focused on Core infrastructure validation and authoring support; gameplay-module tooling belongs in the relevant gameplay package.
