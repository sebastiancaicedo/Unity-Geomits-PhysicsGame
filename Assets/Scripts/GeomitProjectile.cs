using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomitProjectile : MonoBehaviour {

    Rigidbody2D rb;

    public Rigidbody2D _Rigidbody { get { return rb; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
