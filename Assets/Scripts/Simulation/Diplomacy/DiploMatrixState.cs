/* =============================================================================
 * File:           Assets/Scripts/Simulation/Diplomacy/DiploMatrixState.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   İki fraksiyon arası: güven, gerginlik, savaş durumu.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Diplomacy
{
    /// <summary> İki fraksiyon arası: güven, gerginlik, savaş durumu. </summary>
    public sealed class DiploMatrixState
    {
        private int[,] _trust;
        private int[,] _tension;
        private bool _initialized;

        public void EnsureSize(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
            if (_initialized && _trust.GetLength(0) == n) return;
            _trust = new int[Math.Max(1, n), Math.Max(1, n)];
            _tension = new int[Math.Max(1, n), Math.Max(1, n)];
            _initialized = true;
        }

        public int Size => _initialized ? _trust.GetLength(0) : 0;

        public void GetPair(int a, int b, out int trust, out int tension)
        {
            if (!_initialized || a < 0 || b < 0 || a >= _trust.GetLength(0) || b >= _trust.GetLength(0))
            {
                trust = 0;
                tension = 0;
                return;
            }
            trust = _trust[a, b];
            tension = _tension[a, b];
        }

        public void SetRelation(int a, int b, int trustDelta, int tension)
        {
            if (!_initialized) throw new InvalidOperationException("EnsureSize");
            if (a < 0 || b < 0 || a >= _trust.GetLength(0) || b >= _trust.GetLength(0)) return;
            _trust[a, b] = trustDelta;
            _tension[a, b] = tension;
        }
    }

    public sealed class Treaty
    {
        public DefId Kind;
        public int FactionA;
        public int FactionB;
        public int RemainingTurns;
    }
}
