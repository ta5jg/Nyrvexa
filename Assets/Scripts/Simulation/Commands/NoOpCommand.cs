// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Commands
{
    public sealed class NoOpCommand : IGameCommand
    {
        public int IssuerFactionIndex { get; }
        public NoOpCommand(int issuerFactionIndex) => IssuerFactionIndex = issuerFactionIndex;
        public void Apply(TurnContext ctx) { }
    }
}
