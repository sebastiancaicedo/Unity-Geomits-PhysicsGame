using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public static GameUIController Instance { get; private set; }

    [SerializeField]
    Text levelGravityText;
    [Header("Inputs Menu")]
    [SerializeField]
    RectTransform inputsMenu;
    [SerializeField]
    InputField inputfieldAngle;
    [SerializeField]
    InputField inputfieldForce;
    [Header("Top Panel Text Indicators")]
    [SerializeField]
    Text projectilesText;
    [SerializeField]
    Text geomitsText;
    [SerializeField]
    Text diamondStarsText;
    [SerializeField]
    Text goldStarsText;
    [SerializeField]
    Text diamondCoinsText;
    [SerializeField]
    Text goldCoinsText;
    [Header("Next Projectile Info")]
    [SerializeField]
    GameObject projetileInfoPanel;
    [SerializeField]
    Image projectileImage;
    [SerializeField]
    Text projectileNameText;
    [SerializeField]
    Text projectileMassText;
    [Header("Sub Menus")]
    [SerializeField]
    GameObject loseMenu;
    [SerializeField]
    Text tipText;
    [SerializeField]
    GameObject winMenu;
    [SerializeField]
    Button nextLevelButton;
    [SerializeField]
    GameObject pauseMenu;
    [Header("Rule")]
    [SerializeField]
    Transform rulePrefab;

    private Transform rule;
    private int ruleRotInput;
    public bool isShowingRule;

    private LevelInfo nextLevelInfo;

    public string AngleInput { get { return inputfieldAngle.text; } }
    public string ForceInput { get { return inputfieldForce.text; } }

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
        inputfieldForce.onValueChanged.AddListener(delegate { ValidateNoNegative(inputfieldForce); });
        inputfieldAngle.onValueChanged.AddListener(delegate { ValidateNoNegative(inputfieldAngle); });
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        rule = Instantiate(rulePrefab);
        rule.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isShowingRule = !isShowingRule;
            rule.gameObject.SetActive(isShowingRule);
        }

        if (isShowingRule)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            rule.position = mousePos;

            if (Input.GetKeyDown(KeyCode.Q))
                ruleRotInput = -1;

            if (Input.GetKeyDown(KeyCode.E))
                ruleRotInput = 1;

            if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
                ruleRotInput = 0;
      
            rule.Rotate(0, 0, 45 * ruleRotInput * Time.deltaTime);
        }
    }

    /// <summary>
    /// Valida que la fuerza no sea negativa,
    /// Se llama en el OnValueChange del forceInputField
    /// </summary>
    public void ValidateNoNegative(InputField inputField)
    {
        if (inputField.text.Contains("-"))
            inputField.text = string.Empty;
    }

    public void OnShootButtonClick()
    {
        string strAnlgle = inputfieldAngle.text;
        string strForce = inputfieldForce.text;
        int angle, force;
        if (int.TryParse(strAnlgle, out angle) && int.TryParse(strForce, out force))
        {
            GameManager.Instance.ShootGeomit(angle, force);
        }
        else
        {
            Debug.LogError("Verifique los campos");
        }
    }

    public void OnPauseButtonClick()
    {
        pauseMenu.SetActive(true);
    }

    public void OnContinueButtonClick()
    {
        pauseMenu.SetActive(false);
    }

    public void OnRetryButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnExitButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }

    public void OnNextButtonClick()
    {
        if (nextLevelInfo)
        {
            GameManager.Instance.selectedLevel++;
            GameManager.Instance.levelInfo = nextLevelInfo;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }

    public void ShowShootingPanels(bool show = true)
    {
        if (inputsMenu && projetileInfoPanel)
        {
            inputsMenu.gameObject.SetActive(show);
            projetileInfoPanel.SetActive(show);
        }
    }

    public void SetLevelGravityText(float levelGravity)
    {
        if(levelGravityText)
            levelGravityText.text = string.Format("Gravity: {0} m/s2", Mathf.Abs(levelGravity));
    }

    public void SetDiamondStarsText(int diamondStars)
    {
        if (diamondStarsText)
            diamondStarsText.text = diamondStars.ToString();
    }

    public void SetGoldStarsText(int goldStars)
    {
        if(goldStarsText)
            goldStarsText.text = goldStars.ToString();
    }

    public void SetDiamondCoinsText(int diamondCoins)
    {
        if(diamondCoinsText)
            diamondCoinsText.text = diamondCoins.ToString();
    }

    public void SetGoldCoinsText(int goldCoins)
    {
        if(goldCoinsText)
            goldCoinsText.text = goldCoins.ToString();
    }

    public void SetProjectilesLeftText(int projectiles)
    {
        if(projectilesText)
            projectilesText.text = projectiles.ToString();
    }

    public void SetGeomitsLeftsText(int geomits)
    {
        if(geomitsText)
            geomitsText.text = geomits.ToString();
    }

    public void SetNextProjectileInfo(GeomitProjectile projectile)
    {
        if (projectileNameText && projectile)
        {
            projectileNameText.text = projectile.gameObject.name;
            projectileImage.sprite = projectile.Sprite_;
            projectileMassText.text = string.Format("Mass: {0} Kg", projectile.Rigidbody_.mass);
        }
    }

    public void ShowWinPanel(LevelInfo nextLevelInfo, bool show = true)
    {
        if (winMenu)
        {
            if (nextLevelInfo == null) nextLevelButton.interactable = false;
            this.nextLevelInfo = nextLevelInfo;
            winMenu.SetActive(show);
        }
    }

    public void ShowLosePanel(string loseMessaje, bool show = true)
    {
        if (loseMenu)
        {
            loseMenu.SetActive(show);
            if (show)
                if (tipText)
                    tipText.text = string.Format("Level Failed: {0}\nTip: {1}", loseMessaje, TipsHandler.Instance.GetTip());
        }
    }

}
