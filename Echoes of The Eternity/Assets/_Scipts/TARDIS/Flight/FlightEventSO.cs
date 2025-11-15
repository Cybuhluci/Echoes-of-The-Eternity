using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlightEventSO", menuName = "Scriptable Objects/FlightEventSO")]
public class FlightEventSO : ScriptableObject
{
    // this SO will hold a list of actions needed for a single flight event
    // other flight events are separete assets.

    // THIS SCRIPTABLE OBJECT CURRENTLY DOES NOT HANDLE RANDOMIZATION OR TRIGGERING OF EVENTS
    // NOR DOES THIS SCRIPTABLE OBJECT HOLD THE BUTTONS NEEDED TO RESOLVE THE EVENT

    public string eventName;
    public AudioClip eventSound;
    public float eventDuration;
    public bool isCritical; // if true, the event must be resolved to continue flight

    public GameObject consoleButtonParent; // the parent object that holds the buttons for this event
    // since i cant find a way to have the buttons in a list, i have to use findobject using strings for object names.

    public List<string> actionsNeeded; // list of script names to be performed during the event
}
