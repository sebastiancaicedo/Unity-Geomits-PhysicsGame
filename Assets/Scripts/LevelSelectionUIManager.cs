using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionUIManager : MonoBehaviour {

    [SerializeField]
    Text titleText;
    [SerializeField]
    GameObject worldsPanel;
    [SerializeField]
    GameObject[] levelsPanel;

    private Dictionary<int, GameObject> levelsPanelsByWorld = new Dictionary<int, GameObject>();

    private void Awake()
    {
        for (int world = 1; world <= levelsPanel.Length; world++)
        {
            levelsPanelsByWorld.Add(world, levelsPanel[world - 1]);
            levelsPanel[world - 1].SetActive(false);
        }
        worldsPanel.SetActive(true);
    }

    public void OnWorldButtonClick(int worldIndex)
    {
        GameManager.Instance.selectedWorld = worldIndex;
        worldsPanel.SetActive(false);
        titleText.text = "Level Selection";
        levelsPanelsByWorld[worldIndex].SetActive(true);
    }

    public void OnBackButonClick()
    {
        GameObject activePanel;
        if (IsAnyLevelsPanelActive(out activePanel))
        {
            GameManager.Instance.selectedLevel = 0;
            activePanel.SetActive(false);
            titleText.text = "World Selection";
            worldsPanel.SetActive(true);
        }
        else
            if (worldsPanel.activeInHierarchy)
        {
            Destroy(GameManager.Instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnStoreButtonClick()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("Store");
    }

    private bool IsAnyLevelsPanelActive(out GameObject activePanel)
    {
        for (int world = 1; world <= levelsPanelsByWorld.Count; world++)
        {
            if (levelsPanelsByWorld[world].activeInHierarchy)
            {
                activePanel = levelsPanelsByWorld[world];
                return true;
            }
        }
        activePanel = null;
        return false;
    }
}
