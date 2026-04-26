// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


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
