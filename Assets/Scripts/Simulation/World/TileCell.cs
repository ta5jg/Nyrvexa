// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


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
