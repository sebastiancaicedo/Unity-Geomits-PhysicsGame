using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomitProjectile : MonoBehaviour {

    Rigidbody2D rb;

    [SerializeField]
    float maxLifetime = 4;

    private float startTime;

    public Rigidbody2D _Rigidbody { get { return rb; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad - startTime > maxLifetime)
        {
            Destroy(gameObject);
        }
    }
}
