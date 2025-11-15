using TMPro;
using UnityEngine;

public class PlotterIncrement : ConsoleCore
{
    // --- TARDISSubsystemController Implementations ---

    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
    protected override void OnCircuitActivated() { }
    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
    protected override void OnCircuitDeactivated() { }

    public DeltaCircuit deltaCircuit;

    public void IncreaseAmount()
    {
        if (deltaCircuit.IsCircuitActive)
        {
            deltaCircuit.CycleIncrementAmountUp();
        }
    }

    public void DecreaseAmount()
    {
        if (deltaCircuit.IsCircuitActive)
        {
            deltaCircuit.CycleIncrementAmountDown();
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
            tempdebugtext.text = $"{this.GetType().Name}\nActive: {deltaCircuit._selectedIncrementAmount}";
        }
    }
}