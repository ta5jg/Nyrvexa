/* =============================================================================
 * File:           Assets/Scripts/Simulation/World/BiomeM1Names.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   M1: prosedürel biyom 1..6 (0 = ayrılmamış) kısa etiket - HUD/araç için.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.World
{
    /// <summary> M1: prosedürel biyom 1..6 (0 = ayrılmamış) kısa etiket — HUD/araç için. </summary>
    public static class BiomeM1Names
    {
        private static readonly string[] k1To6 = { "Ova", "Koru", "Tepe", "Kıyı", "Bozkır", "Tundra" };

        public static string TryGet(ushort biomeId)
        {
            if (biomeId == 0) return "—";
            if (biomeId < 1 || biomeId > 6) return "?" + biomeId;
            return k1To6[biomeId - 1];
        }
    }
}
