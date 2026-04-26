/* =============================================================================
 * File:           Assets/Scripts/Simulation/World/FogOfWarState.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Keşif maskesine yazar; görüş halkası ileride birim menzilinden yeniden
 *   hesaplanır.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

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
