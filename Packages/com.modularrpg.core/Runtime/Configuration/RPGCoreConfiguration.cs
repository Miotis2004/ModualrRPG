using System.Collections.Generic;
using UnityEngine;

namespace ModularRPG.Core
{
    public interface IRPGConfigurationProvider
    {
        string ConfigurationId { get; }
        bool Validate(out string message);
    }

    [CreateAssetMenu(menuName = "Modular RPG/Core Configuration", fileName = "RPGCoreConfiguration")]
    public sealed class RPGCoreConfiguration : ScriptableObject
    {
        [SerializeField] private bool enableDiagnostics = true;
        [SerializeField] private bool logLifecycleEvents = true;

        public bool EnableDiagnostics => enableDiagnostics;
        public bool LogLifecycleEvents => logLifecycleEvents;
    }

    public sealed class RPGConfigurationHub
    {
        private readonly List<IRPGConfigurationProvider> providers = new List<IRPGConfigurationProvider>();
        public IReadOnlyList<IRPGConfigurationProvider> Providers => providers;

        public void RegisterProvider(IRPGConfigurationProvider provider)
        {
            if (provider != null && !providers.Contains(provider))
            {
                providers.Add(provider);
            }
        }
    }
}
