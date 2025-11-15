using TMPro;
using UnityEngine;

public class Yplot : ConsoleCore
{
    // --- TARDISSubsystemController Implementations ---

    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
    protected override void OnCircuitActivated() { }
    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
    protected override void OnCircuitDeactivated() { }

    public DeltaCircuit deltaCircuit;

    public void IncreaseY()
    {
        if (deltaCircuit.IsCircuitActive)
        {
            deltaCircuit.IncreaseYCoordinate();
        }
    }

    public void DecreaseY()
    {
        if (deltaCircuit.IsCircuitActive)
        {
            deltaCircuit.DecreaseYCoordinate();
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
            tempdebugtext.text = $"{this.GetType().Name}\nActive: {deltaCircuit.targetCoordinates.y}";
        }
    }
}