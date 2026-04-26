// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


namespace Nyrvexa.Simulation.World
{
    /// <summary> M1: kara keşifçi (unit.scout) hedefi — biyom 4 (kıyı/su) geçit yok. </summary>
    public static class TilePassage
    {
        public static bool IsBlockedScoutEnter(ushort biomeId) => biomeId == 4;
    }
}
