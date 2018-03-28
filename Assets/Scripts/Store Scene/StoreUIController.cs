using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreUIController : MonoBehaviour {

    public static StoreUIController Instance { get; private set; }

    [SerializeField]
    Text goldCoinsText;
    [SerializeField]
    Text diamondCoinsText;
    [SerializeField]
    Text goldStarsText;
    [SerializeField]
    Text diamondStarsText;
    [SerializeField]
    RectTransform scrollViewport;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        goldCoinsText.text = GameProgress.Instance.PlayerProgress.goldCoins.ToString();
        diamondCoinsText.text = GameProgress.Instance.PlayerProgress.diamondCoins.ToString();
        goldStarsText.text = GameProgress.Instance.PlayerProgress.goldStars.ToString();
        diamondStarsText.text = GameProgress.Instance.PlayerProgress.diamondStars.ToString();

        scrollViewport.anchorMax = new Vector2(1, 1);
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void SetGoldCoinsText(int coins)
    {
        goldCoinsText.text = coins.ToString();
    }

    public void SetDiamondCoinsText(int coins)
    {
        diamondCoinsText.text = coins.ToString();
    }
}
