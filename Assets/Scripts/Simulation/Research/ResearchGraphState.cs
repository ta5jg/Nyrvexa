// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Research
{
    /// <summary> Rezerv / MP-ileri senk. M1'de ağaç <see cref="Nyrvexa.Simulation.State.FactionState.UnlockedTechnologies"/> üzerinde. </summary>
    public sealed class ResearchGraphState
    {
        public readonly HashSet<DefId> Unlocked = new HashSet<DefId>();
        public readonly Dictionary<DefId, int> InProgress = new Dictionary<DefId, int>();
    }
}
