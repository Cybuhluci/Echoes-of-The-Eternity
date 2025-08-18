using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS.ConsoleSystems
{
    /// <summary>
    /// DoorControl is a subsystem controller for the TARDIS console's door control system.
    /// It manages the door control mechanism of the TARDIS.
    /// </summary>

    public class DoorControl : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }

        public bool IsDoorOpen;
        public bool IsDoorLocked;

        public void ToggleDoor()
        {
            if (_inFlightEvent)
            {
                Debug.Log($"{gameObject.name}: Cannot toggle door. In flight event.");
                return;
            }
            if (IsDoorLocked)
            {
                Debug.Log($"{gameObject.name}: Cannot toggle door. Door is locked.");
                return;
            }
            IsDoorOpen = !IsDoorOpen;
            Debug.Log($"{gameObject.name}: Door toggled to {(IsDoorOpen ? "open" : "closed")}.");
        }

        public void ToggleDoorLock()
        {
            if (_inFlightEvent)
            {
                Debug.Log($"{gameObject.name}: Cannot lock door. In flight event.");
                return;
            }
            if (!IsFunctional)
            {
                Debug.Log($"{gameObject.name}: Cannot lock door. Not functional.");
                return;
            }
            IsDoorLocked = !IsDoorLocked;
            Debug.Log($"{gameObject.name}: Door locked.");
        }
    }
}