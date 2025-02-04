using UnityEngine;

public class SnakeSegmentTwoFactory : ObjectFactory
{
    public SnakeSegmentTwoFactory(PoolType poolType, GameObject prefab, int initialPoolSize) : base(poolType, prefab, initialPoolSize)
    {
    }

    public override void ResetObject(GameObject obj)
    {

    }
}
