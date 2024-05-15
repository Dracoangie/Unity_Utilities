using System.Collections;
using UnityEngine;

public class Publisher : MonoBehaviour
{
    void Update()
    {
        // Disparar el evento cuando se presiona la tecla espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Publisher: Evento disparado");
            EventManager.TriggerEvent();
        }
    }
}
