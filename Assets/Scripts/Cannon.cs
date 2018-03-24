using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public GeomitProjectile bulletPrefab;
    [SerializeField]
    Transform pivot;
    [SerializeField]
    Transform muzzle;

    public void Shoot(int angle, int force)
    {
        StartCoroutine(AimAndFire(angle, force));
    }

    private IEnumerator AimAndFire(int angle, int force)
    {
        yield return null;
        //Manejo de camara
        //if (GameManager.Instance.UseDynamicCamera)
        //{
        //    GameManager.Instance.Camera.SetShootPosition();
        //    yield return new WaitUntil(() => GameManager.Instance.Camera.IsCameraReady);
        //}

        //Rotacion
        //audioSource.loop = true;
        //PlaySound(aimSound);
        angle = angle % 360;
        do
        {
            pivot.localEulerAngles = Vector3.Lerp(pivot.localEulerAngles, new Vector3(0, 0, angle), 1.2f * Time.deltaTime);
            yield return null;

        } while (Mathf.Abs(Mathf.Abs(angle) - Mathf.Abs(pivot.localEulerAngles.z)) > 2);

        pivot.localEulerAngles = new Vector3(0, 0, angle);
        //audioSource.Stop();
        //audioSource.loop = false;
        yield return new WaitForSeconds(0.3f);

        Fire(force);
        yield return new WaitForEndOfFrame();
    }

    private void Fire(int force)
    {
        //smokeParticles.Play();
        GeomitProjectile bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        //bullet.PlaySound(shootSound);//Lo sonamos desde la bala porque cada bala es una instancia diferencte con su propio audioSource
        bullet._Rigidbody.AddForce(muzzle.right * force, ForceMode2D.Impulse);
    }
}
