/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/IPipelineStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   IPipelineStep — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.Loop
{
    public interface IPipelineStep
    {
        string Name { get; }
        void Execute(TurnContext ctx);
    }
}
