using System;

namespace Nyrvexa.Simulation.Core
{
    /// <summary>
    /// JSON/mod tanımlarındaki tekil string anahtarın çalışma zamanı temsilcisi.
    /// </summary>
    public readonly struct DefId : IEquatable<DefId>
    {
        public readonly string Key;

        public DefId(string key)
        {
            Key = string.IsNullOrEmpty(key) ? string.Empty : key;
        }

        public bool IsValid => !string.IsNullOrEmpty(Key);

        public bool Equals(DefId other) => string.Equals(Key, other.Key, StringComparison.Ordinal);
        public override bool Equals(object obj) => obj is DefId o && Equals(o);
        public override int GetHashCode() => Key != null ? StringComparer.Ordinal.GetHashCode(Key) : 0;
        public static bool operator ==(DefId a, DefId b) => a.Equals(b);
        public static bool operator !=(DefId a, DefId b) => !a.Equals(b);
    }
}
