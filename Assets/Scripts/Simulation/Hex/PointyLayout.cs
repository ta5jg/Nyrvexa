using System;

namespace Nyrvexa.Simulation.Hex
{
    /// <summary>
    /// Pointy-topped, odd-r offset: dünya XZ (Y düzlemi) ↔ offset sütun/satır.
    /// redblobgames: pixel/hex, küp yuvarlama.
    /// </summary>
    public static class PointyLayout
    {
        public const float Sqrt3 = 1.732050808f;
        public const float InvSqrt3 = 0.5773502692f; // 1/√3

        /// <summary> (0,0) hücresi orijinde, yerel XZ. </summary>
        public static void ColRowToLocalXZ(float hexSize, int col, int row, out float x, out float z)
        {
            var a = OffsetOddRPointyGrid.OffsetToAxial(col, row);
            x = Sqrt3 * hexSize * (a.Q + a.R * 0.5f);
            z = 1.5f * hexSize * a.R;
        }

        public static void LocalXZToFractAxial(float hexSize, float x, float z, out float fq, out float fr)
        {
            fr = 2f * z / (3f * hexSize);
            fq = (InvSqrt3 * x - 1f / 3f * z) / hexSize;
        }

        public static void RoundFractAxial(float fq, float fr, out int q, out int r)
        {
            // Küp (q, s, r) sözeli: x̂Q = fq, ỹ = -fq - fr, ẑR = fr (x + ỹ + ẑ = 0)
            var fx = fq;
            var fy = -fq - fr;
            var fz = fr;
            var rx = (float)Math.Round(fx, MidpointRounding.AwayFromZero);
            var ry = (float)Math.Round(fy, MidpointRounding.AwayFromZero);
            var rz = (float)Math.Round(fz, MidpointRounding.AwayFromZero);
            var xDiff = Math.Abs(rx - fx);
            var yDiff = Math.Abs(ry - fy);
            var zDiff = Math.Abs(rz - fz);
            if (xDiff > yDiff && xDiff > zDiff) rx = -ry - rz;
            else if (yDiff > zDiff) ry = -rx - rz;
            else rz = -rx - ry;
            q = (int)rx;
            r = (int)rz;
        }

        /// <summary> Yerel (orijin hücrese göre) XZ; harita sınırlarında bir offset hücres. </summary>
        public static bool TryLocalXZToColRow(float hexSize, int mapWidth, int mapHeight, float x, float z, out int col, out int row)
        {
            col = 0;
            row = 0;
            if (mapWidth < 1 || mapHeight < 1 || hexSize <= 0) return false;
            LocalXZToFractAxial(hexSize, x, z, out var fq, out var fr);
            RoundFractAxial(fq, fr, out var q, out var r);
            var a = new AxialCoord(q, r);
            OffsetOddRPointyGrid.AxialToOffset(a, out col, out row);
            if (col < 0 || row < 0 || col >= mapWidth || row >= mapHeight) return false;
            return true;
        }

        public static bool VerifyInversionAllCells(float hexSize, int mapWidth, int mapHeight)
        {
            for (int row = 0; row < mapHeight; row++)
            for (int col = 0; col < mapWidth; col++)
            {
                ColRowToLocalXZ(hexSize, col, row, out var x, out var z);
                if (!TryLocalXZToColRow(hexSize, mapWidth, mapHeight, x, z, out var c, out var r) || c != col || r != row)
                    return false;
            }
            return true;
        }
    }
}
