using TARDIS.Core;
using TMPro;
using UnityEngine;

public class Throttle : ConsoleCore
{
    // --- TARDISSubsystemController Implementations ---

    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
    protected override void OnCircuitActivated() { flightCore.ThrottleUpdate(true); }
    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
    protected override void OnCircuitDeactivated() { flightCore.ThrottleUpdate(false); }

    public FlightCore flightCore;

    public TMP_Text tempdebugtext;

    void Update()
    {
        if (tempdebugtext != null)
        {
            tempdebugtext.text = $"{this.GetType().Name}\nActive: {_isCircuitActive}";
        }
    }
}