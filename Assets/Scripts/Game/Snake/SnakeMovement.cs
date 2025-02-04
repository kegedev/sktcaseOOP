using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement
{
    private List<Transform> _segments;
    private List<Vector3> _positions;

    private readonly float _speed;
    private readonly float _turnSpeed;
    private readonly float _distanceBetweenTrackingPositions;
    private readonly int _segmentSpacing;
    private float _followSpeed { get { return _speed * 2; } }



    private float _turnDirection = 0f;
    private Vector3 _lastTrackedPosition;
    private float _accumulatedDistance = 0f;

    public SnakeMovement(List<Transform> segments, List<Vector3> positions, float speed, float turnSpeed, float distanceBetweenTrackingPositions, int segmentSpacing)
    {
        _segments = segments;
        _positions = positions;
        _speed = speed;
        _turnSpeed = turnSpeed;
        _distanceBetweenTrackingPositions = distanceBetweenTrackingPositions;
        _segmentSpacing = segmentSpacing;

        if (segments.Count > 0)
        {
            _lastTrackedPosition = segments[0].position;
        }
    }

    public void MoveHead()
    {
        if (_segments.Count == 0) return;

        _segments[0].Rotate(0f, _turnDirection * _turnSpeed * Time.deltaTime, 0f);
        _segments[0].position += _segments[0].forward * _speed * Time.deltaTime;

        CheckMapBoundaries();

        _accumulatedDistance += Vector3.Distance(_segments[0].position, _lastTrackedPosition);
        _lastTrackedPosition = _segments[0].position;
    }

    private void CheckMapBoundaries()
    {
       float minX = -12.5f;
       float maxX = 12.5f;
       float minZ = -8f;
       float maxZ = 8f;

        Vector3 currentPosition = _segments[0].position;

        if (currentPosition.x < minX || currentPosition.x > maxX)
        {
            Vector3 newDirection = Vector3.Reflect(_segments[0].forward, Vector3.right);
            _segments[0].forward = newDirection;

            currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
            _segments[0].position = currentPosition;
        }

        if (currentPosition.z < minZ || currentPosition.z > maxZ)
        {
            Vector3 newDirection = Vector3.Reflect(_segments[0].forward, Vector3.forward);
            _segments[0].forward = newDirection;

            currentPosition.z = Mathf.Clamp(currentPosition.z, minZ, maxZ);
            _segments[0].position = currentPosition;
        }
    }

    public void TrackPositions()
    {
        while (_accumulatedDistance >= _distanceBetweenTrackingPositions)
        {
            float t = (_distanceBetweenTrackingPositions / _accumulatedDistance);
            Vector3 interpolatedPosition = Vector3.Lerp(_positions[_positions.Count - 1], _segments[0].position, t);

            _positions.Add(_segments[0].position); 

            CleanupOldPositions();

            _accumulatedDistance -= _distanceBetweenTrackingPositions;
        }
    }

    public void MoveSegments()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Vector3 targetPos = GetTargetPositionForSegment(i);
            _segments[i].position = Vector3.MoveTowards(_segments[i].position, targetPos, _followSpeed * Time.deltaTime);

            if (_segments[i - 1] != null)
                _segments[i].transform.LookAt(_segments[i - 1].position);
        }
    }

    private Vector3 GetTargetPositionForSegment(int segmentIndex)
    {
        float targetDistance = segmentIndex * _segmentSpacing;
        if (_positions.Count - (int)targetDistance<0) return _positions[0];
        return _positions[_positions.Count - (int)targetDistance];
    }

    private void CleanupOldPositions()
    {
        int removedPositionCount = _positions.Count - (_segments.Count * _segmentSpacing + _segmentSpacing);
        for (int i = 0; i < removedPositionCount; i++)
        {
          
            _positions.RemoveAt(0);
            
        }
    }

    public void SetTurnDirection(float dir)
    {
        _turnDirection = dir;
    }
}