using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PooledObjectExample : MonoBehaviour, IPooledObject
{
    public int lifeSpan = 2;
    private float lifeSpanAux;
    private bool isAlife = false;

    public void OnObjectSpawn()
    {
        lifeSpanAux = lifeSpan;
        isAlife = true;
    }

    public void FixedUpdate()
    {
        if (lifeSpanAux <= 0 && isAlife)
        {
            isAlife = false;
            lifeSpanAux = lifeSpan;
            gameObject.SetActive(false);
        }
        lifeSpanAux -= Time.deltaTime;
    }
}
