using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravitonForceType
{
    Attraction = 0,
    Repulsion = 1
}

public class Graviton : MonoBehaviour {

    [SerializeField]
    GravitonForceType forceType;
    [SerializeField]
    float force;

    private List<Rigidbody2D> objectsToAffect = new List<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (objectsToAffect.Count == 0) return;

        for (int index = 0; index < objectsToAffect.Count; index++)
        {
            Rigidbody2D elem = objectsToAffect[index];
            int forceSign = forceType == GravitonForceType.Attraction ? -1 : 1;
            Vector2 forceDirection = (elem.transform.position - transform.position).normalized * forceSign;
            elem.AddForce(forceDirection * force * (elem.mass / 2));

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsToAffect.Add(other.GetComponent<Rigidbody2D>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        objectsToAffect.Remove(other.GetComponent<Rigidbody2D>());
    }
}
