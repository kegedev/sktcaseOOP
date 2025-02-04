using UnityEngine;

public class EnemyHealthComponent : HealthComponent
{
    [SerializeField] Enemy Enemy;
    public override void HandleDeath()
    {
        Enemy.Kill();
        
    }
}
