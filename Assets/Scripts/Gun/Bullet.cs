using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    
    public Transform Target;
    public float Speed;
    public int Damage;
    public bool canMove = false;

    public void Initialize(Transform target)
    {
        Target = target;
        canMove = true;
        transform.LookAt(target.position);
    }
    private void Update()
    {
      if(canMove)  MoveToTarget();
    }


    public abstract void MoveToTarget();
}
