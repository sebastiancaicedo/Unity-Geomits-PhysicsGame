﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour {

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
