/* =============================================================================
 * File:           Assets/Scripts/Simulation/Data/UnitCatalogImporter.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   UnitCatalogImporter — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Data
{
    public static class UnitCatalogImporter
    {
        public const int SupportedSchema = 1;

        /// <summary> Başarıda mevcut <paramref name="catalog"/> sözlüğü temizlenir ve dosyadaki tüm tiplerle doldurulur. </summary>
        public static bool TryImport(UnitsFileDto file, UnitArchetypeCatalog catalog, out string? error)
        {
            error = null;
            if (file == null)
            {
                error = "file null";
                return false;
            }
            if (file.schema != SupportedSchema)
            {
                error = $"desteklenmeyen schema: {file.schema} (beklenen: {SupportedSchema})";
                return false;
            }
            if (file.archetypes == null || file.archetypes.Length == 0)
            {
                error = "archetypes boş";
                return false;
            }
            var next = new Dictionary<DefId, UnitArchetypeDef>();
            for (var i = 0; i < file.archetypes.Length; i++)
            {
                var a = file.archetypes[i];
                if (a == null || string.IsNullOrWhiteSpace(a.id)) continue;
                var id = new DefId(a.id.Trim());
                if (!id.IsValid) continue;
                next[id] = new UnitArchetypeDef
                {
                    Id = id,
                    VisionRange = a.visionRange,
                    MovementPerTurn = a.movementPerTurn,
                    CombatPower = a.combatPower,
                    Defense = a.defense
                };
            }
            if (next.Count == 0)
            {
                error = "geçerli archetype yok";
                return false;
            }
            catalog.Archetypes.Clear();
            foreach (var kv in next)
                catalog.Archetypes[kv.Key] = kv.Value;
            return true;
        }
    }
}
