using System;
using TMPro;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _turnSpeed = 180f;

    [Header("Tracking and Segment Settings")]
    [SerializeField] private float _distanceBetweenTrackingPositions = 0.2f;
    [SerializeField] private int _segmentSpacing = 5;
    [SerializeField] private int _maxSegmentCount = 12;

    [Header("Segment Prefab")]
    [SerializeField] private GameObject _snakeSegmentPrefab;

    private SnakeMovement _snakeMovement;
    private SnakeSegmentManager _segmentManager;

    private void Start()
    {
        _segmentManager = new SnakeSegmentManager(_snakeSegmentPrefab,_segmentSpacing, _maxSegmentCount);
        _snakeMovement = new SnakeMovement(_segmentManager.Segments, _segmentManager.Positions, _speed, _turnSpeed, _distanceBetweenTrackingPositions, _segmentSpacing);

        _segmentManager.InitializeSegments(5);
        ActionManager.GetClosestSnakeSegment += GetClosestSegment;
        ActionManager.SegmentKilled += RemoveSegment;
        ActionManager.AddSegment += AddSegment;
    }

    private SnakeSegment GetClosestSegment(Vector3 enemy)
    {
        Transform closestSegment = _segmentManager.GetClosestSegment(enemy);
        SnakeSegment snakeSegment = closestSegment.GetComponent<SnakeSegment>();
        
        return snakeSegment;
    }

    private void FixedUpdate()
    {
        _snakeMovement.MoveHead();
        _snakeMovement.TrackPositions();
        _snakeMovement.MoveSegments();
    }

    public void SetTurnDirection(float dir)
    {
        _snakeMovement.SetTurnDirection(dir);
    }

    public void AddSegment(GunType gunType)
    {
        _segmentManager.AddSegment(GetPoolType(gunType));
    }
    private SegmentType GetPoolType(GunType gunType)
    {
        return gunType switch
        {
            GunType.Gun1 => SegmentType.Segment1,
            GunType.Gun2 => SegmentType.Segment2,
            GunType.Gun3 => SegmentType.Segment3,
            _ => SegmentType.Segment1,
        };
    }
    public void RemoveSegment(SnakeSegment snakeSegment)
    {
        _segmentManager.RemoveSegment(snakeSegment);
    }
}