using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    [SerializeField]
    Transform pivot;
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    AudioClip shootSound;
    [SerializeField]
    AudioClip aimSound;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(GeomitProjectile projectile, int angle, int force)
    {
        StartCoroutine(AimAndFire(projectile, angle, force));
    }

    private IEnumerator AimAndFire(GeomitProjectile projectile, int angle, int force)
    {
        yield return null;
        //Manejo de camara
        //if (GameManager.Instance.UseDynamicCamera)
        //{
        //    GameManager.Instance.Camera.SetShootPosition();
        //    yield return new WaitUntil(() => GameManager.Instance.Camera.IsCameraReady);
        //}

        //Rotacion
        audioSource.loop = true;
        PlaySound(aimSound);
        angle = angle % 360;
        do
        {
            pivot.localEulerAngles = Vector3.Lerp(pivot.localEulerAngles, new Vector3(0, 0, angle), 1.2f * Time.deltaTime);
            yield return null;

        } while (Mathf.Abs(Mathf.Abs(angle) - Mathf.Abs(pivot.localEulerAngles.z)) > 2);

        pivot.localEulerAngles = new Vector3(0, 0, angle);
        audioSource.Stop();
        audioSource.loop = false;
        yield return new WaitForSeconds(0.3f);

        //Fire(projectile, force);
        Fire(projectile, muzzle.right * force);
        yield return new WaitForEndOfFrame();
    }

    //private void Fire(GeomitProjectile projectile, int force)
    //{
    //    //smokeParticles.Play();
    //    projectile.transform.position = muzzle.position;
    //    projectile.Rigidbody_.simulated = true;
    //    projectile.Rigidbody_.velocity = Vector2.zero;
    //    projectile.PlaySound(shootSound);//Lo sonamos desde la bala porque cada bala es una instancia diferencte con su propio audioSource
    //    projectile.Rigidbody_.AddForce(muzzle.right * force, ForceMode2D.Impulse);
    //    projectile.isBeenShooted = true;
    //}

    private void Fire(GeomitProjectile projectile, Vector2 initialVelocity)
    {
        //smokeParticles.Play();
        projectile.transform.position = muzzle.position;
        projectile.Rigidbody_.simulated = true;
        projectile.Rigidbody_.velocity = Vector2.zero;
        projectile.PlaySound(shootSound);//Lo sonamos desde la bala porque cada bala es una instancia diferencte con su propio audioSource
        //projectile.Rigidbody_.AddForce(muzzle.right * force, ForceMode2D.Impulse);
        projectile.Rigidbody_.velocity = initialVelocity;
        projectile.isBeenShooted = true;
    }

    private void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
