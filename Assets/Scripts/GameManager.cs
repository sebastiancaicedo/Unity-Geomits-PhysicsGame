using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }
    public static GameProgressInfo GameProgress { get; set; }

    public int selectedWorld;
    public int selectedLevel;
    public LevelInfo levelInfo;

    private bool gameStarted;

    CameraController camController;
    Cannon cannon;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            cannon = FindObjectOfType<Cannon>();
            camController = Camera.main.GetComponent<CameraController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //camController.Target = cannon.transform;
    }

    private void FixedUpdate()
    {
        if (!gameStarted) return;

        Physics2D.gravity = new Vector2(0, levelInfo.Gravity);
    }

    public void ShootGeomit(int angle, int force)
    {
        cannon.Shoot(angle, force);
    }
}
