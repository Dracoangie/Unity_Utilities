using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{
    public string speakerName;
    public List<string> texts = new List<string>();
    public List<DialogOption> options = new List<DialogOption>();
}

[System.Serializable]
public class DialogOption
{
    public string optionText;
    public Dialog nextDialog;
}