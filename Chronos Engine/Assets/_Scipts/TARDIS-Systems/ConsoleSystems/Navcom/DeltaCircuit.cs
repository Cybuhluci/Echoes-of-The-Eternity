using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using Unity.Mathematics;
using UnityEngine;

namespace Luci.TARDIS.ConsoleSystems.Navcom
{   
    /// <summary>
    /// DeltaCircuit is a subsystem controller for the TARDIS console's Delta circuit.
    /// It manages the TARDIS coordinates
    /// </summary>

    public class DeltaCircuit : TARDISConsoleController
    {
        // --- TARDISSubsystemController Implementations ---

        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
        protected override void OnCircuitActivated() { }
        // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
        protected override void OnCircuitDeactivated() { }

        // --- Coordinate Adjustment Methods for Physical Buttons ---

        [Header("Current Destination")]
        public int4 targetCoordinates;

        [Header("Adjustment Settings")]
        public int[] incrementAmounts = { 1, 10, 50, 100 }; // The different increment sizes
        private int _currentIncrementIndex = 0;
        public int _selectedIncrementAmount = 1; // Starts at 1

        // NEW: Flag to store the current adjustment direction (true = positive, false = negative)
        public bool isIncrementDirectionPositive = true;

        // We still keep this enum here incase the new system needs it.
        private enum SelectedCoordinate { None, ClusterPlot, GalaxyPlot, PlanetPlot, PocketPlot }
        private SelectedCoordinate _lastAdjustedCoordinate = SelectedCoordinate.None; // Track last for status display

        private void Awake()
        {
            ToggleCircuit();
        }

        public void increaseWCoordinate() { AdjustPocketCoordinate(1); }
        public void decreaseWCoordinate() { AdjustPocketCoordinate(-1); }
        public void increaseXCoordinate() { AdjustSpatialCoordinate(0, 1); }
        public void decreaseXCoordinate() { AdjustSpatialCoordinate(0, -1); }
        public void increaseYCoordinate() { AdjustSpatialCoordinate(1, 1); }
        public void decreaseYCoordinate() { AdjustSpatialCoordinate(1, -1); }
        public void increaseZCoordinate() { AdjustSpatialCoordinate(2, 1); }
        public void decreaseZCoordinate() { AdjustSpatialCoordinate(2, -1); }

        // Helper methods
        private void AdjustSpatialCoordinate(int axis, int direction)
        {
            int adjustment = _selectedIncrementAmount * direction * (isIncrementDirectionPositive ? 1 : -1);
            switch (axis)
            {
                case 0: targetCoordinates.x += adjustment; break;
                case 1: targetCoordinates.y += adjustment; break;
                case 2: targetCoordinates.z += adjustment; break;
            }
        }

        private void AdjustPocketCoordinate(int direction)
        {
            int adjustment = direction * (isIncrementDirectionPositive ? 1 : -1);
            targetCoordinates.w += adjustment;
            targetCoordinates.w = Mathf.Clamp(targetCoordinates.w, 1, 9);
        }

        public void UpdateNavcom()
        {
            engineManager.navigationcom.SetDestination(targetCoordinates);
        }

        // Optionally, add methods to cycle increment and toggle direction for physical controls
        public void CycleIncrementAmountUp()
        {
            _currentIncrementIndex = (_currentIncrementIndex + 1) % incrementAmounts.Length;
            _selectedIncrementAmount = incrementAmounts[_currentIncrementIndex];
        }

        public void CycleIncrementAmountDown()
        {
            _currentIncrementIndex = (_currentIncrementIndex - 1 + incrementAmounts.Length) % incrementAmounts.Length;
            _selectedIncrementAmount = incrementAmounts[_currentIncrementIndex];
        }
    }
}