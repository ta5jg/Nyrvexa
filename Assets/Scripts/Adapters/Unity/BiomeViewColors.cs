// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.World;
using UnityEngine;

namespace Nyrvexa.Adapters
{
    /// <summary> Biyom 1–6 zemin rengi (M1) — hex + minimap ortak. </summary>
    public static class BiomeViewColors
    {
        private static readonly Color[] kDefault1To6 =
        {
            new Color(0.20f, 0.50f, 0.30f, 1f),
            new Color(0.10f, 0.36f, 0.24f, 1f),
            new Color(0.70f, 0.58f, 0.32f, 1f),
            new Color(0.40f, 0.50f, 0.55f, 1f),
            new Color(0.45f, 0.36f, 0.32f, 1f),
            new Color(0.22f, 0.48f, 0.45f, 1f),
        };

        public static void EnsureBiomePalette(ref Color[] paletteBiome1To6)
        {
            if (paletteBiome1To6 == null || paletteBiome1To6.Length != 6)
            {
                paletteBiome1To6 = new Color[6];
                for (int i = 0; i < 6; i++)
                    paletteBiome1To6[i] = kDefault1To6[i];
            }
        }

        /// <summary> <paramref name="config"/> dolu: asset paleti; yok: satır içi <paramref name="inlinePalette"/> doldurulup kullanılır. </summary>
        public static Color[] ResolveBiomePalette(BiomeVisualConfig config, ref Color[] inlinePalette)
        {
            if (config != null) return config.GetPalette1To6();
            EnsureBiomePalette(ref inlinePalette);
            return inlinePalette;
        }

        public static Color Ground(ushort biomeId, Color neutral, Color[] paletteBiome1To6)
        {
            if (biomeId == 0) return neutral;
            if (biomeId is < 1 or > 6) return neutral;
            var i = biomeId - 1;
            if (paletteBiome1To6 != null && i < paletteBiome1To6.Length) return paletteBiome1To6[i];
            return kDefault1To6[i];
        }

        public static Color ExploredCellSurface(
            in TileCell t,
            bool tintByBiome,
            float biomeFactionMix,
            Color neutral,
            Color faction0,
            Color faction1,
            Color[] paletteBiome1To6)
        {
            if (!tintByBiome)
            {
                return t.OwnerFactionIndex switch
                {
                    0 => faction0,
                    1 => faction1,
                    _ => neutral
                };
            }
            var ground = Ground(t.BiomeId, neutral, paletteBiome1To6);
            return t.OwnerFactionIndex switch
            {
                0 => Color.Lerp(ground, faction0, biomeFactionMix),
                1 => Color.Lerp(ground, faction1, biomeFactionMix),
                _ => ground
            };
        }
    }
}
