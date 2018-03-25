using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    [SerializeField]
    CollectableType type;

    public override void Collect(Collider2D other)
    {
        print("Coin collected " + type);
        Destroy(gameObject);
    }
}
