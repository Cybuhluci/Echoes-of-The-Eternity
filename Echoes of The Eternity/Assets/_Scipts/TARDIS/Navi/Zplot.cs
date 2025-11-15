using TMPro;
using UnityEngine;

public class Zplot : ConsoleCore
{
    // --- TARDISSubsystemController Implementations ---

    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
    protected override void OnCircuitActivated() { }
    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
    protected override void OnCircuitDeactivated() { }

    public DeltaCircuit deltaCircuit;

    public void IncreaseZ()
    {
        if (deltaCircuit.IsCircuitActive)
        {
            deltaCircuit.IncreaseZCoordinate();
        }
    }
    public void DecreaseZ()
    {
        if (deltaCircuit.IsCircuitActive)
        {
            deltaCircuit.DecreaseZCoordinate();
        }
    }

    private void Awake()
    {
        ToggleCircuit();
    }

    public TMP_Text tempdebugtext;

    void Update()
    {
        if (tempdebugtext != null)
        {
            tempdebugtext.text = $"{this.GetType().Name}\nActive: {deltaCircuit.targetCoordinates.z}";
        }
    }
}