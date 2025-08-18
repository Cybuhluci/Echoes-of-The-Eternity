using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

public abstract class TARDISEngineController : MonoBehaviour
{
    [Header("Dependencies")]
    [HideInInspector] protected TARDISMain tardisMain;
    [HideInInspector] protected TARDISConsoleManager consoleManager;
    [HideInInspector] protected TARDISEngineManager engineManager;

    private void Awake()
    {
        tardisMain ??= FindAnyObjectByType<TARDISMain>();
        consoleManager ??= tardisMain?.consoleManager;
        engineManager ??= tardisMain?.engineManager;
    }
    private void Start()
    {
        tardisMain ??= FindAnyObjectByType<TARDISMain>();
        consoleManager ??= tardisMain?.consoleManager;
        engineManager ??= tardisMain?.engineManager;
    }

    public virtual string GetButtonState() { return _isCircuitActive ? "On" : "Off"; }
    public virtual string GetButtonFunctionality() { return _durability > 0 ? "Working" : "Broken"; }

    [Header("Subsystem Common Settings")]

    // circuit state management
    [SerializeField] protected bool _isCircuitActive = true; // true means the Circuit is running. false means the Circuit is stopped (off).
    public bool IsCircuitActive => _isCircuitActive;

    // functionality management 
    [SerializeField] protected bool _isFunctional => _durability > 0; // If durability is greater than 0, the circuit is functional. If 0 or less, it is not functional.
    public bool IsFunctional => _isFunctional; // Read-only property to access functionality state

    // damage/durability management
    [SerializeField, Range(0, 1250)] protected int _durability = 1250; // Default durability value, can be adjusted in the inspector - 0 durability means broken
    public int Durability => _durability; // Read-only property to access durability

    public virtual void SetDurability(int value)
    {
        _durability = Mathf.Clamp(value, 0, 1250);
    }

    public virtual void MinusDurability(int value)
    {
        _durability = Mathf.Clamp(_durability - value, 0, 1250);
    }

    public virtual void PlusDurability(int value)
    {
        _durability = Mathf.Clamp(_durability + value, 0, 1250);
    }

    public virtual void OnTARDISDematerialize() { }
    public virtual void OnTARDISMaterialize() { }
    public virtual void OnTARDISFlightStart() { }
    public virtual void OnTARDISPowerOn() { }
    public virtual void OnTARDISPowerOff() { }

    public bool IsFullyOperational() // getting a false from this means that the circuit is entering a critial state and needs repairs.
    {
        return _isCircuitActive && _durability > 128;
    }
}

/*        
void cheese() // cheese, all over
{
    OnTARDISDematerialize(); // cheese, all over
    OnTARDISMaterialize(); // cheese, all over
    OnTARDISFlightStart(); // cheese, all over
    OnTARDISPowerOn(); // cheese, all over
    OnTARDISPowerOff(); // cheese, all over
}
*/