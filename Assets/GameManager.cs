using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }

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

    public void ShootGeomit(int angle, int force)
    {
        cannon.Shoot(angle, force);
    }
}
