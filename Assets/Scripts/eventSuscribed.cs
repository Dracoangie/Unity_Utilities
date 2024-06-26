using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventSuscribed : MonoBehaviour
{
    public void response(Component sender, object data)
    {
        Debug.Log("response " + data);
    }

    public void escaped()
    {
        Debug.Log("response ");
        EventManager.UnregisterListener(this); // Desuscribir después de manejar el evento
    }
}
