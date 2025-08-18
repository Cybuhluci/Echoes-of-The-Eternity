using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS.ConsoleSystems.Desperation
{
    /// <summary>
    /// SelfDestructionSwitch is a subsystem controller for the TARDIS console's self-destruction system.
    /// It manages the activation and deactivation of the self-destruction circuit.
    /// </summary>

    public class SelfDestructionSwitch : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}