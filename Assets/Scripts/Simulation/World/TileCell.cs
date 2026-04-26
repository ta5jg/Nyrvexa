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
