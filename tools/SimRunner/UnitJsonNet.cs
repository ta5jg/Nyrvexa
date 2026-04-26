/* =============================================================================
 * File:           tools/SimRunner/UnitJsonNet.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   SimRunner: ile JSON; Unity aynı DTO'yu JsonUtility ile yükler.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using System.IO;
using System.Text.Json;
using Nyrvexa.Simulation.Data;

namespace Nyrvexa.SimRunner;

/// <summary> SimRunner: <see cref="System.Text.Json"/> ile JSON; Unity aynı DTO'yu <c>JsonUtility</c> ile yükler. </summary>
internal static class UnitJsonNet
{
    private static readonly JsonSerializerOptions Opts = new()
    {
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
        IncludeFields = true
    };

    public static string DefaultUnitsPath()
    {
        // bin/{Configuration}/net8.0/ → depo kökü (5 seviye yukarı)
        var baseDir = AppContext.BaseDirectory;
        return Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "..", "Assets", "StreamingAssets", "Defs", "units.example.json"));
    }

    public static bool TryReadFile(string path, out UnitsFileDto? dto, out string? error)
    {
        dto = null;
        try
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                error = path is null or "" ? "yol boş" : "dosya yok: " + path;
                return false;
            }
            var text = File.ReadAllText(path);
            dto = JsonSerializer.Deserialize<UnitsFileDto>(text, Opts);
            if (dto == null)
            {
                error = "çözülüm null";
                return false;
            }
            error = null;
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }
}
