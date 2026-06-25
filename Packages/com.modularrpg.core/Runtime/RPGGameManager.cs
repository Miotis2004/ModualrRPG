using System;
using UnityEngine;

namespace ModularRPG.Core
{
    [DisallowMultipleComponent]
    public sealed class RPGGameManager : MonoBehaviour, IRPGServiceProvider
    {
        [SerializeField] private RPGCoreConfiguration configuration;
        [SerializeField] private bool initializeOnAwake = true;
        [SerializeField] private bool startOnStart = true;

        private readonly RPGSystemRegistry registry = new RPGSystemRegistry();
        private readonly RPGEventBus eventBus = new RPGEventBus();
        private readonly RPGConfigurationHub configurationHub = new RPGConfigurationHub();
        private readonly UnityRPGDiagnostics diagnostics = new UnityRPGDiagnostics();

        public RPGManagerState State { get; private set; } = RPGManagerState.Created;
        public RPGSystemRegistry Registry => registry;
        public IRPGEventBus Events => eventBus;
        public RPGConfigurationHub ConfigurationHub => configurationHub;
        public RPGCoreConfiguration Configuration => configuration;

        private void Awake()
        {
            RegisterCoreServices();
            if (initializeOnAwake)
            {
                InitializeSystems();
            }
        }

        private void Start()
        {
            if (startOnStart)
            {
                StartSystems();
            }
        }

        private void OnDestroy()
        {
            if (State != RPGManagerState.Shutdown)
            {
                ShutdownSystems();
            }
        }

        public void RegisterCoreServices()
        {
            RegisterIfMissing<IRPGServiceProvider>(this);
            RegisterIfMissing<IRPGEventBus>(eventBus);
            RegisterIfMissing<RPGConfigurationHub>(configurationHub);
            RegisterIfMissing<IRPGDiagnostics>(diagnostics);
        }

        public void RegisterSystem<TSystem>(TSystem system) where TSystem : class, IRPGSystem => registry.RegisterSystem(system);
        public bool TryGetService<TService>(out TService service) where TService : class => registry.TryGetService(out service);
        public TService GetService<TService>() where TService : class => registry.GetService<TService>();

        public void InitializeSystems()
        {
            RegisterCoreServices();
            if (State != RPGManagerState.Created) return;

            foreach (IRPGSystem system in registry.GetSystemsInInitializationOrder())
            {
                if (system is IRPGInitializable initializable)
                {
                    initializable.Initialize(this);
                    PublishLifecycle(system, "Initialized");
                }
            }

            State = RPGManagerState.Initialized;
        }

        public void StartSystems()
        {
            if (State == RPGManagerState.Created)
            {
                InitializeSystems();
            }

            if (State != RPGManagerState.Initialized) return;

            foreach (IRPGSystem system in registry.GetSystemsInInitializationOrder())
            {
                if (system is IRPGStartable startable)
                {
                    startable.StartSystem(this);
                    PublishLifecycle(system, "Started");
                }
            }

            State = RPGManagerState.Started;
        }

        public void ShutdownSystems()
        {
            if (State == RPGManagerState.Shutdown) return;

            IRPGSystem[] systems = registry.GetSystemsInInitializationOrder() as IRPGSystem[];
            if (systems == null)
            {
                systems = new System.Collections.Generic.List<IRPGSystem>(registry.GetSystemsInInitializationOrder()).ToArray();
            }

            for (int i = systems.Length - 1; i >= 0; i--)
            {
                if (systems[i] is IRPGShutdown shutdown)
                {
                    shutdown.Shutdown(this);
                    PublishLifecycle(systems[i], "Shutdown");
                }
            }

            eventBus.Clear();
            State = RPGManagerState.Shutdown;
        }

        private void RegisterIfMissing<TService>(TService service) where TService : class
        {
            if (!registry.IsRegistered<TService>())
            {
                registry.RegisterService(service);
            }
        }

        private void PublishLifecycle(IRPGSystem system, string stage)
        {
            eventBus.Publish(new RPGSystemLifecycleEvent(system.SystemId, stage));
            if (configuration == null || configuration.LogLifecycleEvents)
            {
                diagnostics.Log(RPGLogCategory.Lifecycle, $"{system.SystemId} {stage}.");
            }
        }
    }
}
