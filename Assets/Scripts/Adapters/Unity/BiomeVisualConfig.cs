/* =============================================================================
 * File:           Assets/Scripts/Adapters/Unity/BiomeVisualConfig.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Biyom 1–6 zemin renklerinin tek merkezî kaynağı; hex + minimap’e sürükle.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

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
