using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS.ConsoleSystems
{
    /// <summary>
    /// ExteriorFacing is a subsystem controller for the TARDIS console's Exterior Facing system.
    /// It manages the exterior facing operations of the TARDIS.
    /// </summary>

    public class ExteriorFacing : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}

// Precise Controls for Triangulation: Using a precise, slow rotation for a triangulation puzzle is a perfect fit.
// It makes the player feel like a true pilot, carefully aligning the TARDIS's sensors with a delicate signal.
// The difficulty comes from fine-tuning, not speed.

//90-Degree Turns for Flight Events: This is brilliant for quick-time events or urgent maneuvers.
//A fast, decisive button press for a 90-degree turn would feel powerful and urgent, perfect for dramatic escapes,
//dodging debris in the vortex, or reorienting the TARDIS to avoid a sudden hazard.