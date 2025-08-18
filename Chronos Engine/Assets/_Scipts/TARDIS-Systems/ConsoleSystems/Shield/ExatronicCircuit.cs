using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

namespace Luci.TARDIS.ConsoleSystems.Shield
{
    /// <summary>
    /// ExatronicCircuit is a subsystem controller for the TARDIS console's Exatronic circuit.
    /// It manages the engagement state of the circuit and provides methods to toggle and check its status.
    /// </summary>

    public class ExatronicCircuit : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}