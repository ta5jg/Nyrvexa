/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/TurnContext.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   TurnContext — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Loop
{
    public sealed class TurnContext
    {
        public GameStateRoot State { get; }
        private DeterministicRng _rng;

        /// <summary> Tüm aşamalar aynı durumu paylaşır; deterministik oynat için struct kopya değil. </summary>
        public ref DeterministicRng Rng => ref _rng;

        public TurnContext(GameStateRoot state, DeterministicRng rng)
        {
            State = state;
            _rng = rng;
        }

        public void SyncRngFromHeader() => _rng.Reseed(State.Header.RngState64);

        public void PersistRngToHeader() => State.Header.RngState64 = _rng.State;
    }
}
