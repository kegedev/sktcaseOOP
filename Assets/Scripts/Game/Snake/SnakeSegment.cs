using System;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public PoolType PoolType;
    [SerializeField] private HealthComponent _healthComponent;


    private void Start()
    {
        ActionManager.Attack += GetDamage;
    }


 
    private void GetDamage(SnakeSegment segment, int damage)
    {
        if (segment == this)
        {
            _healthComponent.TakeDamage(damage);
           
        }
    }

    internal void Kill()
    {
        ActionManager.SegmentKilled?.Invoke(this);
    }
}

public static partial class ActionManager
{
    public static Action<SnakeSegment> SegmentKilled;
}