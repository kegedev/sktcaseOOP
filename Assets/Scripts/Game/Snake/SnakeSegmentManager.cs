using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnakeSegmentManager
{
    private GameObject _snakeSegmentPrefab;
    private GameObject _debugObj;

    public List<Transform> Segments { get; private set; }
    public List<Vector3> Positions { get; private set; }

    private int _segmentSpacing=0;
    private int _maxSegmentCount=0;

    public SnakeSegmentManager(GameObject snakeSegmentPrefab, int segmentSpacing, int maxSegmentCount)
    {
        _snakeSegmentPrefab = snakeSegmentPrefab;

        Segments = new List<Transform>();
        Positions = new List<Vector3>();
        _segmentSpacing = segmentSpacing;
        _maxSegmentCount = maxSegmentCount;
    }

    public void InitializeSegments(int initialSegmentCount)
    {
        for (int i = 0; i < initialSegmentCount; i++)
        {
            AddSegment((SegmentType)Random.Range(0,3));
        }
    }

    public void AddSegment(SegmentType segmentType)
    {
        if (Segments.Count >= _maxSegmentCount) return;
        if (Segments.Count == 0)
        {
            CreateInitialSegment(segmentType);
        }
        else
        {
            CreateSegmentAtTail(segmentType);
        }
    }

    private void CreateInitialSegment(SegmentType segmentType)
    {
        GameObject newSegment = ObjectFactoryManager.Instance.GetInstance(GetPoolType(segmentType), Vector3.zero,Quaternion.identity);
        Segments.Add(newSegment.transform);
        Positions.Add(Vector3.zero);
    }

    private void CreateSegmentAtTail(SegmentType segmentType)
    {
        Transform tail = Segments[Segments.Count - 1];
        GameObject newSegment = ObjectFactoryManager.Instance.GetInstance(GetPoolType(segmentType), tail.position, tail.rotation);
        Segments.Add(newSegment.transform);
    }

    private PoolType GetPoolType(SegmentType segmentType)
    {
        return segmentType switch
        {
            SegmentType.Segment1 => PoolType.SnakeSegment1,
            SegmentType.Segment2 => PoolType.SnakeSegment2,
            SegmentType.Segment3 => PoolType.SnakeSegment3,
            _ => PoolType.SnakeSegment1,
        };
    }

    public void RemoveSegment(SnakeSegment snakeSegment)
    {
        
            RemoveSegmentAtIndex(snakeSegment.PoolType,GetSegmentIndex(snakeSegment.transform));
        
    }

    private void RemoveSegmentAtIndex(PoolType pooltype,int index)
    {
        GameObject oldHead = Segments[index].gameObject;
        Segments.RemoveAt(index);
        ObjectFactoryManager.Instance.ReturnInstance(pooltype, oldHead);

        if (index == 0)
        {
            RemoveExtraPositions();
        }
    }

    private int GetSegmentIndex(Transform transform)
    {
        for (int i = 0; i < Segments.Count; i++)
        {
            if (Segments[i]==transform)
            {
                return i;
            }
        }
        return -1;
    }

    private void RemoveExtraPositions()
    {
        for (int i = 0; i < _segmentSpacing * 2 - 2; i++)
        {
            Positions.RemoveAt(Positions.Count - 1);
        }
        for (int i = 0; i < 2; i++) 
        {
            if (Positions.Count > 0)
            {
                Positions.RemoveAt(Positions.Count - 1);
            }
        }
    }
    public Transform GetClosestSegment(Vector3 targetPosition)
    {
        if (Segments == null || Segments.Count == 0)
        {
            Debug.LogWarning("No Segment");
            return null;
        }

        Transform closestSegment = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform segment in Segments)
        {
            if (segment == null) continue; 

            float distance = Vector3.Distance(segment.position, targetPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSegment = segment;
            }
        }

        return closestSegment;
    }

}