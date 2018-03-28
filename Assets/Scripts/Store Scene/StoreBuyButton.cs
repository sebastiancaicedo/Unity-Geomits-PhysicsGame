using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreBuyButton : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    GameObject ownedText;
    [SerializeField]
    GameObject priceGO;

    Button button;
    StoreCard card;

    private bool elementOwned;

    private void Awake()
    {
        button = GetComponent<Button>();
        card = GetComponentInParent<StoreCard>();
    }

    private void Start()
    {
        ValidateOwning();
    }

    private void ValidateOwning()
    {
        elementOwned = GameProgress.Instance.PlayerProgress.projectilesOwned.Contains(card.ElementName);
        if (elementOwned)
        {
            Destroy(button);
            priceGO.SetActive(false);
            ownedText.SetActive(true);
        }
        else
        {
            priceGO.SetActive(true);
            ownedText.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (elementOwned) return;

        switch (card.CoinType)
        {
            case CollectableType.Gold:
                if(GameProgress.Instance.PlayerProgress.goldCoins >= card.Price)
                {
                    GameProgress.Instance.PlayerProgress.goldCoins -= card.Price;
                    StoreUIController.Instance.SetGoldCoinsText(GameProgress.Instance.PlayerProgress.goldCoins);
                    GameProgress.Instance.PlayerProgress.projectilesOwned.Add(card.ElementName);
                    GameProgress.Instance.SaveProgress();
                    ValidateOwning();
                }
                break;
            case CollectableType.Diamond:
                if (GameProgress.Instance.PlayerProgress.diamondCoins >= card.Price)
                {
                    GameProgress.Instance.PlayerProgress.diamondCoins -= card.Price;
                    StoreUIController.Instance.SetDiamondCoinsText(GameProgress.Instance.PlayerProgress.diamondCoins);
                    GameProgress.Instance.PlayerProgress.projectilesOwned.Add(card.ElementName);
                    GameProgress.Instance.SaveProgress();
                    ValidateOwning();
                }
                break;
        }
    }

}
