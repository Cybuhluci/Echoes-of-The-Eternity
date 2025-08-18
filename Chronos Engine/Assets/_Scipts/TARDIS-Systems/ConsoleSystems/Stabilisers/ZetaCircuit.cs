using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

namespace Luci.TARDIS.ConsoleSystems.Demat
{
    /// <summary>
    /// ZetaCircuit is a subsystem controller for the TARDIS console's Zeta Circuit.
    /// It manages forceful exit from the time vortex.
    /// </summary>

    public class ZetaCircuit : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}