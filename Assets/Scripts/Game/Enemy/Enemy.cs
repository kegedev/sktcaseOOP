using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyTargeting))]
public class Enemy : MonoBehaviour
{
    private const int Damage = 10;

    public int GetDamage() => Damage;
   
    [SerializeField]  HealthComponent _healthComponent;

    private void Start()
    {
       // _healthComponent = GetComponent<HealthComponent>();
    }


    public void TakeDamage(int damage)
    {
        _healthComponent.TakeDamage(damage);
    }

    internal void Kill()
    {
        ActionManager.EnemyKilled?.Invoke(this);
    }
}
public static partial class ActionManager
{
    public static Func<Vector3, SnakeSegment> GetClosestSnakeSegment;
    public static Action<Enemy> UnSubscribeFromAllSegments;
    public static Action<Enemy> EnemyKilled;
    public static Action<SnakeSegment, int> Attack;
}
