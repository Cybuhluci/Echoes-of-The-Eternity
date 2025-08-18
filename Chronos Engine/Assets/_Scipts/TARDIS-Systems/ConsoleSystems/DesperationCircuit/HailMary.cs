using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS.ConsoleSystems.Desperation
{
    /// <summary>
    /// HailMary is a subsystem controller for the TARDIS console's Hail Mary circuit.
    /// It manages the emergency protocol for desperate situations.
    /// </summary>


    public class HailMary : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}