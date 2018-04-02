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

    Rigidbody2D rb;

    public CollectableType Type { get { return type; } }
    public Rigidbody2D Rigidbody_ { get { return rb; } }
    public Vector2 VelocityDir { get; set; }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Collect(Collider2D other)
    {
        switch (type)
        {
            case CollectableType.Gold:
                GameManager.Instance.GoldStarsPicked++;
                break;
            case CollectableType.Diamond:
                GameManager.Instance.DiamondStarsPicked++;
                break;
        }
        Destroy(gameObject);
    }
}
