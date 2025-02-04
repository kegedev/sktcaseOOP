using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ObjectFactory
{
    public PoolType PoolType;
    public GameObject Prefab;
    public int InitialPoolSize;
    private ObjectPool pool;//TODO: poolu factory disina al

    public ObjectFactory(PoolType poolType, GameObject prefab, int initialPoolSize)
    {
        InitialPoolSize=initialPoolSize;
        Prefab = prefab;
        PoolType=poolType;
        pool = new ObjectPool();
        CreateInitialObjects(initialPoolSize);
    }

    private void CreateInitialObjects(int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
        {
            pool.ReturnObject(CreateObject());
        }
    }

    public GameObject GetObject()
    {
        GameObject obj = pool.GetObject();
        if (obj == null)
        {
            obj = CreateObject();
        }
        ResetObject(obj);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.ReturnObject(obj);
    }

    private GameObject CreateObject()
    {
        return Object.Instantiate(Prefab);
    }

    public virtual void ResetObject(GameObject obj)
    { }
}