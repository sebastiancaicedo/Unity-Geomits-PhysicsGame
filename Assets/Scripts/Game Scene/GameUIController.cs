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

    public void SetLevelGravityText(float levelGravity)
    {
        levelGravityText.text = string.Format("Gravity: {0} m/s2", Mathf.Abs(levelGravity));
    }

    public void SetDiamondStarsText(int diamondStars)
    {
        diamondStarsText.text = diamondStars.ToString();
    }

    public void SetGoldStarsText(int goldStars)
    {
        goldStarsText.text = goldStars.ToString();
    }

    public void SetDiamondCoinsText(int diamondCoins)
    {
        diamondCoinsText.text = diamondCoins.ToString();
    }

    public void SetGoldCoinsText(int goldCoins)
    {
        goldCoinsText.text = goldCoins.ToString();
    }

    public void SetProjectilesLeftText(int projectiles)
    {
        projectilesText.text = projectiles.ToString();
    }

    public void SetGeomitsLeftsText(int geomits)
    {
        geomitsText.text = geomits.ToString();
    }

}
