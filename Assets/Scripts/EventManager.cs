using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnEventTriggered;

    // Definimos un m�todo est�tico llamado TriggerEvent
    // Este m�todo se usa para disparar el evento OnEventTriggered
    public static void TriggerEvent()
    {
        // Verificamos si hay alg�n suscriptor al evento OnEventTriggered
        // Si hay suscriptores, invocamos el evento
        if (OnEventTriggered != null)
        {
            OnEventTriggered.Invoke();
        }
    }
}