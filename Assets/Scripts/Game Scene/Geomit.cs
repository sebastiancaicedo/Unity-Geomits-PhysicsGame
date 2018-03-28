using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geomit : Collectable {

    public override void Collect(Collider2D other)
    {
        GameManager.Instance.GeomitsToPickLeft--;
        Destroy(gameObject);
    }
}
