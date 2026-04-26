/* =============================================================================
 * File:           Assets/Scripts/Simulation/World/WorldMapState.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   WorldMapState — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Hex;

namespace Nyrvexa.Simulation.World
{
    public sealed class WorldMapState
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public TileCell[] Tiles { get; private set; }

        public void Allocate(int width, int height, ushort defaultBiome)
        {
            if (width <= 0 || height <= 0) throw new ArgumentOutOfRangeException();
            Width = width;
            Height = height;
            var n = width * height;
            Tiles = new TileCell[n];
            for (int i = 0; i < n; i++)
            {
                Tiles[i] = new TileCell
                {
                    BiomeId = defaultBiome,
                    OwnerFactionIndex = -1,
                    ExploredMask = 0
                };
            }
        }

        public bool InBounds(int col, int row) => col >= 0 && row >= 0 && col < Width && row < Height;

        public int ToIndex(int col, int row)
        {
            if (!InBounds(col, row)) throw new ArgumentOutOfRangeException();
            return OffsetOddRPointyGrid.FlatIndex(col, row, Width);
        }

        public void SetOwner(int tileIndex, int factionIndex)
        {
            if (tileIndex < 0 || tileIndex >= Tiles.Length) throw new ArgumentOutOfRangeException(nameof(tileIndex));
            if (factionIndex < -1) throw new ArgumentOutOfRangeException(nameof(factionIndex));
            var t = Tiles[tileIndex];
            t.OwnerFactionIndex = (sbyte)Math.Min(factionIndex, sbyte.MaxValue);
            Tiles[tileIndex] = t;
        }

        public void SetBiomeId(int tileIndex, ushort biomeId)
        {
            if (tileIndex < 0 || tileIndex >= Tiles.Length) throw new ArgumentOutOfRangeException(nameof(tileIndex));
            var t = Tiles[tileIndex];
            t.BiomeId = biomeId;
            Tiles[tileIndex] = t;
        }

        public ref TileCell GetTileRef(int tileIndex) => ref Tiles[tileIndex];

        /// <summary> 0..5 komşu; dışarıdaysa -1. </summary>
        public int GetNeighborIndex(int fromIndex, int direction0to5)
        {
            OffsetOddRPointyGrid.Decompose(fromIndex, Width, out var c, out var r);
            var ax = OffsetOddRPointyGrid.OffsetToAxial(c, r);
            Span<AxialCoord> n = stackalloc AxialCoord[6];
            ax.GetNeighbors(n);
            var dest = n[direction0to5 % 6];
            OffsetOddRPointyGrid.AxialToOffset(in dest, out var dc, out var dr);
            if (!InBounds(dc, dr)) return -1;
            return ToIndex(dc, dr);
        }

        public int AxialDistanceBetween(int tileA, int tileB)
        {
            OffsetOddRPointyGrid.Decompose(tileA, Width, out var ca, out var ra);
            OffsetOddRPointyGrid.Decompose(tileB, Width, out var cb, out var rb);
            var a = OffsetOddRPointyGrid.OffsetToAxial(ca, ra);
            var b = OffsetOddRPointyGrid.OffsetToAxial(cb, rb);
            return a.DistanceTo(in b);
        }
    }
}
