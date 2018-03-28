using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Setted Automatically")]
    [SerializeField]
    int levelIndex;
    [SerializeField]
    LevelInfo levelInfo;

    Button button;

    public int LevelIndex { get { return levelIndex; } }
    public LevelInfo LevelInfo_ { get { return levelInfo; } }

    private void Awake()
    {
        levelIndex = transform.GetSiblingIndex() + 1;
        button = GetComponent<Button>();
        GetComponentInChildren<Text>().text = levelIndex.ToString();
    }

    private void Start()
    {
        int world = GameManager.Instance.selectedWorld;        
        if(LevelIndex > GameProgress.Instance.PlayerProgress.worldsProgress[world])
        {
            button.interactable = false;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.interactable) return;

        GameManager.Instance.selectedLevel = levelIndex;
        GameManager.Instance.levelInfo = levelInfo;
        DontDestroyOnLoad(GameManager.Instance.gameObject);
        SceneManager.LoadScene("Game");
    }
}
