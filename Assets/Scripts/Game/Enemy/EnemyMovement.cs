using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] Transform Model;
    private Transform _target;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            Debug.LogWarning("No Target");
            return;
        }

        MoveTowardsTarget();
       RotateTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, _target.position);

        if (distanceToTarget > _stoppingDistance)
        {
            _rigidbody.linearVelocity = direction * _moveSpeed;
        }
        else
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Model.rotation = Quaternion.Slerp(Model.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, _target.position)<=_stoppingDistance;
    }
}