using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolObject
    {
        public GameObject obj;
        public string name;
        public int count;
    }

    public static ObjectPool instance;
    private void Awake()
    {
        instance = this;
    }

    public List<PoolObject> objects;

    public Dictionary<string, Queue<GameObject>> poolObjects;

    void Start()
    {
        poolObjects = new Dictionary<string, Queue<GameObject>>();

        foreach(PoolObject pool in objects)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.count; i++)
            {
                GameObject obj = Instantiate(pool.obj);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolObjects.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!poolObjects.ContainsKey(name))
        {
            Debug.Log(name + "excist");
            return null;
        }

        GameObject objectToSpawn = poolObjects[name].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IObjectSpawn pooledObj = objectToSpawn.GetComponent<IObjectSpawn>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolObjects[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}