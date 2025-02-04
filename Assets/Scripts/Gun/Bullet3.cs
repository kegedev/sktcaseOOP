using UnityEngine;

public class Bullet3 : Bullet
{
    private Vector3 _circleCenter;
    private float _circleRadius = 1f;
    private float _angle = 0f;

    public override void MoveToTarget()
    {
        if (Target == null)
        {
            GetNewTarget();
            if (Target == null)
            {
                MoveInCircle();
                return;
            }
        }

        transform.LookAt(Target.position);
        transform.position += transform.forward * Speed * Time.deltaTime;

        if (Target != null && Vector3.Distance(transform.position, Target.position) < 0.6f)
        {
            Target.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }

        if (!MapBorderChecker.IsPositionInsideMap(transform.position))
        {
            Destroy(gameObject);
        }
    }

    private void GetNewTarget()
    {
        Enemy closestEnemy = ActionManager.GetClosestEnemy.Invoke(transform.position);
        if (closestEnemy != null)
        {
            Target = closestEnemy.transform;
            _circleCenter = transform.position;
        }
    }

    private void MoveInCircle()
    {
        _angle += Speed * Time.deltaTime / 2;
        if (_angle >= 360f) _angle -= 360f;

        float x = _circleCenter.x + Mathf.Cos(_angle) * _circleRadius;
        float z = _circleCenter.z + Mathf.Sin(_angle) * _circleRadius;
        Vector3 newPosition = new Vector3(x, _circleCenter.y, z);

        float nextAngle = _angle + 0.1f; 
        float nextX = _circleCenter.x + Mathf.Cos(nextAngle) * _circleRadius;
        float nextZ = _circleCenter.z + Mathf.Sin(nextAngle) * _circleRadius;
        Vector3 nextPosition = new Vector3(nextX, _circleCenter.y, nextZ);

        transform.position = newPosition;
        transform.LookAt(nextPosition);

        GetNewTarget();
    }

}