using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geomit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            print("Collected");
            Destroy(gameObject);
        }
    }
}
