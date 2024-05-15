using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnEventTriggered;

    // Definimos un método estático llamado TriggerEvent
    // Este método se usa para disparar el evento OnEventTriggered
    public static void TriggerEvent()
    {
        // Verificamos si hay algún suscriptor al evento OnEventTriggered
        // Si hay suscriptores, invocamos el evento
        if (OnEventTriggered != null)
        {
            OnEventTriggered.Invoke();
        }
    }
}