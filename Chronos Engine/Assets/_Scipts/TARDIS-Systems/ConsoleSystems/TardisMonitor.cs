using UnityEngine;
using TMPro;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using Unity.Mathematics;

namespace Luci.TARDIS.ConsoleSystems
{
    /// <summary>
    /// TardisMonitor is a subsystem controller for the TARDIS console's monitor.
    /// It manages the display of various TARDIS operational data.
    /// </summary>


    public class TardisMonitor : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }

        // --- Monitor Update Methods ---

        private void Update()
        {
            TelepathicGUIUpdate(); // Update monitor for fuel and flight percent
        }

        private void Monitor1Update() // fuel and flight percent
        {
            if (consoleManager == null || engineManager == null)
            {
                Debug.LogWarning("Monitor1Update: Missing required manager references. Cannot update monitor.");
                return;
            }
        }

        [Header("Monitor GUI")]
        public GameObject TelepathicGUI;
        public TMP_Text CurrentSpatial;
        public TMP_Text CurrentPocket;
        public TMP_Text DestinationSpatial;
        public TMP_Text DestinationPocket;
        public TMP_Text FlightPercent;
        public TMP_Text CoordinateIncrement;
        public TMP_Text FuelPercent;
        public TMP_Text ThrottleAmount; // This is the throttle amount text, not a reference to Console_Throttle
        public TMP_Text HandbrakeStatus; // This is the handbrake status text, not a reference to Console_Handbrake
        public TMP_Text VortexFlight;
        public TMP_Text Refueller;
        public TMP_Text FastReturn;
        public TMP_Text Wcoordinate;
        public TMP_Text Xcoordinate;
        public TMP_Text Ycoordinate;
        public TMP_Text Zcoordinate;

        private void TelepathicGUIUpdate() // spatial and pocket coordinates
        {
            int4 currentS = engineManager.navigationcom.GetCurrentSpatial();

            int4 destS = engineManager.navigationcom.GetDestinationSpatial();

            if (CurrentSpatial != null) CurrentSpatial.text = $"Current Spatial: {currentS.w}, {currentS.x}, {currentS.y}, {currentS.z}";
            if (DestinationSpatial != null) DestinationSpatial.text = $"Dest Spatial: {destS.w}, {destS.x}, {destS.y}, {destS.z}";

            // --- New coordinate TMP_Texts for W, X, Y, Z ---
            if (Wcoordinate != null)
                Wcoordinate.text = $"Crnt: {currentS.w}\nDest: {destS.w}";
            if (Xcoordinate != null)
                Xcoordinate.text = $"Crnt: {currentS.x}\nDest: {destS.x}";
            if (Ycoordinate != null)
                Ycoordinate.text = $"Crnt: {currentS.y}\nDest: {destS.y}";
            if (Zcoordinate != null)
                Zcoordinate.text = $"Crnt: {currentS.z}\nDest: {destS.z}";

            if (FlightPercent != null)
            {
                float progress = engineManager.navigationcom.GetFlightPercent();
                FlightPercent.text = $"Flight Progress: {(progress * 100):F1}%";
            }

            CoordinateIncrement.text = $"Increment: {consoleManager.deltaCircuit._selectedIncrementAmount}";

            //FuelPercent.text = $"Fuel Left: {engineManager.fluidlinks.FuelPercent:F1}%"; // Assuming FuelLeft is a float percentage

            ThrottleAmount.text = $"Throttle Amount: {consoleManager.spaceTimeThrottle.currentThrottleValue}";
            HandbrakeStatus.text = $"Handbrake Status: {(consoleManager.timeRotorHandbrake.IsCircuitActive ? "Active" : "Inactive")}";

            VortexFlight.text = $"Vortex Flight: {(consoleManager.vortexFlight != null && consoleManager.vortexFlight.IsCircuitActive ? "Active" : "Inactive")}";
            //Refueller.text = $"Refueller: {(consoleManager.refueller != null && consoleManager.refueller.IsEngaged ? "Refuelling" : "Idle")}";
            //FastReturn.text = $"Fast Return: {(consoleManager.fastReturn != null && consoleManager.fastReturn.IsEngaged ? "Active" : "Inactive")}";
        }
    }
}