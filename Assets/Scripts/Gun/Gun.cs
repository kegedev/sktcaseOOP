using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject BulletPrefab;
    public Enemy TargetEnemy;
    [SerializeField] BulletType BulletType;
    [SerializeField] Transform Model;
    [SerializeField] Transform GunTip;

    [SerializeField] float FireRate;
    private float timeElapsed = 0f;
    private bool isTimerRunning = false;

    private void Start()
    {
        ActionManager.EnemyKilled += OnEnemyKilled;
        StartTimer();
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        if(TargetEnemy==enemy)
        {
            TargetEnemy = null;
        }
    }

    void Update()
    {
        if(TargetEnemy!=null) Model.LookAt(TargetEnemy.transform.position);
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= FireRate)
            {
                isTimerRunning = false;
                timeElapsed = 0f;

                OnTimerCompleted();
            }
        }
    }

    public virtual void Fire()
    {
        Enemy closestEnemy = ActionManager.GetClosestEnemy.Invoke(transform.position);
        if (closestEnemy != TargetEnemy)
        {
            TargetEnemy = closestEnemy;
        }
        if(closestEnemy==null) return;
        GameObject bullet= Instantiate(BulletPrefab, GunTip.position,Quaternion.identity,null);
        bullet.GetComponent<Bullet>().Initialize(closestEnemy.transform);
    }

    public void OnTimerCompleted()
    {
        StopTimer();
        ResetTimer();
        Fire();
        StartTimer();
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        timeElapsed = 0f;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timeElapsed = 0f;
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
    }

}
public static partial class ActionManager
{
    public static Func<Vector3, Enemy> GetClosestEnemy;
}