// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


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
