using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

namespace Luci.TARDIS.EngineSystems
{
    /// <summary>
    /// FluidLinks is a subsystem controller for the TARDIS console's fluid links.
    /// It manages artron energy flow and amount.
    /// </summary>

    public class FluidLinks : TARDISEngineController
    {
        [Header("Fluid Links Status")]
        [SerializeField] private float artronEnergyFlow = 0f; // Current flow of artron energy
        [SerializeField] private float artronEnergyAmount = 0f; // Current amount of artron energy
        [SerializeField] private float BaseArtronEnergyAmount = 512f; // Maximum flow of artron energy
        [SerializeField] private float maxArtronEnergyPossible = 8192f; // Maximum artron energy capacity 
        [SerializeField] private float CurrentArtronEnergyAmount = 512f; // Current artron energy amount
        [SerializeField] public float FuelPercent = 0f; // Percentage of artron energy available
    }
}