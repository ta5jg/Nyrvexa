using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.AI
{
    /// <summary> Savaş / keşif alt oturumunda eylem seçimi (GOAP / yerel değerlendirme). </summary>
    public interface ITacticalController
    {
        void IssueOrders(EntityId groupId, GameStateRoot state, TurnContext ctx);
    }
}
