using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ModularRPG.Core.Editor
{
    public sealed class RPGCoreValidationWindow : EditorWindow
    {
        private Vector2 scroll;
        private RPGValidationReport lastReport;

        [MenuItem("Tools/Modular RPG/Core Validation")]
        public static void Open()
        {
            GetWindow<RPGCoreValidationWindow>("RPG Core Validation");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Modular RPG Core Validation", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Validates RPGGameManager registration, lifecycle setup, and configuration references in open scenes.", MessageType.Info);

            if (GUILayout.Button("Validate Open Scenes"))
            {
                ValidateOpenScenes();
            }

            if (lastReport == null) return;

            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (RPGValidationMessage message in lastReport.Messages)
            {
                MessageType type = message.Severity == RPGValidationSeverity.Error ? MessageType.Error :
                    message.Severity == RPGValidationSeverity.Warning ? MessageType.Warning : MessageType.Info;
                EditorGUILayout.HelpBox(message.Message, type);
            }
            EditorGUILayout.EndScrollView();
        }

        private void ValidateOpenScenes()
        {
            lastReport = new RPGValidationReport();
            RPGGameManager[] managers = FindObjectsByType<RPGGameManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (managers.Length == 0)
            {
                lastReport.Add(RPGValidationSeverity.Warning, "No RPGGameManager exists in the open scenes.");
                return;
            }

            if (managers.Length > 1)
            {
                lastReport.Add(RPGValidationSeverity.Error, "More than one RPGGameManager exists in the open scenes.");
            }

            foreach (RPGGameManager manager in managers)
            {
                RPGValidationReport registryReport = RPGCoreValidator.ValidateRegistry(manager.Registry);
                foreach (RPGValidationMessage message in registryReport.Messages)
                {
                    lastReport.Add(message.Severity, message.Message);
                }

                if (manager.Configuration == null)
                {
                    lastReport.Add(RPGValidationSeverity.Warning, "RPGGameManager has no RPGCoreConfiguration assigned; defaults will be used.");
                }
            }

            if (!lastReport.Messages.Any(message => message.Severity == RPGValidationSeverity.Error || message.Severity == RPGValidationSeverity.Warning))
            {
                lastReport.Add(RPGValidationSeverity.Info, "Open scene Core validation passed.");
            }
        }
    }
}
