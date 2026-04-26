using System;
using System.IO;
using Nyrvexa.Simulation.Data;
using UnityEngine;

namespace Nyrvexa.Adapters
{
    /// <summary> Editor / PC: <c>StreamingAssets/Defs</c> altındaki JSON → <see cref="UnitsFileDto"/>. </summary>
    public static class UnitCatalogFile
    {
        public static bool TryLoadFromStreamingData(string fileName, out UnitsFileDto dto, out string error)
        {
            dto = null;
            error = null;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                error = "fileName boş";
                return false;
            }
            var path = Path.Combine(Application.dataPath, "StreamingAssets", "Defs", fileName.Trim());
            try
            {
                if (!File.Exists(path))
                {
                    error = "yok: " + path;
                    return false;
                }
                var json = File.ReadAllText(path);
                dto = JsonUtility.FromJson<UnitsFileDto>(json);
                if (dto == null)
                {
                    error = "JsonUtility null DTO";
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }
    }
}
