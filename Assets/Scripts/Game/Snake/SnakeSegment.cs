using System;
using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public PoolType PoolType;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private ParticleSystem protection;
    [SerializeField] GameObject KillParticle;
    [SerializeField] float ProtectionTime;
    private float timeElapsed = 0f;
    private bool isTimerRunning = false;
    private void Start()
    {
        ActionManager.Attack += GetDamage;
        ActivationProtectionFX(false);
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= ProtectionTime)
            {
                isTimerRunning = false;
                timeElapsed = 0f;

                OnTimerCompleted();
            }
        }
    }
    public void OnTimerCompleted()
    {
        StopTimer();
        ResetTimer();
        ActivationProtectionFX(false);
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
    private void GetDamage(SnakeSegment segment, int damage)
    {
        if (!isTimerRunning && segment == this)
        {
            _healthComponent.TakeDamage(damage);
            ActivationProtectionFX(true);
            StartTimer();
        }
    }

    public void ActivationProtectionFX(bool isActive)
    {
        if(isActive) protection.Play();
        else protection.Stop();
    }

    internal void Kill()
    {
        Instantiate(KillParticle, transform.position, Quaternion.identity, null);
        ActionManager.SegmentKilled?.Invoke(this);
    }


}

public static partial class ActionManager
{
    public static Action<SnakeSegment> SegmentKilled;
}