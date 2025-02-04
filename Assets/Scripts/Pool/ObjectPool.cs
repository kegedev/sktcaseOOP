using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    public GameObject GetObject()
    {
        if (pool.Count == 0) return null;
        GameObject obj = pool.Dequeue();
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        pool.Enqueue(obj);
    }
}