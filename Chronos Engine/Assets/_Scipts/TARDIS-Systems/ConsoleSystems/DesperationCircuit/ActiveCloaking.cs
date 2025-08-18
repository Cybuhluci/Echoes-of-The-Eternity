using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS.ConsoleSystems.Desperation
{
    /// <summary>
    /// ActiveCloaking is a subsystem controller for the TARDIS console's active cloaking system.
    /// It manages the cloaking functionality of the TARDIS during time vortex travel.
    /// </summary>

    public class ActiveCloaking : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}