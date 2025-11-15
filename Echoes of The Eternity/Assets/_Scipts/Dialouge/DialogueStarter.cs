using UnityEngine;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour, IInteractable
{
    public DialogueScriptableObject StarterDialogue; // Reference to the starting dialogue

    [Header("Button Events")]
    public UnityEvent RegularInteraction;
    public UnityEvent ModifierInteraction;

    public void RegularInteract()
    {
        //Debug.Log("Regular interaction!");
        RegularInteraction.Invoke();  // Calls whatever methods are assigned in the Inspector
    }

    public void ModifierInteract()
    {
        //Debug.Log("Modifier interaction!");
        ModifierInteraction.Invoke(); // Calls whatever methods are assigned in the Inspector
    }

    public EInteractionType GetInteractionType()
    {
        return EInteractionType.InteractShort;
    }
}
