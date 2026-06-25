using System;
using System.Collections.Generic;

namespace ModularRPG.Core
{
    public interface IRPGEvent { }

    public interface IRPGEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> listener) where TEvent : IRPGEvent;
        void Unsubscribe<TEvent>(Action<TEvent> listener) where TEvent : IRPGEvent;
        void Publish<TEvent>(TEvent eventData) where TEvent : IRPGEvent;
        void Clear();
    }

    public sealed class RPGEventBus : IRPGEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> listeners = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<TEvent>(Action<TEvent> listener) where TEvent : IRPGEvent
        {
            if (listener == null) throw new ArgumentNullException(nameof(listener));
            Type eventType = typeof(TEvent);
            if (!listeners.TryGetValue(eventType, out List<Delegate> eventListeners))
            {
                eventListeners = new List<Delegate>();
                listeners.Add(eventType, eventListeners);
            }

            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void Unsubscribe<TEvent>(Action<TEvent> listener) where TEvent : IRPGEvent
        {
            if (listener == null) return;
            if (listeners.TryGetValue(typeof(TEvent), out List<Delegate> eventListeners))
            {
                eventListeners.Remove(listener);
            }
        }

        public void Publish<TEvent>(TEvent eventData) where TEvent : IRPGEvent
        {
            if (eventData == null) throw new ArgumentNullException(nameof(eventData));
            if (!listeners.TryGetValue(typeof(TEvent), out List<Delegate> eventListeners)) return;

            Delegate[] snapshot = eventListeners.ToArray();
            for (int i = 0; i < snapshot.Length; i++)
            {
                ((Action<TEvent>)snapshot[i]).Invoke(eventData);
            }
        }

        public void Clear() => listeners.Clear();
    }

    public readonly struct RPGSystemLifecycleEvent : IRPGEvent
    {
        public RPGSystemLifecycleEvent(string systemId, string lifecycleStage)
        {
            SystemId = systemId;
            LifecycleStage = lifecycleStage;
        }

        public string SystemId { get; }
        public string LifecycleStage { get; }
    }
}
