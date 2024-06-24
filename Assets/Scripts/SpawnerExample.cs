using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerExample : MonoBehaviour
{
    ObjectPoolManager poolManager;
    public float spawnTime = 1;
    private float spawnTimeAux = 0;

    private void Start()
    {
        poolManager = ObjectPoolManager.instance;
    }

    void FixedUpdate()
    {
        if (spawnTimeAux <= 0)
        {
            spawnTimeAux = spawnTime;
            poolManager.SpawnFromPool("Square", transform.position, transform.rotation);
        }
        spawnTimeAux -= Time.deltaTime;
    }
}
