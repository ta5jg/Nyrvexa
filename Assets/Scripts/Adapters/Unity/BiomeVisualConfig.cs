// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using UnityEngine;

namespace Nyrvexa.Adapters
{
    /// <summary> Biyom 1–6 zemin renklerinin tek merkezî kaynağı; hex + minimap’e sürükle. </summary>
    [CreateAssetMenu(fileName = "BiomeVisualConfig", menuName = "Nyrvexa/Biome Visual Config", order = 10)]
    public sealed class BiomeVisualConfig : ScriptableObject
    {
        [Tooltip("Eleman 0..5 = biyom id 1..6.")]
        [SerializeField] private Color[] _biome1To6;

        private void OnValidate() => BiomeViewColors.EnsureBiomePalette(ref _biome1To6);

        /// <summary> Daima uzunluk 6. </summary>
        public Color[] GetPalette1To6()
        {
            BiomeViewColors.EnsureBiomePalette(ref _biome1To6);
            return _biome1To6;
        }
    }
}
