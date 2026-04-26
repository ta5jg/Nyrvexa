namespace Nyrvexa.Simulation.World
{
    /// <summary> M1: kara keşifçi (unit.scout) hedefi — biyom 4 (kıyı/su) geçit yok. </summary>
    public static class TilePassage
    {
        public static bool IsBlockedScoutEnter(ushort biomeId) => biomeId == 4;
    }
}
