using System.Collections;
using UnityEngine;

public class Publisher : MonoBehaviour
{
    public int mesaje;


    void Update()
    {
        // Disparar el evento cuando se presiona la tecla espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Publisher: Space disparado");
            EventManager.Raise("space", mesaje);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Publisher: Escape disparado");
            EventManager.Raise("escape", mesaje);
        }
    }
}
