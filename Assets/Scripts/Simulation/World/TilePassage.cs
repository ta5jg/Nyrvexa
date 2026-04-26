/* =============================================================================
 * File:           Assets/Scripts/Simulation/World/TilePassage.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   M1: kara keşifçi (unit.scout) hedefi - biyom 4 (kıyı/su) geçit yok.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.World
{
    /// <summary> M1: kara keşifçi (unit.scout) hedefi — biyom 4 (kıyı/su) geçit yok. </summary>
    public static class TilePassage
    {
        public static bool IsBlockedScoutEnter(ushort biomeId) => biomeId == 4;
    }
}
