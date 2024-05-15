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
                    // Calcular la altura del texto
                    GUIStyle style = EditorStyles.textArea;
                    float height = style.CalcHeight(new GUIContent(dialog.texts[index]), rect.width);
                    rect.height = height;
                    dialog.texts[index] = EditorGUI.TextArea(rect, dialog.texts[index], style);
                }
            },

            elementHeightCallback = (int index) =>
            {
                // Configurar altura del elemento seg�n el contenido del texto
                if (dialog != null && index < dialog.texts.Count)
                {
                    GUIStyle style = EditorStyles.textArea;
                    float height = style.CalcHeight(new GUIContent(dialog.texts[index]), EditorGUIUtility.currentViewWidth);
                    return height + EditorGUIUtility.standardVerticalSpacing;
                }
                return EditorGUIUtility.singleLineHeight;
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
        GUILayout.Label("Editor de Di�logos", EditorStyles.boldLabel);

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