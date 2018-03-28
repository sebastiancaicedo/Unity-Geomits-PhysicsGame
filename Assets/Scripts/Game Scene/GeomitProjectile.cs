using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomitProjectile : MonoBehaviour {

    [SerializeField]
    float maxLifetime = 4;

    private float livingTime = 0;

    public bool isBeenShooted;

    public Rigidbody2D Rigidbody_ { get; private set; }
    public Sprite Sprite_ { get; private set; }

    private void Awake()
    {
        Rigidbody_ = GetComponent<Rigidbody2D>();
        Sprite_ = GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (!isBeenShooted) return;

        livingTime += Time.deltaTime;

        if(livingTime > maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.EndShoot();
    }
}
