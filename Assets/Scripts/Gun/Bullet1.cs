using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bullet1 : Bullet
{
    public override void MoveToTarget()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
        if(Target != null &&Vector3.Distance(transform.position, Target.position)<0.6f)
        {
            Target.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if(!MapBorderChecker.IsPositionInsideMap(transform.position)) Destroy(gameObject);
      
    }


}
