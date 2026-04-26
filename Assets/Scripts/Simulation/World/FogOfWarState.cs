// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using System;

namespace Nyrvexa.Simulation.World
{
    /// <summary> Keşif maskesine yazar; görüş halkası ileride birim menzilinden yeniden hesaplanır. </summary>
    public static class FogOfWarState
    {
        public static void RevealTileForFaction(ref TileCell tile, int factionIndex)
        {
            if (factionIndex < 0 || factionIndex >= TileCell.MaxSupportedFactionsForMask)
                throw new ArgumentOutOfRangeException(nameof(factionIndex));
            tile.ExploredMask |= (ushort)(1 << factionIndex);
        }

        public static bool IsExploredBy(in TileCell tile, int factionIndex)
        {
            if (factionIndex < 0 || factionIndex >= TileCell.MaxSupportedFactionsForMask) return false;
            return (tile.ExploredMask & (1 << factionIndex)) != 0;
        }
    }
}
