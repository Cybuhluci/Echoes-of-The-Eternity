using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

namespace Luci.TARDIS.ConsoleSystems.Navcom
{
    /// <summary>
    /// EpsilonCircuit is a subsystem controller for the TARDIS console's Epsilon circuit.
    /// It manages flight speed based on the TARDIS's current state and piloting skills.
    /// </summary>

    public class EpsilonCircuit : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}