/* =============================================================================
 * File:           Assets/Scripts/Simulation/Core/EntityId.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   Kalıcı varlık kimliği: indeks + nesil (yeniden kullanım için).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;

namespace Nyrvexa.Simulation.Core
{
    /// <summary>
    /// Kalıcı varlık kimliği: indeks + nesil (yeniden kullanım için).
    /// </summary>
    public readonly struct EntityId : IEquatable<EntityId>
    {
        public readonly uint Index;
        public readonly uint Generation;

        public EntityId(uint index, uint generation)
        {
            Index = index;
            Generation = generation;
        }

        public static EntityId Invalid => new EntityId(0, 0);

        public bool IsValid => Index != 0;

        public bool Equals(EntityId other) => Index == other.Index && Generation == other.Generation;
        public override bool Equals(object obj) => obj is EntityId o && Equals(o);
        public override int GetHashCode() => (int)Index ^ ((int)Generation << 16);
        public static bool operator ==(EntityId a, EntityId b) => a.Equals(b);
        public static bool operator !=(EntityId a, EntityId b) => !a.Equals(b);
    }
}
