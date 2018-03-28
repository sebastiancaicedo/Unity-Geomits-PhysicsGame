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
