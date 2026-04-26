/* =============================================================================
 * File:           Assets/Scripts/Simulation/World/WorldEntityRegistry.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   WorldEntityRegistry — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.World
{
    public sealed class WorldEntityRegistry
    {
        private uint _nextIndex = 1;
        public uint Generation { get; private set; } = 1;

        public EntityId Create()
        {
            if (_nextIndex == uint.MaxValue) throw new System.InvalidOperationException("Entity tükendi");
            return new EntityId(_nextIndex++, Generation);
        }

        public void Reset()
        {
            _nextIndex = 1;
            Generation++;
        }

        public void ReseedForLoad(uint nextIndex, uint generation)
        {
            if (nextIndex < 1) nextIndex = 1;
            _nextIndex = nextIndex;
            Generation = generation;
        }

        public uint GetNextIdSlot() => _nextIndex;
    }
}
