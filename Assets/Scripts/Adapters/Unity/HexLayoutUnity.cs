// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.Hex;
using UnityEngine;

namespace Nyrvexa.Adapters
{
    /// <summary> Simülasyondaki odd-r pointy ızgara → dünya (XZ, yatay düzlem), `PointyLayout` ile aynı matematik. </summary>
    public static class HexLayoutUnity
    {
        public const float Sqrt3 = PointyLayout.Sqrt3;

        /// <param name="hexRadius">Dairesel yarıçap (merkez → köşe), redblob “size” (pointy).</param>
        public static Vector3 AxialToWorldPointy(float hexRadius, int q, int r, float y = 0f)
        {
            var x = hexRadius * Sqrt3 * (q + r * 0.5f);
            var z = hexRadius * 1.5f * r;
            return new Vector3(x, y, z);
        }

        public static Vector3 ColRowToWorldPointy(float hexRadius, int col, int row, float y = 0f)
        {
            PointyLayout.ColRowToLocalXZ(hexRadius, col, row, out var x, out var z);
            return new Vector3(x, y, z);
        }

        public static bool TryWorldToColRow(
            float hexRadius, int mapWidth, int mapHeight,
            in Vector3 world, in Vector3 gridRoot, out int col, out int row)
        {
            var lx = world.x - gridRoot.x;
            var lz = world.z - gridRoot.z;
            return PointyLayout.TryLocalXZToColRow(hexRadius, mapWidth, mapHeight, lx, lz, out col, out row);
        }

        /// <summary> Tüm hücre merkezlerinin XZ sınırlayıcı kutusunu (merkez + yarım genişlik/derinlik) hesaplar. </summary>
        public static void GetMapAxisAlignedBounds(
            float hexRadius, int w, int h, in Vector3 originOffset, float y,
            out Vector3 center, out float halfExtentX, out float halfExtentZ)
        {
            if (w < 1 || h < 1)
            {
                center = originOffset;
                halfExtentX = halfExtentZ = 1f;
                return;
            }
            float minX = float.MaxValue, maxX = float.MinValue, minZ = float.MaxValue, maxZ = float.MinValue;
            for (int row = 0; row < h; row++)
            for (int col = 0; col < w; col++)
            {
                var p = ColRowToWorldPointy(hexRadius, col, row, 0f);
                var wx = p.x + originOffset.x;
                var wz = p.z + originOffset.z;
                if (wx < minX) minX = wx;
                if (wx > maxX) maxX = wx;
                if (wz < minZ) minZ = wz;
                if (wz > maxZ) maxZ = wz;
            }
            center = new Vector3((minX + maxX) * 0.5f, y + originOffset.y, (minZ + maxZ) * 0.5f);
            var pad = hexRadius * 0.5f;
            halfExtentX = (maxX - minX) * 0.5f + pad;
            halfExtentZ = (maxZ - minZ) * 0.5f + pad;
        }
    }
}
