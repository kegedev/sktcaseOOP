using UnityEngine;

public class SegmentHealthComponent : HealthComponent
{
    [SerializeField] SnakeSegment snakeSegment;
    public override void HandleDeath()
    {
        snakeSegment.Kill();

    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
