using TARDIS.Core;
using Unity.Mathematics;
using UnityEngine;

public class DeltaCircuit : ConsoleCore
{
    // --- TARDISSubsystemController Implementations ---

    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
    protected override void OnCircuitActivated() { }
    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
    protected override void OnCircuitDeactivated() { }

    // --- Coordinate Adjustment Methods for Physical Buttons ---

    public NaviCore naviCore; // Reference to the NaviCore to update target coordinates

    [Header("Current Destination")]
    public int4 targetCoordinates;
    public int sectorCoordinates; // 1-8, mimics a 2x2x2 vector3
    public Vector3 majorCoordinates; // 250x250x250
    public Vector3 minorCoordinates; // 500x500x500

    [Header("Adjustment Settings")]
    public int[] incrementAmounts = { 1, 10, 50, 100 }; // The different increment sizes
    private int _currentIncrementIndex = 0;
    public int _selectedIncrementAmount = 1; // Starts at 1

    // NEW: Flag to store the current adjustment direction (true = positive, false = negative)
    public bool isIncrementDirectionPositive = true;

    // We still keep this enum here incase the new system needs it.
    private enum SelectedCoordinate { None, ClusterPlot, GalaxyPlot, PlanetPlot, PocketPlot }

    private void Awake()
    {
        ToggleCircuit();
    }

    public void IncreaseWCoordinate() { AdjustSectorCoordinate(1); }
    public void DecreaseWCoordinate() { AdjustSectorCoordinate(-1); }
    public void IncreaseXCoordinate() { AdjustSpatialCoordinate(0, 1); }
    public void DecreaseXCoordinate() { AdjustSpatialCoordinate(0, -1); }
    public void IncreaseYCoordinate() { AdjustSpatialCoordinate(1, 1); }
    public void DecreaseYCoordinate() { AdjustSpatialCoordinate(1, -1); }
    public void IncreaseZCoordinate() { AdjustSpatialCoordinate(2, 1); }
    public void DecreaseZCoordinate() { AdjustSpatialCoordinate(2, -1); }

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
        UpdateNavcom();
    }

    private void AdjustSectorCoordinate(int direction)
    {
        int adjustment = direction * (isIncrementDirectionPositive ? 1 : -1);
        targetCoordinates.w += adjustment;
        targetCoordinates.w = Mathf.Clamp(targetCoordinates.w, 1, 8);

        UpdateNavcom();
    }

    public void UpdateNavcom()
    {
        naviCore.UpdateTargetCoords(targetCoordinates); // Assuming x is the primary coordinate for NaviCore
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