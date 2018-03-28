using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreUIController : MonoBehaviour {

    [SerializeField]
    Text[] texts;

    private void Start()
    {
        texts[0].text = string.Format("Gold Coins: {0}", GameProgress.Instance.PlayerProgress.goldCoins);
        texts[1].text = string.Format("Diamond Coins: {0}", GameProgress.Instance.PlayerProgress.diamondCoins);
        texts[2].text = string.Format("Gold Stars: {0}", GameProgress.Instance.PlayerProgress.goldStars);
        texts[3].text = string.Format("Diamond Stars: {0}", GameProgress.Instance.PlayerProgress.diamondStars);
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
