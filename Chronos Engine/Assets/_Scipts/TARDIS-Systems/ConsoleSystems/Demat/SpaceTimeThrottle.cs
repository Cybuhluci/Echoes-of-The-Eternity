using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

namespace Luci.TARDIS.ConsoleSystems.Demat
{
    /// <summary>
    /// SpaceTimeThrottle is a subsystem controller for the TARDIS console's Space-Time Throttle.
    /// It manages the throttle amount.
    /// </summary>

    public class SpaceTimeThrottle : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() 
        {
            engineManager.dematCircuit.TryTakeoff();
        }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated()
        {
            SetThrottle(0); // Reset throttle to 0 when deactivated
        }

        // --- Throttle Specific Methods ---

        [Header("Throttle Settings")]
        [Range(0, 11)] public int currentThrottleValue = 0; // 0 = off, 11 = full throttle
        public float throttleChangeSpeed = 0.1f; // How fast throttle increases/decreases flight speed

        // Public property to get the current throttle value
        public float CurrentThrottleValue => currentThrottleValue;

        void Awake()
        {
            // Ensure the throttle starts at zero.
            // _isCircuitActive from the base class defaults to false, which is correct for an 'inactive' throttle.
            SetThrottle(0); // Ensure value is 0
            Debug.Log($"Console_Throttle: Initial state - {GetButtonState()}, Value: {currentThrottleValue}");
        }

        // Increases the throttle value by 1
        public void IncreaseThrottle()
        {
            if (tardisMain.currentTARDISState == TARDISMain.TARDIFlightState.Dematerializing || tardisMain.currentTARDISState == TARDISMain.TARDIFlightState.Materializing)
            {
                Debug.Log($"Please be patient when flying.");
                return;
            }

            if (_inFlightEvent) // Check if the circuit is in a flight event
            {
                Debug.Log($"{gameObject.name}: Cannot activate circuit. In flight event.");
                return;
            }

            if (!_isFunctional)
            {
                Debug.Log("Throttle: Cannot increase, not functional.");
                return;
            }

            // If throttle is off and circuit is not active, activate it first
            if (!_isCircuitActive && currentThrottleValue == 0)
            {
                ToggleCircuit();
            }

            currentThrottleValue = Mathf.Min(currentThrottleValue + 1, 11);
            Debug.Log($"Console_Throttle: INCREASED to {currentThrottleValue}");
        }

        // Decreases the throttle value by 1
        public void DecreaseThrottle()
        {
            if (tardisMain.currentTARDISState == TARDISMain.TARDIFlightState.Dematerializing || tardisMain.currentTARDISState == TARDISMain.TARDIFlightState.Materializing)
            {
                Debug.Log($"Please be patient when flying.");
                return;
            }

            if (_inFlightEvent) // Check if the circuit is in a flight event
            {
                Debug.Log($"{gameObject.name}: Cannot activate circuit. In flight event.");
                return;
            }

            if (!_isCircuitActive || !_isFunctional)
            {
                Debug.Log("Throttle: Cannot decrease, control is disengaged or not functional.");
                return;
            }

            currentThrottleValue = Mathf.Max(currentThrottleValue - 1, 0);
            Debug.Log($"Console_Throttle: DECREASED to {currentThrottleValue}");

            // If throttle reaches 0, deactivate the circuit
            if (currentThrottleValue == 0)
            {
                ToggleCircuit();
            }
        }

        // Sets the throttle to a specific value (e.g., for quick presets)
        public void SetThrottle(int value) // Changed parameter to int for consistency with currentThrottleValue
        {
            if (!_isCircuitActive && value != 0) // Cannot set non-zero if control is disengaged
            {
                Debug.Log("Throttle: Cannot set non-zero value, control is disengaged.");
                return;
            }
            if (!_isFunctional) // Cannot set if not functional
            {
                Debug.Log("Throttle: Cannot set, not functional.");
                return;
            }

            currentThrottleValue = Mathf.Clamp(value, 0, 11);
            Debug.Log($"Console_Throttle: SET to {currentThrottleValue}");
        }
    }
}