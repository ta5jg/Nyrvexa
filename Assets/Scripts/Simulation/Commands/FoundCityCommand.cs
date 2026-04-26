using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;
using Nyrvexa.Simulation.World;

namespace Nyrvexa.Simulation.Commands
{
    public sealed class FoundCityCommand : IGameCommand
    {
        public int IssuerFactionIndex { get; }
        public int TileIndex { get; }
        public string CityName { get; }

        public FoundCityCommand(int issuerFaction, int tileIndex, string name)
        {
            IssuerFactionIndex = issuerFaction;
            TileIndex = tileIndex;
            CityName = name ?? "Settlement";
        }

        public void Apply(TurnContext ctx)
        {
            var s = ctx.State;
            if (TileIndex < 0 || TileIndex >= s.World.Tiles.Length) return;
            if (IssuerFactionIndex < 0 || IssuerFactionIndex >= s.Factions.Count) return;
            if (s.World.Tiles[TileIndex].OwnerFactionIndex != -1) return;
            foreach (var kv in s.Cities)
            {
                if (kv.Value.TileIndex == TileIndex) return;
            }
            var id = s.Registry.Create();
            var city = new CityState
            {
                Name = CityName,
                FactionIndex = IssuerFactionIndex,
                TileIndex = TileIndex,
                Population = 1
            };
            city.BuildingIds.Add(new DefId("building.palace"));
            s.Cities[id] = city;
            s.Factions[IssuerFactionIndex].CityIds.Add(id);
            s.World.SetOwner(TileIndex, IssuerFactionIndex);
            ref var t = ref s.World.GetTileRef(TileIndex);
            FogOfWarState.RevealTileForFaction(ref t, IssuerFactionIndex);
            if (city.CityVision > 0)
                FogRevealService.RevealDisk(s.World, TileIndex, city.CityVision, IssuerFactionIndex);
        }

    }
}
