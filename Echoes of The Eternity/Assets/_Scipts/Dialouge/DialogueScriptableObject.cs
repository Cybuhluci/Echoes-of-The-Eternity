using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Scriptable Object")]
public class DialogueScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class DialogueEntry
    {
        public string SpeakerName; // Name of the person speaking
        [TextArea]
        public string DialogueText; // The dialogue text
        public Sprite SpeakerSprite; // Sprite for the person speaking
    }

    public List<DialogueEntry> DialogueTexts; // Array of dialogue entries

    [System.Serializable]
    public class DialogueChoice
    {
        public string ChoiceText; // Text for the choice
        public DialogueScriptableObject NextDialogue; // Reference to the next dialogue
    }

    public List<DialogueChoice> Choices; // Array of choices

    public DialogueScriptableObject NextDialogue; // Reference to the next dialogue without a choice

    private void OnEnable()
    {
        if (DialogueTexts == null)
        {
            DialogueTexts = new List<DialogueEntry>();
        }

        if (Choices == null)
        {
            Choices = new List<DialogueChoice>();
        }
    }
}