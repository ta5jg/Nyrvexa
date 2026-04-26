// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


namespace Nyrvexa.Simulation.Events
{
    /// <summary> Deterministik "olay" aşamasından düşen kod (M1: ileri HUD/rapor; mekanik yok). </summary>
    public sealed class RumorRollEvent : IGameEvent
    {
        public string Channel => "Crisis";
        public int Code { get; }
        public long Turn { get; }

        public RumorRollEvent(int code, long turn)
        {
            Code = code;
            Turn = turn;
        }
    }
}
