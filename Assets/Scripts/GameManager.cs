using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }

    public float levelGravity = 9.81f;

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
        camController.Target = cannon.transform;
    }

    private void FixedUpdate()
    {
        Physics2D.gravity = new Vector2(0, -levelGravity);
    }

    public void ShootGeomit(int angle, int force)
    {
        cannon.Shoot(angle, force);
    }
}
