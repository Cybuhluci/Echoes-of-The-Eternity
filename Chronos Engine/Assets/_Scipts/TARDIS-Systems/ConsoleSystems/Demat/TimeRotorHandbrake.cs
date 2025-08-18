using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

namespace Luci.TARDIS.ConsoleSystems.Demat
{
    /// <summary>
    /// TimeRotorHandbrake is a subsystem controller for the TARDIS console's Time Rotor Handbrake.
    /// It manages the tardis handbrake engagement.
    /// </summary>

    public class TimeRotorHandbrake : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() 
        { 
            engineManager.dematCircuit.TryLanding(); 
        }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }

        private void Start()
        {
            ActivateCircuit();
        }
    }
}