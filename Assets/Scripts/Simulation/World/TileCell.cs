/* =============================================================================
 * File:           Assets/Scripts/Simulation/World/TileCell.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   TileCell — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.World
{
    public struct TileCell
    {
        public ushort BiomeId;
        /// <summary>-1 = doğa, 0+ = fraksiyon indeksi.</summary>
        public sbyte OwnerFactionIndex;
        /// <summary>Her bit bir fraksiyonun “bir kez gördü” bilgisi (Civ tarzı kalıcı sis açma).</summary>
        public ushort ExploredMask;

        public static readonly int MaxSupportedFactionsForMask = 16;
    }
}
