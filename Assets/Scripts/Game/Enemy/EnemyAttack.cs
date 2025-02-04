using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;
    EnemyMovement _enemyMovement;
    private Transform _target;
    private Enemy _enemy;
    private float _timeElapsed;
    private bool _isTimerRunning=false;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (_target == null) return;
        if (_enemyMovement.IsTargetInRange() && !_isTimerRunning)
        {
            Attack();
        }

        if (_isTimerRunning)
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _attackCooldown)
            {
                OnTimerCompleted();
            }
        }
    }

    private void Attack()
    {
        Debug.Log("ATTACK");
        ActionManager.Attack?.Invoke(_target.GetComponent<SnakeSegment>(), _enemy.GetDamage());
        StartTimer();
    }

    private void OnTimerCompleted()
    {
        StopTimer();
        ResetTimer();
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
        _timeElapsed = 0f;
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
        _timeElapsed = 0f;
    }

    private void ResetTimer()
    {
        _timeElapsed = 0f;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}