/* =============================================================================
 * File:           Assets/Scripts/Simulation/Research/ResearchM1Catalog.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   M1: küçük doğrusal sıra. Etkiler: hazine → keşif +1 görüş; tarım → şehir
 *   başına +1 yiy (Production).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Research
{
    /// <summary> M1: küçük doğrusal sıra. Etkiler: hazine → keşif <c>+1</c> görüş; tarım → şehir başına <c>+1</c> yiy (Production). </summary>
    public static class ResearchM1Catalog
    {
        public static readonly DefId SurveyM1 = new("tech.survey_m1");
        public static readonly DefId AgricultureM1 = new("tech.agriculture_m1");

        public static readonly (DefId Id, int Cost)[] Ordered =
        {
            (SurveyM1, 9),
            (AgricultureM1, 16)
        };

        /// <summary> Katalogda, henüz açılmamış, önkoşulları sağlanmış hedef (M1: tarımdan önce hazine). </summary>
        public static bool IsValidResearchTarget(DefId tech, HashSet<DefId> unlocked)
        {
            if (!tech.IsValid || unlocked.Contains(tech)) return false;
            if (!TryGetCost(tech, out _)) return false;
            if (tech == SurveyM1) return true;
            if (tech == AgricultureM1) return unlocked.Contains(SurveyM1);
            return false;
        }

        public static bool TryGetCost(DefId id, out int cost)
        {
            for (int i = 0; i < Ordered.Length; i++)
            {
                if (Ordered[i].Id == id)
                {
                    cost = Ordered[i].Cost;
                    return true;
                }
            }
            cost = 0;
            return false;
        }

        public static bool TryGetNextLockable(
            HashSet<DefId> unlocked,
            out DefId id,
            out int cost)
        {
            for (int i = 0; i < Ordered.Length; i++)
            {
                if (IsValidResearchTarget(Ordered[i].Id, unlocked))
                {
                    id = Ordered[i].Id;
                    cost = Ordered[i].Cost;
                    return true;
                }
            }
            id = default;
            cost = 0;
            return false;
        }
    }
}
