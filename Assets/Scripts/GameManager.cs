using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FailCause
{
    OutOfProjectiles,
    GeomitFallen
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public int selectedWorld;
    public int selectedLevel;
    public LevelInfo levelInfo;

    CameraController camController;
    Cannon cannon;
    private bool inGameScene;

    private bool projectileShooted;

    private int projectilesLeft;
    private int geomitsToPickLeft;
    private int goldCoinsPicked;
    private int diamondCoinsPicked;
    private int goldStarsPicked;
    private int diamondStarsPicked;
    private GeomitProjectile[] levelProjectiles;
    private GeomitProjectile nextProjectile;
    private int nextProjectileIndex;
    private bool superSpeedUsed;

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

    public GeomitProjectile NextProjectile
    {
        get { return nextProjectile; }
        set { nextProjectile = value; GameUIController.Instance.SetNextProjectileInfo(value); }
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

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            inGameScene = false;
            InitGame();
            inGameScene = true;
        }
        if(scene.name == "MainMenu" || scene.name == "Shop")
        {
            SceneManager.sceneLoaded -= OnSceneLoad;
        }
    }

    private void Update()
    {
        if (!projectileShooted) return;

        if (superSpeedUsed) return;

        if(Input.GetMouseButton(0))
        {
            superSpeedUsed = true;
            print("Super Speed");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            NextProjectile.SuperSpeed(mousePos);
            print(mousePos);
        }

    }

    private void FixedUpdate()
    {
        if (!inGameScene) return;

        Physics2D.gravity = new Vector2(0, levelInfo.Gravity);
    }

    private void InitGame()
    {
        GameUIController.Instance.ShowShootingPanels(false);
        Instantiate(levelInfo.Scenario);
        cannon = FindObjectOfType<Cannon>();
        camController = Camera.main.GetComponent<CameraController>();
        camController.SetCameraLimits(levelInfo.CamHztalLimits, levelInfo.CamVrtcalLimits);

        //Load level projectiles
        levelProjectiles = new GeomitProjectile[levelInfo.ProjectilesCount];
        for (int index = 0; index < levelInfo.ProjectilesCount; index++)
        {
            GeomitProjectile prefab = PrefabsLibrary.Instance.GetAvailableRandomProjectile();
            levelProjectiles[index] = Instantiate(prefab, new Vector3(100, 10, -20), Quaternion.identity);
            levelProjectiles[index].Rigidbody_.simulated = false;
            levelProjectiles[index].gameObject.name = prefab.name;
            levelProjectiles[index].Rigidbody_.mass = levelInfo.ProjectilesMass[index];
        }

        GameUIController.Instance.SetLevelGravityText(levelInfo.Gravity);
        ProjectilesLeft = levelInfo.ProjectilesCount;
        GeomitsToPickLeft = FindObjectsOfType<Geomit>().Length;
        GoldCoinsPicked = 0;
        DiamondCoinsPicked = 0;
        GoldStarsPicked = 0;
        DiamondStarsPicked = 0;

        nextProjectileIndex = 0;
        NextProjectile = levelProjectiles[nextProjectileIndex];

        GameUIController.Instance.ShowShootingPanels();
    }

    public void ShootGeomit(int angle, int force)
    {
        GameUIController.Instance.ShowShootingPanels(false);
        cannon.Shoot(NextProjectile, angle, force);
        projectileShooted = true;
    }

    public void EndShoot()
    {
        projectileShooted = false;
        superSpeedUsed = false;
        if (GeomitsToPickLeft == 0)
        {
            //Game Completed
            GameProgress.Instance.PlayerProgress.worldsProgress[selectedWorld] = selectedLevel + 1;
            GameProgress.Instance.PlayerProgress.goldCoins += GoldCoinsPicked;
            GameProgress.Instance.PlayerProgress.diamondCoins += DiamondCoinsPicked;
            GameProgress.Instance.PlayerProgress.goldStars += GoldStarsPicked;
            GameProgress.Instance.PlayerProgress.diamondStars += DiamondStarsPicked;

            GameUIController.Instance.ShowWinPanel(levelInfo.NextLevelInfo);

            GameProgress.Instance.SaveProgress();

        }
        else
        {
            ProjectilesLeft--;
            if (ProjectilesLeft > 0)
            {
                nextProjectileIndex++;
                NextProjectile = levelProjectiles[nextProjectileIndex];
                GameUIController.Instance.ShowShootingPanels();
            }
            else
            {
                //Perdio
                LevelFailed(FailCause.OutOfProjectiles);
            }
        }
    }

    public void LevelFailed(FailCause cause)
    {
        string message = string.Empty;
        switch (cause)
        {
            case FailCause.OutOfProjectiles:
                message = "Out of Projectiles";
                break;
            case FailCause.GeomitFallen:
                message = "Geomit Fallen";
                break;
        }

        GameUIController.Instance.ShowLosePanel(message);
    }
}
