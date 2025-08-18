using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using UnityEngine;

public abstract class TARDISConsoleController : MonoBehaviour
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
    public virtual string GetButtonFunctionality() { return _isFunctional ? "Working" : "Broken"; }

    [Header("Subsystem Common Settings")]
    [SerializeField] protected bool _isCircuitActive = false; // Is the circuit actively "doing its stuff"?
    public bool IsCircuitActive => _isCircuitActive;

    // isFunctional implies structural integrity (e.g., not broken, not fried).
    // If false, the circuit cannot be activated or perform its function.
    [SerializeField] protected bool _isFunctional = true;
    public bool IsFunctional => _isFunctional;

    // flight event button lockoff management
    [SerializeField] protected bool _inFlightEvent = false; // If true, the button cannot be pressed during flight events.
    public bool InFlightEvent => _inFlightEvent;

    protected abstract void OnCircuitActivated();
    protected abstract void OnCircuitDeactivated();

    public virtual void ActivateCircuit()
    {
        if (_inFlightEvent) // Check if the circuit is in a flight event
        {
            Debug.Log($"{gameObject.name}: Cannot activate circuit. In flight event.");
            return;
        }

        if (!_isFunctional)
        {
            Debug.Log($"{gameObject.name}: Cannot activate circuit. Not functional.");
            return;
        }
        if (!_isCircuitActive)
        {
            _isCircuitActive = true; // Set the active state to true
            OnCircuitActivated(); // Call the specific implementation for "on"
            //Debug.Log($"{gameObject.name}: Circuit Activated.");
        }
    }

    public virtual void DeactivateCircuit()
    {
        if (_inFlightEvent) // Check if the circuit is in a flight event
        {
            Debug.Log($"{gameObject.name}: Cannot activate circuit. In flight event.");
            return;
        }

        if (!_isFunctional)
        {
            Debug.Log($"{gameObject.name}: Cannot deactivate circuit. Not functional.");
            return;
        }
        if (_isCircuitActive)
        {
            _isCircuitActive = false; // Set the active state to false
            OnCircuitDeactivated(); // Call the specific implementation for "off"
            //Debug.Log($"{gameObject.name}: Circuit Deactivated.");
        }
    }

    public virtual void ToggleCircuit()
    {
        if (_isCircuitActive)
        {
            DeactivateCircuit(); // Call the specific implementation for "on"
            //Debug.Log($"{gameObject.name}: Circuit Toggled ON.");
        }
        else
        {
            ActivateCircuit(); // Call the specific implementation for "off"
            //Debug.Log($"{gameObject.name}: Circuit Toggled OFF.");
        }
    }

    public virtual void SetInFlightEvent(bool state)
    {
        _inFlightEvent = state; // Flip the active state
    }
}
