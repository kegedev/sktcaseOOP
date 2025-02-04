using System;
using UnityEngine;

public class CollectableGun : Collectable
{
    [SerializeField] private GunType _gunType;


    public override void Interact()
    {
        ActionManager.AddSegment.Invoke(_gunType);
        Destroy(gameObject);
    }
}


public static partial class ActionManager
{
    public static Action<GunType> AddSegment;
}