using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialougeManager : MonoBehaviour
{
    public DialogueScriptableObject CurrentDialogue;

    public FirstPersonController fpscontrol;

    [Header("UI Elements")]
    public TMP_Text DialogueText;
    public TMP_Text NameText;
    public Image SpeakerImage;
    public GameObject dialoguebox;
    public Button ChoiceButton1;
    public Button ChoiceButton2;
    public Button ChoiceButton3;
    public PlayerInput playerInput;

    //private bool isWaitingForInput = false;

    public void StartDialogue(DialogueScriptableObject dialogue)
    {
        dialoguebox.SetActive(true);
        CurrentDialogue = dialogue;
        fpscontrol.disableCamera = true;
        fpscontrol.disableMovement = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        HideAllButtons();
        StartCoroutine(ProcessDialogue(dialogue));
    }

    private IEnumerator ProcessDialogue(DialogueScriptableObject dialogue)
    {
        if (dialogue == null)
        {
            Debug.Log("End of Dialogue");
            EndDialogue();
            yield break;
        }

        HideAllButtons();
        foreach (var text in dialogue.DialogueTexts)
        {
            DialogueText.text = text.DialogueText;
            NameText.text = text.SpeakerName;
            SpeakerImage.sprite = text.SpeakerSprite;
            Debug.Log($"{text.SpeakerName}: {text.DialogueText}");

            //isWaitingForInput = true;
            yield return new WaitUntil(() => playerInput.actions["NextText"].WasPressedThisFrame());
            //isWaitingForInput = false;
        }

        if (dialogue.Choices.Count > 0)
        {
            Debug.Log("Choices:");
            for (int i = 0; i < dialogue.Choices.Count; i++)
            {
                Debug.Log($"{i + 1}: {dialogue.Choices[i].ChoiceText}");
                if (i == 0) SetupChoiceButton(ChoiceButton1, dialogue.Choices[i]);
                if (i == 1) SetupChoiceButton(ChoiceButton2, dialogue.Choices[i]);
                if (i == 2) SetupChoiceButton(ChoiceButton3, dialogue.Choices[i]);
            }
        }
        else if (dialogue.NextDialogue != null)
        {
            StartCoroutine(ProcessDialogue(dialogue.NextDialogue));
        }
        else
        {
            Debug.Log("End of Dialogue");
            EndDialogue();
        }
    }

    private void SetupChoiceButton(Button button, DialogueScriptableObject.DialogueChoice choice)
    {
        button.gameObject.SetActive(true);
        button.GetComponentInChildren<TMP_Text>().text = choice.ChoiceText;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => StartCoroutine(ProcessDialogue(choice.NextDialogue)));
    }

    private void HideAllButtons()
    {
        ChoiceButton1.gameObject.SetActive(false);
        ChoiceButton2.gameObject.SetActive(false);
        ChoiceButton3.gameObject.SetActive(false);
    }

    private void EndDialogue()
    {
        dialoguebox.SetActive(false);
        fpscontrol.disableCamera = false;
        fpscontrol.disableMovement = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
