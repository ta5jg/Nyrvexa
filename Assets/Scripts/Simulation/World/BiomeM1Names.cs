namespace Nyrvexa.Simulation.World
{
    /// <summary> M1: prosedürel biyom 1..6 (0 = ayrılmamış) kısa etiket — HUD/araç için. </summary>
    public static class BiomeM1Names
    {
        private static readonly string[] k1To6 = { "Ova", "Koru", "Tepe", "Kıyı", "Bozkır", "Tundra" };

        public static string TryGet(ushort biomeId)
        {
            if (biomeId == 0) return "—";
            if (biomeId < 1 || biomeId > 6) return "?" + biomeId;
            return k1To6[biomeId - 1];
        }
    }
}
