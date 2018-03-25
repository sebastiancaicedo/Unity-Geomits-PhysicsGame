using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Gold = 0,
    Diamond = 1
}

public class Star : Collectable
{

    [SerializeField]
    CollectableType type;

    public override void Collect(Collider2D other)
    {
        print("Star Colected: " + type);
        Destroy(gameObject);
    }
}
