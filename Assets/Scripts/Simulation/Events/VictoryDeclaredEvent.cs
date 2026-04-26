// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


namespace Nyrvexa.Simulation.Events
{
    public sealed class VictoryDeclaredEvent : IGameEvent
    {
        public string Channel => "Victory";
        public int VictorFactionIndex { get; }
        public long TurnIndex;
        public long VictorScore { get; }

        public VictoryDeclaredEvent(int victorFactionIndex, long atTurn, long score)
        {
            VictorFactionIndex = victorFactionIndex;
            TurnIndex = atTurn;
            VictorScore = score;
        }
    }
}
