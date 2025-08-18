using UnityEngine;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS
{
    /// <summary>
    /// Main TARDIS control class that manages the overall state and functionality of the TARDIS.
    /// Handles TARDIS state management for ConsoleManager and EngineManager.
    /// </summary>
    /// <remarks>
    /// This class serves as the central hub for TARDIS operations, coordinating between various subsystems
    /// and managing the TARDIS's state throughout its lifecycle.
    /// </remarks>

    public class TARDISMain : MonoBehaviour // just a large state machine full of enums
    {
        [Header("TARDIS Managers")]
        public TARDISConsoleManager consoleManager;
        public TARDISEngineManager engineManager; // Ensure this manager has a public reference to Engine_Stabilisers

        [Header("TARDIS State")]
        public TARDIFlightState currentTARDISState = TARDIFlightState.GroundLanded;

        //void Update()
        //{
        //    HandleTARDISState(); // Manages overall TARDIS state transitions
        //}

        //private void HandleTARDISState()
        //{
        //    // This switch can be expanded for more complex state-specific behaviors
        //    switch (currentTARDISState)
        //    {
        //        case TARDIFlightState.GroundLanded:
        //        case TARDIFlightState.SpaceLanded:
        //        case TARDIFlightState.Dematerializing:
        //        case TARDIFlightState.VortexFlying:
        //        case TARDIFlightState.SpatialFlying:
        //        case TARDIFlightState.DriftFlying:
        //        case TARDIFlightState.Materializing:
        //            break;
        //    }
        //}

        public enum TARDIFlightState
        {
            GroundLanded,
            SpaceLanded,
            Dematerializing,
            VortexFlying,
            SpatialFlying,
            DriftFlying,
            Materializing
        }

        public enum TARDISState
        {
            Healthy,
            Crashed,
            Broken,
            Disrepair
        }
    }
}