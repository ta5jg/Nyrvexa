// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Commands
{
    public interface IGameCommand
    {
        int IssuerFactionIndex { get; }
        void Apply(TurnContext ctx);
    }
}
