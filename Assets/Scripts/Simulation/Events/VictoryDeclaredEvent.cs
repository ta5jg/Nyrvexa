namespace Nyrvexa.Simulation.Events
{
    public sealed class VictoryDeclaredEvent : IGameEvent
    {
        public string Channel => "Victory";
        public int VictorFactionIndex { get; }
        public long TurnIndex;
        public long VictorScore { get; }

        public VictoryDeclaredEvent(int victorFactionIndex, long atTurn, long score)
        {
            VictorFactionIndex = victorFactionIndex;
            TurnIndex = atTurn;
            VictorScore = score;
        }
    }
}
