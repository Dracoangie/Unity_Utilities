using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;

public class DialogEditor : EditorWindow
{
    private Dialog dialog;
    private ReorderableList textList;
    private ReorderableList optionList;

    [MenuItem("Window/Dialog Editor")]
    public static void ShowWindow()
    {
        GetWindow<DialogEditor>("Dialog Editor");
    }

    private void OnEnable()
    {
        textList = new ReorderableList(new List<string>(), typeof(string), true, true, true, true)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Textos");
            },

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (dialog != null && index < dialog.texts.Count)
                {
                    dialog.texts[index] = EditorGUI.TextField(rect, dialog.texts[index]);
                }
            },

            onAddCallback = (ReorderableList list) =>
            {
                if (dialog != null)
                {
                    dialog.texts.Add(string.Empty);
                }
            },

            onRemoveCallback = (ReorderableList list) =>
            {
                if (dialog != null && list.index >= 0 && list.index < dialog.texts.Count)
                {
                    dialog.texts.RemoveAt(list.index);
                }
            }
        };

        optionList = new ReorderableList(new List<DialogOption>(), typeof(DialogOption), true, true, true, true)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Opciones");
            },

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (dialog != null && index < dialog.options.Count)
                {
                    DialogOption option = dialog.options[index];
                    option.optionText = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight), option.optionText);
                    option.nextDialog = (Dialog)EditorGUI.ObjectField(new Rect(rect.x + rect.width / 2 + 5, rect.y, rect.width / 2 - 5, EditorGUIUtility.singleLineHeight), option.nextDialog, typeof(Dialog), false);
                }
            },

            onAddCallback = (ReorderableList list) =>
            {
                if (dialog != null)
                {
                    dialog.options.Add(new DialogOption());
                }
            },

            onRemoveCallback = (ReorderableList list) =>
            {
                if (dialog != null && list.index >= 0 && list.index < dialog.options.Count)
                {
                    dialog.options.RemoveAt(list.index);
                }
            }
        };
    }

    private void OnGUI()
    {
        GUILayout.Label("Editor de Diálogos", EditorStyles.boldLabel);

        dialog = (Dialog)EditorGUILayout.ObjectField("Dialogo", dialog, typeof(Dialog), false);

        if (dialog != null)
        {
            dialog.speakerName = EditorGUILayout.TextField("Nombre del personaje", dialog.speakerName);

            EditorGUILayout.Space();

            // Actualizar listas para apuntar a los datos actuales
            textList.list = dialog.texts;
            textList.DoLayoutList();

            optionList.list = dialog.options;
            optionList.DoLayoutList();
        }
    }
}