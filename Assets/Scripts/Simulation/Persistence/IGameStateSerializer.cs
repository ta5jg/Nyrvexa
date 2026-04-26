// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Persistence
{
    public interface IGameStateSerializer
    {
        string SerializeGameState(GameStateRoot state);
        GameStateRoot DeserializeGameState(string json);
    }
}
