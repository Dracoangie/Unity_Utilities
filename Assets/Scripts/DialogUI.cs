using TMPro; 
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogUI : MonoBehaviour
{
    public TMP_Text speakerText; 
    public TMP_Text dialogueText; 
    public GameObject optionButtonPrefab;
    public Transform optionButtonContainer;
    public DialogManager manager;

    public void UpdateDialogueUI(string speaker, string text)
    {
        speakerText.text = speaker;
        dialogueText.text = text;
    }

    public void ShowOptions(List<DialogOption> options)
    {
        ClearOptions();

        for (int i = 0; i < options.Count; i++)
        {
            GameObject buttonObj = Instantiate(optionButtonPrefab, optionButtonContainer);
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {
                buttonText.text = options[i].optionText;

                Button button = buttonObj.GetComponent<Button>();
                if (button != null)
                {
                    int optionIndex = i; // Captura el índice en una variable local para el closure
                    button.onClick.AddListener(() => manager.SelectOption(optionIndex));
                }
                else
                {
                    Debug.LogError("No Button component found on the option button prefab!");
                }
                buttonObj.SetActive(true);
            }
            else
            {
                Debug.LogError("No TMP_Text component found on the option button prefab!");
                Destroy(buttonObj); // Elimina el botón para evitar clutter visual si no está configurado correctamente
            }
        }
    }

    private void ClearOptions()
    {
        foreach (Transform child in optionButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    void Start()
    {
        // Inicia un diálogo para demostración (asumiendo que 'manager' y un objeto Dialog están ya asignados)
        manager.StartDialogue(manager.currentDialog);
        UpdateDialogueUI(manager.currentDialog.speakerName, manager.currentDialog.texts[0]);
    }

    void Update()
    {
        // Este es un disparador simple para avanzar el texto o mostrar opciones
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.DisplayText();
            ShowOptions(manager.currentDialog.options);
        }
    }
}