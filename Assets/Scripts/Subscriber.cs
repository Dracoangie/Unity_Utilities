using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscriber : MonoBehaviour
{
    // suscribimos RespondToEvent al EventManager cuando se activa el objeto
    void OnEnable()
    {
        EventManager.OnEventTriggered += RespondToEvent;
    }

    // desuscribimos RespondToEvent al EventManager cuando se desactiva el objeto
    void OnDisable()
    {
        EventManager.OnEventTriggered -= RespondToEvent;
    }

    // evento que se suscribe
    void RespondToEvent()
    {
        Debug.Log("Subscriber: Evento recibido");
    }
}
