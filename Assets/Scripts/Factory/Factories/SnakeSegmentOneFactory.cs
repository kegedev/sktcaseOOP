using UnityEngine;

public class SnakeSegmentOneFactory : ObjectFactory
{
    public SnakeSegmentOneFactory(PoolType poolType, GameObject prefab, int initialPoolSize) : base(poolType, prefab, initialPoolSize)
    {
    }

    public override void ResetObject(GameObject obj)
    {

    }
}
