using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Geomit")
        {
            GameManager.Instance.LevelFailed(FailCause.GeomitFallen);
        }
    }
}
