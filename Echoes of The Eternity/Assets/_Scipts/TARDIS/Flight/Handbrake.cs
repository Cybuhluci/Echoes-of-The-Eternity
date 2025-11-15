using TARDIS.Core;
using TMPro;
using UnityEngine;

public class Handbrake : ConsoleCore
{
    // --- TARDISSubsystemController Implementations ---

    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes TRUE.
    protected override void OnCircuitActivated() { flightCore.HandbrakeUpdate(true); }
    // This method is called by the base ToggleCircuit() when _isCircuitActive becomes FALSE.
    protected override void OnCircuitDeactivated() { flightCore.HandbrakeUpdate(false); }

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