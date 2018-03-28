using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    [SerializeField]
    CollectableType type;

    public override void Collect(Collider2D other)
    {
        
        switch (type)
        {
            case CollectableType.Gold:
                GameManager.Instance.GoldCoinsPicked++;
                break;
            case CollectableType.Diamond:
                GameManager.Instance.DiamondCoinsPicked++;
                break;
        }
        Destroy(gameObject);
    }
}
