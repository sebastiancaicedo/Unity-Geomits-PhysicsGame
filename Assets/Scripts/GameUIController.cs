using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public static GameUIController Instance { get; private set; }

    [SerializeField]
    RectTransform inputsMenu;
    [SerializeField]
    InputField inputfieldAngle;
    [SerializeField]
    InputField inputfieldForce;

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



}
