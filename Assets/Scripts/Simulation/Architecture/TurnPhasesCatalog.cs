// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


// Referans: TurnPipeline ekleme noktaları. İsimler senkron; StubStep'ler
// ilerde gerçek modüllere ayrışır.
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
