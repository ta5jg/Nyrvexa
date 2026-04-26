/* =============================================================================
 * File:           Assets/Scripts/Simulation/State/GameStateHeader.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   GameStateHeader — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.State
{
    public sealed class GameStateHeader
    {
        public int SchemaVersion = GameSchema.Current;
        public ulong WorldSeed;
        public long TurnIndex;
        /// <summary>Deterministik RNG tam durumu (kayıt/MP eşiği).</summary>
        public ulong RngState64;
        /// <summary>-1 = oyun açık; 0+ = ilan edilen kazanan fraksiyon. </summary>
        public int DeclaredVictorFactionIndex = -1;

        public void AdvanceTurn()
        {
            TurnIndex++;
        }
    }
}
