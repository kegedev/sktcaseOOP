using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactoryManager : MonoBehaviour
{
    public static ObjectFactoryManager Instance { get; private set; }

    [SerializeField] private List<FactorySettings> SubscribedFactories= new List<FactorySettings>();
    private Dictionary<PoolType, ObjectFactory> Factories = new Dictionary<PoolType, ObjectFactory>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        foreach (var factorySettings in SubscribedFactories)
        {
            ObjectFactory objectFactory = CreateFactory(factorySettings.PoolType, factorySettings.Prefab, factorySettings.InitialSize);
            Factories.Add(factorySettings.PoolType, objectFactory);
        }
    }

    private ObjectFactory CreateFactory(PoolType poolType, GameObject prefab, int initialSize)
    {
        return poolType switch
        {
            PoolType.SnakeSegment1 => new SnakeSegmentOneFactory(poolType, prefab, initialSize),
            PoolType.SnakeSegment2 => new SnakeSegmentTwoFactory(poolType, prefab, initialSize),
            PoolType.SnakeSegment3 => new SnakeSegmentThreeFactory(poolType, prefab, initialSize),
            _ => null,
        };
    }

    public GameObject GetInstance(PoolType poolType, Vector3 position,Quaternion rotation)
    {
        if(Factories.ContainsKey(poolType))
        {
            GameObject obj = Factories[poolType].GetObject();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void ReturnInstance(PoolType poolType, GameObject gameObject)
    {
        Factories[poolType].ReturnObject(gameObject);
    }

}

[Serializable]
public struct FactorySettings
{
    public PoolType PoolType;
    public GameObject Prefab;
    public int InitialSize;
}
