using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUI : MonoBehaviour
{
    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public GameObject optionButtonPrefab;
    public Transform optionButtonContainer;
    private DialogManager activeDialogManager;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public void SetActiveDialogManager(DialogManager dialogManager)
    {
        activeDialogManager = dialogManager;
    }

    public void UpdateDialogueUI(string speaker, string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        speakerText.text = speaker;
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }

    public void CompleteTyping(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogueText.text = text;
        isTyping = false;
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
                    button.onClick.AddListener(() => activeDialogManager.SelectOption(optionIndex));
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

    public void ClearOptions()
    {
        foreach (Transform child in optionButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void CloseDialogueUI()
    {
        gameObject.SetActive(false);
        activeDialogManager.ResetDialogue();
        activeDialogManager.EnablePlayerInput();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Este es un disparador simple para avanzar el texto o mostrar opciones
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                CompleteTyping(activeDialogManager.currentDialog.texts[activeDialogManager.currentTextIndex - 1]);
            }
            else
            {
                if (activeDialogManager.currentTextIndex < activeDialogManager.currentDialog.texts.Count)
                {
                    activeDialogManager.DisplayText();
                }
                else
                {
                    activeDialogManager.DisplayOptions();
                }
            }
        }
    }

    void OnEnable()
    {
        DialogEvents.OnEventTriggered += RespondToEvent;
    }

    void RespondToEvent(GameObject sender, DialogManager dialogManager)
    {
        Debug.Log("Subscriber: Evento recibido");
        gameObject.SetActive(true);
        dialogManager.StartDialogue(dialogManager.currentDialog, sender);
    }
}
