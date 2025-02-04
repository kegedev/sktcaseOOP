using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    private SnakeSegment _targetSnakeSegment;
    private EnemyMovement _enemyMovement;
    private EnemyAttack _enemyAttack;

    private void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        SnakeSegment newTarget = ActionManager.GetClosestSnakeSegment?.Invoke(transform.position);
        if (newTarget != null && newTarget != _targetSnakeSegment)
        {
            SetTarget(newTarget);
        }
    }

    public void SetTarget(SnakeSegment targetSnakeSegment)
    {
        _targetSnakeSegment = targetSnakeSegment;
        _enemyMovement.SetTarget(_targetSnakeSegment.transform);
        _enemyAttack.SetTarget(_targetSnakeSegment.transform);
    }
}