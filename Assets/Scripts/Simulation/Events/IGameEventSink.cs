// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


namespace Nyrvexa.Simulation.Events
{
    public interface IGameEventSink
    {
        void Publish(IGameEvent evt);
    }
}
