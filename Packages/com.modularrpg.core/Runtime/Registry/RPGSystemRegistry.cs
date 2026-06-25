using System;
using System.Collections.Generic;
using System.Linq;

namespace ModularRPG.Core
{
    public sealed class RPGSystemRegistry : IRPGServiceProvider
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
        private readonly List<IRPGSystem> systems = new List<IRPGSystem>();

        public IReadOnlyList<IRPGSystem> Systems => systems;

        public void RegisterSystem<TSystem>(TSystem system) where TSystem : class, IRPGSystem
        {
            if (system == null)
            {
                throw new ArgumentNullException(nameof(system));
            }

            Type type = typeof(TSystem);
            if (services.ContainsKey(type))
            {
                throw new InvalidOperationException($"A service or system of type '{type.FullName}' is already registered.");
            }

            services.Add(type, system);
            if (!systems.Contains(system))
            {
                systems.Add(system);
                systems.Sort((left, right) => left.InitializationOrder.CompareTo(right.InitializationOrder));
            }
        }

        public void RegisterService<TService>(TService service) where TService : class
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Type type = typeof(TService);
            if (services.ContainsKey(type))
            {
                throw new InvalidOperationException($"A service of type '{type.FullName}' is already registered.");
            }

            services.Add(type, service);
        }

        public bool TryGetService<TService>(out TService service) where TService : class
        {
            if (services.TryGetValue(typeof(TService), out object value))
            {
                service = value as TService;
                return service != null;
            }

            service = null;
            return false;
        }

        public TService GetService<TService>() where TService : class
        {
            if (TryGetService(out TService service))
            {
                return service;
            }

            throw new KeyNotFoundException($"No service of type '{typeof(TService).FullName}' is registered.");
        }

        public bool IsRegistered<TService>() where TService : class => services.ContainsKey(typeof(TService));

        public IReadOnlyList<IRPGSystem> GetSystemsInInitializationOrder() => systems.OrderBy(system => system.InitializationOrder).ToArray();
    }
}
