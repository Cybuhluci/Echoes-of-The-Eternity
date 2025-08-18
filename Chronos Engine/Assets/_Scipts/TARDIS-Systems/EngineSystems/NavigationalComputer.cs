using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using Unity.Mathematics;
using UnityEngine;

namespace Luci.TARDIS.EngineSystems
{
    /// <summary>
    /// NavigationalComputer is a subsystem controller for the TARDIS console's navigational computer.
    /// It manages location and destination settings for travel.
    /// </summary>

    public class NavigationalComputer : TARDISEngineController
    {
        // --- Navigational Computer Methods ---
        [Header("Navigational Computer Status")]
        [SerializeField] private int4 currentLocation; // Current location of the TARDIS
        [SerializeField] private int4 destination; // Destination set for travel

        [SerializeField] private float flightPercent; // Percentage of flight completion

        public void SetDestination(int4 newDestination)
        {
            destination = newDestination;
        }

        public int4 GetDestinationSpatial()
        {
            return destination;
        }
        public int4 GetCurrentSpatial()
        {
            return currentLocation;
        }

        public float GetFlightPercent()
        {
            return flightPercent;
        }
    }
}