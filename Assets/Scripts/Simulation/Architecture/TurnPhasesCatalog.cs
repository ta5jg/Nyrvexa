/* =============================================================================
 * File:           Assets/Scripts/Simulation/Architecture/TurnPhasesCatalog.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   Sistem 1–30 ile TurnPipeline aşamaları arasında sembolik eşleme.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.Architecture
{
    /// <summary> Sistem 1–30 ile TurnPipeline aşamaları arasında sembolik eşleme. </summary>
    public static class TurnPhasesCatalog
    {
        public const string UpkeepUnitMovement = "Upkeep_RefreshMovement";
        public const string EventsAndCrises = "EventsAndCrises";
        public const string Economy = "Economy";
        public const string Production = "Production";
        public const string ResearchTick = "ResearchTick";
        public const string OpponentStrategic = "OpponentStrategic";
        public const string ApplyPlayerCommands = "ApplyCommands";
        public const string SightAndExploration = "SightAndExploration";
        public const string Movement = "Movement";
        public const string Combat = "Combat";
        public const string Espionage = "Espionage";
        public const string CulturePressure = "CulturePressure";
        public const string DiplomacyCommit = "DiplomacyCommit";
        public const string TradeRoutes = "TradeRoutes";
        public const string PopulationHappiness = "PopulationHappiness";
        public const string GovernmentPolicies = "GovernmentPolicies";
        public const string VictoryCheck = "VictoryCheck";
    }
}
