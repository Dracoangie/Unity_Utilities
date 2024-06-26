using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }

public class Subscriber : MonoBehaviour
{
    public string id; 

    [Tooltip("Response to invoke when Event with GameData is raised.")]
    public CustomGameEvent response;

    private void OnEnable()
    {
        EventManager.RegisterListener(this);
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener(this);
    }

    public void HandleEvent(Component sender, object data, bool remainInSubscribed)
    {
        OnEventRaised(sender, data);
        if (!remainInSubscribed) EventManager.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
