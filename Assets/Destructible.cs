using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    [SerializeField]
    protected Sprite[] states;

    protected int hits;

    protected SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        hits = 0;
    }

    public virtual void TakeDamage()
    {
        hits++;
        if (hits < states.Length + 1)
        {
            spriteRenderer.sprite = states[hits - 1];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.relativeVelocity.magnitude > 3)
        {             
            print(collision.relativeVelocity.magnitude);
            TakeDamage();
        }

    }
}
