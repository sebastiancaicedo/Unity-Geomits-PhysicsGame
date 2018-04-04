using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomitProjectile : MonoBehaviour {

    [SerializeField]
    float maxLifetime = 4;

    private float livingTime = 0;
    private bool touchedSomething;
    private bool usingSuperSpeed;
    private float maxSuperSpeedTime = 2;
    private float superSpeedMagnitude = 10;
    private float superSpeedTime = 0;
    private Vector2 superSpeedVelocity;

    public bool isBeenShooted;
    public Rigidbody2D Rigidbody_ { get; private set; }
    public Sprite Sprite_ { get; private set; }

    AudioSource audioSource;

    private void Awake()
    {
        Rigidbody_ = GetComponent<Rigidbody2D>();
        Sprite_ = GetComponent<SpriteRenderer>().sprite;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isBeenShooted) return;

        livingTime += Time.deltaTime;

        if(livingTime > maxLifetime)
        {
            Destroy(gameObject);
        }


        if (!usingSuperSpeed) return;

        superSpeedTime += Time.deltaTime;
        if (superSpeedTime > maxSuperSpeedTime)
        {
            usingSuperSpeed = false;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.EndShoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!touchedSomething)
            touchedSomething = true;

        if (usingSuperSpeed)
            usingSuperSpeed = false;
    }

    public void SuperSpeed(Vector3 targetPoint)
    {
        if (touchedSomething) return;

        Vector2 targetDir = targetPoint - transform.position;
        superSpeedVelocity = targetDir.normalized * superSpeedMagnitude * (Rigidbody_.mass/2);
        Rigidbody_.velocity = superSpeedVelocity;
        usingSuperSpeed = true;

    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
