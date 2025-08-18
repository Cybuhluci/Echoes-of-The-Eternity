using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS.ConsoleSystems
{
    /// <summary>
    /// TardisPower is a subsystem controller for the TARDIS console's power system.
    /// It manages the power circuits of the TARDIS.
    /// </summary>

    public class TardisPower : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }
    }
}