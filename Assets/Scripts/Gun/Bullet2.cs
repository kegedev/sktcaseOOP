using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bullet2 : Bullet
{
    [SerializeField] LineRenderer lineRenderer;

    private void Start()
    {
        StartCoroutine(DelayFire());
    }

    public override void MoveToTarget()
    {
        if(Target==null) Destroy(gameObject);
        lineRenderer.SetPosition(0, transform.position);
        if (Target != null) lineRenderer.SetPosition(1, Target.position);

        
    }
    IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(1);
        if (Target != null)
        {
            Target.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
    
}
