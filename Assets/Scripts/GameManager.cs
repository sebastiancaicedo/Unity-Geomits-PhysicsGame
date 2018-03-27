using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public static GameProgressInfo GameProgress { get; set; }

    public int selectedWorld;
    public int selectedLevel;
    public LevelInfo levelInfo;

    CameraController camController;
    Cannon cannon;
    private bool inGameScene;

    private int projectilesLeft;
    private int geomitsToPickLeft;
    private int goldCoinsPicked;
    private int diamondCoinsPicked;
    private int goldStarsPicked;
    private int diamondStarsPicked;

    #region Properties

    public int ProjectilesLeft
    {
        get { return projectilesLeft; }
        set { projectilesLeft = value; GameUIController.Instance.SetProjectilesLeftText(value); }
    }

    public int GeomitsToPickLeft
    {
        get { return geomitsToPickLeft; }
        set { geomitsToPickLeft = value; GameUIController.Instance.SetGeomitsLeftsText(value); }
    }

    public int GoldCoinsPicked
    {
        get { return goldCoinsPicked; }
        set { goldCoinsPicked = value; GameUIController.Instance.SetGoldCoinsText(value); }
    }

    public int DiamondCoinsPicked
    {
        get { return diamondCoinsPicked; }
        set { diamondCoinsPicked = value; GameUIController.Instance.SetDiamondCoinsText(value); }
    }

    public int GoldStarsPicked
    {
        get { return goldStarsPicked; }
        set { goldStarsPicked = value; GameUIController.Instance.SetGoldStarsText(value); }
    }

    public int DiamondStarsPicked
    {
        get { return diamondStarsPicked; }
        set { diamondStarsPicked = value; GameUIController.Instance.SetDiamondStarsText(value); }
    }

    #endregion Properties

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
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            InitGame();
            inGameScene = true;
        }
    }

    private void FixedUpdate()
    {
        if (!inGameScene) return;

        Physics2D.gravity = new Vector2(0, levelInfo.Gravity);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void InitGame()
    {
        Instantiate(levelInfo.Scenario);
        cannon = FindObjectOfType<Cannon>();
        camController = Camera.main.GetComponent<CameraController>();
        camController.SetCameraLimits(levelInfo.CamHztalLimits, levelInfo.CamVrtcalLimits);

        GameUIController.Instance.SetLevelGravityText(levelInfo.Gravity);
        ProjectilesLeft = levelInfo.ProjectilesCount;
        GeomitsToPickLeft = FindObjectsOfType<Geomit>().Length;
        GoldCoinsPicked = 0;
        DiamondCoinsPicked = 0;
        GoldStarsPicked = 0;
        DiamondStarsPicked = 0;

    }

    public void ShootGeomit(int angle, int force)
    {
        cannon.Shoot(angle, force);
    }
}
