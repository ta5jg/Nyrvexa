using System;

namespace Nyrvexa.Simulation.World
{
    /// <summary> Fraksiyon keşfini karo <see cref="TileCell.ExploredMask"/> üzerine yazar. </summary>
    public static class FogRevealService
    {
        public static void RevealDisk(WorldMapState map, int centerIndex, int radius, int factionIndex)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            if (map.Tiles == null) return;
            if (radius < 0) return;
            for (int i = 0; i < map.Tiles.Length; i++)
            {
                if (map.AxialDistanceBetween(centerIndex, i) <= radius)
                {
                    ref var t = ref map.GetTileRef(i);
                    FogOfWarState.RevealTileForFaction(ref t, factionIndex);
                }
            }
        }
    }
}
