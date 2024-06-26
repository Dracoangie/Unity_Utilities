using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventSuscribed : MonoBehaviour
{
    public void Response(Component sender, object data)
    {
        Debug.Log("response " + data);
    }

    public void escaped()
    {
        Debug.Log("response ");
    }
}
