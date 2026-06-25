using System;

namespace ModularRPG.Core
{
    /// <summary>
    /// Base contract for any runtime system that participates in the Modular RPG lifecycle.
    /// </summary>
    public interface IRPGSystem
    {
        string SystemId { get; }
        int InitializationOrder { get; }
    }

    public interface IRPGInitializable
    {
        void Initialize(IRPGServiceProvider services);
    }

    public interface IRPGStartable
    {
        void StartSystem(IRPGServiceProvider services);
    }

    public interface IRPGShutdown
    {
        void Shutdown(IRPGServiceProvider services);
    }

    public interface IRPGServiceProvider
    {
        bool TryGetService<TService>(out TService service) where TService : class;
        TService GetService<TService>() where TService : class;
    }

    public abstract class RPGSystemBase : IRPGSystem
    {
        public virtual string SystemId => GetType().FullName ?? GetType().Name;
        public virtual int InitializationOrder => 0;
    }

    public enum RPGManagerState
    {
        Created,
        Initialized,
        Started,
        Shutdown
    }
}
