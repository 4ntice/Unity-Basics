using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    public float dTimer;

    private void FixedUpdate()
    {
        while(dTimer > 0)
        {
            dTimer -= Time.deltaTime;
            return;
        }

        dTimer = 1;
        ObjectPool.instance.SpawnFromPool("1", transform.position, transform.rotation);
    }
}
