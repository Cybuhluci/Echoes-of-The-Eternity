using TMPro;
using UnityEngine;

public class ShipPower : ConsoleCore
{
    // --- TARDISSubsystemController Implementations ---

    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
    protected override void OnCircuitActivated() { }
    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
    protected override void OnCircuitDeactivated() { }

    public TMP_Text tempdebugtext;

    void Update()
    {
        if (tempdebugtext != null)
        {
            tempdebugtext.text = $"{this.GetType().Name}\nActive: {_isCircuitActive}";
        }
    }
}
