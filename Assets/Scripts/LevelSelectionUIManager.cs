using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUIManager : MonoBehaviour {

    [SerializeField]
    Text titleText;
    [SerializeField]
    GameObject worldsPanel;
    [SerializeField]
    GameObject levelsPanel;

    private void Awake()
    {
        levelsPanel.SetActive(false);
        worldsPanel.SetActive(true);
    }

    private void Start()
    {
        GameManager.GameProgress = new GameProgressInfo();
    }

    public void OnWorldButtonClick(int worldIndex)
    {
        GameManager.Instance.selectedWorld = worldIndex;
        worldsPanel.SetActive(false);
        titleText.text = "Level Selection";
        levelsPanel.SetActive(true);
    }

    public void OnBackButonClick()
    {
        if (levelsPanel.activeInHierarchy)
        {
            levelsPanel.SetActive(false);
            worldsPanel.SetActive(true);
            GameManager.Instance.selectedLevel = 0;
        }
        else
            if (worldsPanel.activeInHierarchy)
        {
            //Se carga la scena main
        }
    }
}
