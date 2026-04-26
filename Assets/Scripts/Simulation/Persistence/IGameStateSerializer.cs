/* =============================================================================
 * File:           Assets/Scripts/Simulation/Persistence/IGameStateSerializer.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   IGameStateSerializer — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Persistence
{
    public interface IGameStateSerializer
    {
        string SerializeGameState(GameStateRoot state);
        GameStateRoot DeserializeGameState(string json);
    }
}
