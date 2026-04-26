/* =============================================================================
 * File:           Assets/Scripts/Simulation/World/Generation/DefaultProceduralWorldGenerator.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Deterministik, seed’li sütun/ satır biyom varyasyonu (0..5 aralığına
 *   sıkılmış; oyun dengesi ilerde JSON ile).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Architecture;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.World.Generation
{
    /// <summary> Deterministik, seed’li sütun/ satır biyom varyasyonu (0..5 aralığına sıkılmış; oyun dengesi ilerde JSON ile). </summary>
    public sealed class DefaultProceduralWorldGenerator : IWorldGenerator
    {
        public void GenerateInto(GameStateRoot state, ref DeterministicRng rng, in WorldGenParams p)
        {
            _ = rng;
            if (state.World.Tiles == null) return;
            var map = state.World;
            var w = p.Width;
            var h = p.Height;
            if (map.Width != w || map.Height != h) return;
            var seed = state.Header.WorldSeed;
            for (int row = 0; row < h; row++)
            for (int col = 0; col < w; col++)
            {
                int idx = col + row * w;
                if (idx >= map.Tiles.Length) continue;
                var mix = (ulong)(col * 0x9E37_79B1L ^ row * 0x85EB_CA6BL) ^ (seed * 0xC2B2_AE3DUL);
                var biome = (ushort)(1u + (mix % 6u));
                map.SetBiomeId(idx, biome);
            }
        }
    }
}
