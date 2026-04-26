/* =============================================================================
 * File:           Assets/Scripts/Simulation/Architecture/SystemBoundaryInterfaces.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   Aynı seed + parametreyle deterministik doldurma (MP sonrası: senkron ağ
 *   otoritesi).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Architecture
{
    /// <summary> Aynı seed + parametreyle deterministik doldurma (MP sonrası: senkron ağ otoritesi). </summary>
    public interface IWorldGenerator
    {
        void GenerateInto(GameStateRoot state, ref DeterministicRng rng, in WorldGenParams p);
    }

    /// <summary> Basit biyom/ boyut/ kaynak gürültüsü — ileride JSON’den yüklenir. </summary>
    public readonly struct WorldGenParams
    {
        public int Width { get; }
        public int Height { get; }
        public int BiomeSpreadOctaves { get; }

        public WorldGenParams(int w, int h, int biomes = 1)
        {
            Width = w;
            Height = h;
            BiomeSpreadOctaves = biomes;
        }
    }

    /// <summary> Oyunsonu: birleşik kazanma değerlendirmesi. </summary>
    public interface IVictoryConditionEvaluator
    {
        /// <summary> -1 = henüz yok, 0+ = kazanan fraksiyon indeksi. </summary>
        int EvaluateVictor(GameStateRoot state, long turnIndex);
    }

    /// <summary> Ticaret rotası/ akış sözleşmesi (Sistem 21). </summary>
    public interface ITradeRouteResolver
    {
        void TickFlow(GameStateRoot state, TurnContext ctx);
    }

    /// <summary> Faset: mod, AI profili, olay seti (DefId) ile eşleme. </summary>
    public interface ISimulationFacet
    {
        DefId FacetId { get; }
    }
}
