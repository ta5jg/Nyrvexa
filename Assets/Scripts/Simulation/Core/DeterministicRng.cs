/* =============================================================================
 * File:           Assets/Scripts/Simulation/Core/DeterministicRng.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Oyun simülasyonu için deterministik 64-bit durum: kayıt/yeniden oynatma
 *   eşiği.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;

namespace Nyrvexa.Simulation.Core
{
    /// <summary>
    /// Oyun simülasyonu için deterministik 64-bit durum: kayıt/yeniden oynatma eşiği.
    /// </summary>
    public struct DeterministicRng
    {
        private ulong _s;

        public DeterministicRng(ulong seed)
        {
            _s = seed == 0 ? 0x9E3779B97F4A7C15UL : seed;
        }

        public ulong State => _s;

        public void Reseed(ulong seed)
        {
            _s = seed == 0 ? 0x9E3779B97F4A7C15UL : seed;
        }

        /// <summary> [0, ulong.MaxValue] </summary>
        public ulong NextU64()
        {
            ulong x = _s;
            x ^= x << 7;
            x ^= x >> 9;
            _s = x;
            return x;
        }

        /// <summary> [0, max) — max &gt; 0 </summary>
        public int NextInt(int max)
        {
            if (max <= 0) throw new ArgumentOutOfRangeException(nameof(max));
            return (int)(NextU64() % (ulong)max);
        }

        /// <summary> [min, max) — tamsayı aralık </summary>
        public int NextRange(int min, int max)
        {
            if (min >= max) throw new ArgumentOutOfRangeException();
            return min + NextInt(max - min);
        }

        /// <summary> 0.0 (dahil) – 1.0 (hariç) </summary>
        public double Next01()
        {
            return (NextU64() * (1.0 / ulong.MaxValue));
        }
    }
}
