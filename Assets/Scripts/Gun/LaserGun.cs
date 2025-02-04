using UnityEngine;

public class LaserGun : Gun
{
    public override void Fire()
    {
        Enemy closestEnemy = ActionManager.GetClosestEnemy.Invoke(transform.position);
        if (closestEnemy != TargetEnemy)
        {
            TargetEnemy = closestEnemy;
        }
        if (closestEnemy == null) return;
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity, null);
        bullet.transform.parent = transform;
        bullet.GetComponent<Bullet>().Initialize(closestEnemy.transform);
    }
}
