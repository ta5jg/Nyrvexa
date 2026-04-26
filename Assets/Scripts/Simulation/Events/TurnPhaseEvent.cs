// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


namespace Nyrvexa.Simulation.Events
{
    public sealed class TurnPhaseEvent : IGameEvent
    {
        public string Channel => "Turn";
        public string Phase { get; }
        public long Turn { get; }

        public TurnPhaseEvent(string phase, long turn)
        {
            Phase = phase;
            Turn = turn;
        }
    }
}
