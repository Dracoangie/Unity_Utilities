using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public Dialog currentDialog;
    [HideInInspector]
    public int currentTextIndex = 0;
    private DialogUI dialogUI;
    private GameObject playerController;

    void Awake()
    {
        dialogUI = FindObjectOfType<DialogUI>(); // Encontrar automáticamente el DialogUI
        if (dialogUI == null)
        {
            Debug.LogError("DialogUI not found in the scene!");
        }
    }

    public void StartDialogue(Dialog dialog, GameObject sender)
    {
        if (dialogUI == null)
        {
            Debug.LogError("DialogUI is not assigned or found in the scene.");
            return;
        }

        currentDialog = dialog;
        currentTextIndex = 0;
        playerController = sender; // Guardar referencia del objeto que activa el evento
        dialogUI.SetActiveDialogManager(this); // Configurar este DialogManager en el DialogUI
        DisplayText();
        DisablePlayerInput(); // Desactivar entrada del jugador
    }

    public void DisplayText()
    {
        if (currentDialog.texts.Count > 0 && currentTextIndex < currentDialog.texts.Count)
        {
            string speaker = currentDialog.speakerName;
            string text = currentDialog.texts[currentTextIndex];
            dialogUI.UpdateDialogueUI(speaker, text);
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

        DialogOption selectedOption = currentDialog.options[optionIndex];

        dialogUI.ClearOptions(); // Limpiar las opciones actuales

        if (selectedOption.nextDialog != null)
        {
            StartDialogue(selectedOption.nextDialog, playerController); // Pasar la misma referencia del objeto que activa el evento
        }
        else
        {
            Debug.Log("Dialogue ended.");
            dialogUI.CloseDialogueUI();
            EnablePlayerInput(); // Activar entrada del jugador cuando el diálogo termine
        }
    }

    public void DisplayOptions()
    {
        if (currentDialog.options.Count == 0)
        {
            Debug.Log("No more dialogue.");
            dialogUI.CloseDialogueUI();
            EnablePlayerInput(); // Activar entrada del jugador cuando no haya más diálogos
            return;
        }

        dialogUI.ShowOptions(currentDialog.options);
    }

    public void ResetDialogue()
    {
        currentTextIndex = 0;
    }

    private void DisablePlayerInput()
    {
        if (playerController != null)
        {
            playerController.SetActive(false); // Desactivar el controlador del jugador
        }
    }

    public void EnablePlayerInput()
    {
        if (playerController != null)
        {
            playerController.SetActive(true); // Activar el controlador del jugador
        }
    }

    public void TriggerDialogue(GameObject gameObject)
    {
        DialogEvents.TriggerEvent(gameObject, this);
    }
}
