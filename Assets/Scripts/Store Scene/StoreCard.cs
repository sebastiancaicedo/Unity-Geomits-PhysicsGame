using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCard : MonoBehaviour {

    [SerializeField]
    CollectableType coinType;
    [SerializeField]
    int price;
    [SerializeField]
    Text titleText;

    public CollectableType CoinType { get { return coinType; } }
    public int Price { get { return price; } }
    public string ElementName { get { return titleText.text; } }
}
