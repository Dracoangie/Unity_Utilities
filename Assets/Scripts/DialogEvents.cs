using System;
using UnityEngine;

public class DialogEvents : MonoBehaviour
{
    public static event Action<GameObject, DialogManager> OnEventTriggered;

    public static void TriggerEvent(GameObject sender, DialogManager dialogManager)
    {
        if (OnEventTriggered != null)
        {
            OnEventTriggered.Invoke(sender, dialogManager);
        }
    }
}

