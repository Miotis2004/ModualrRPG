using System;
using UnityEngine;

namespace ModularRPG.Core
{
    [Serializable]
    public struct RPGIdentifier : IEquatable<RPGIdentifier>
    {
        [SerializeField] private string value;

        public RPGIdentifier(string value)
        {
            this.value = string.IsNullOrWhiteSpace(value) ? Guid.NewGuid().ToString("N") : value;
        }

        public string Value => value;
        public bool IsValid => !string.IsNullOrWhiteSpace(value);
        public static RPGIdentifier NewIdentifier() => new RPGIdentifier(Guid.NewGuid().ToString("N"));
        public bool Equals(RPGIdentifier other) => string.Equals(value, other.value, StringComparison.Ordinal);
        public override bool Equals(object obj) => obj is RPGIdentifier other && Equals(other);
        public override int GetHashCode() => value == null ? 0 : value.GetHashCode();
        public override string ToString() => value ?? string.Empty;
    }

    public abstract class RPGIdentifiedObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private RPGIdentifier identifier;

        public RPGIdentifier Identifier => identifier;

        public void OnBeforeSerialize()
        {
            EnsureIdentifier();
        }

        public void OnAfterDeserialize() { }

        protected virtual void OnValidate()
        {
            EnsureIdentifier();
        }

        private void EnsureIdentifier()
        {
            if (!identifier.IsValid)
            {
                identifier = RPGIdentifier.NewIdentifier();
            }
        }
    }
}
