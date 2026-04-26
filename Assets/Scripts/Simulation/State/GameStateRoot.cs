/* =============================================================================
 * File:           Assets/Scripts/Simulation/State/GameStateRoot.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Tüm oyun: kayıt, MP senk, tekrar üretim.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using System.Collections.Generic;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Data;
using Nyrvexa.Simulation.Diplomacy;
using Nyrvexa.Simulation.Economy;
using Nyrvexa.Simulation.Research;
using Nyrvexa.Simulation.World;

namespace Nyrvexa.Simulation.State
{
    /// <summary>
    /// Tüm oyun: kayıt, MP senk, tekrar üretim.
    /// </summary>
    public sealed class GameStateRoot
    {
        public GameStateHeader Header = new GameStateHeader();
        public List<FactionState> Factions = new List<FactionState>();
        public ResearchGraphState Research = new ResearchGraphState();
        public DiploMatrixState Diplomacy = new DiploMatrixState();
        public CommandJournal CommandJournal = new CommandJournal();
        public WorldMapState World = new WorldMapState();
        public EconomyDefCatalog Defs = EconomyDefCatalog.CreateDefault();
        public UnitArchetypeCatalog UnitDefs = UnitArchetypeCatalog.CreateDefault();
        public WorldEntityRegistry Registry = new WorldEntityRegistry();
        public readonly Dictionary<EntityId, CityState> Cities = new Dictionary<EntityId, CityState>();
        public readonly Dictionary<EntityId, UnitState> Units = new Dictionary<EntityId, UnitState>();
        public readonly Dictionary<int, EntityId> UnitOnTile = new Dictionary<int, EntityId>();

        public FactionState GetFaction(int index)
        {
            if (index < 0 || index >= Factions.Count) throw new ArgumentOutOfRangeException(nameof(index));
            return Factions[index];
        }
    }
}
