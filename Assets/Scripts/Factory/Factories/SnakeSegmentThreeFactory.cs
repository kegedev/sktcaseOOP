using UnityEngine;

public class SnakeSegmentThreeFactory : ObjectFactory
{
    public SnakeSegmentThreeFactory(PoolType poolType, GameObject prefab, int initialPoolSize) : base(poolType, prefab, initialPoolSize)
    {
    }

    public override void ResetObject(GameObject obj)
    {

    }
}
