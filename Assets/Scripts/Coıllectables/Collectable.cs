using System;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] float InteractDistance = 1.3f;
    void Update()
    {
        SnakeSegment newTarget = ActionManager.GetClosestSnakeSegment?.Invoke(transform.position);
        if(Vector3.Distance(newTarget.transform.position,transform.position)< InteractDistance)
        {
            Interact();
        }
    }

    public abstract void Interact();
}

