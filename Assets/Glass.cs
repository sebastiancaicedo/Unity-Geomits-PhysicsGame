using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : Destructible
{

    public override void TakeDamage()
    {
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        for (int index = 0; index < states.Length; index++)
        {
            spriteRenderer.sprite = states[index];
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }
}
