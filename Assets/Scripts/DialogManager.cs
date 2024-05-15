using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Dialog currentDialog;
    private int currentTextIndex = 0;

    public void StartDialogue(Dialog dialog)
    {
        currentDialog = dialog;
        currentTextIndex = 0;
        DisplayText();
    }

    public void DisplayText()
    {
        if (currentDialog.texts.Count > 0 && currentTextIndex < currentDialog.texts.Count)
        {
            Debug.Log(currentDialog.speakerName + ": " + currentDialog.texts[currentTextIndex]);
            currentTextIndex++;
        }
        else
        {
            DisplayOptions();
        }
    }

    public void SelectOption(int optionIndex)
    {
        if (optionIndex < 0 || optionIndex >= currentDialog.options.Count)
            return;

        if (currentDialog.options[optionIndex].nextDialog != null)
        {
            StartDialogue(currentDialog.options[optionIndex].nextDialog);
        }
        else
        {
            Debug.Log("Dialogue ended.");
        }
    }

    private void DisplayOptions()
    {
        if (currentDialog.options.Count == 0)
        {
            Debug.Log("No more dialogue.");
            return;
        }
        
        Debug.Log("Choose an option:");
        for (int i = 0; i < currentDialog.options.Count; i++)
        {
            Debug.Log($"{i + 1}: {currentDialog.options[i].optionText}");
        }
    }
}