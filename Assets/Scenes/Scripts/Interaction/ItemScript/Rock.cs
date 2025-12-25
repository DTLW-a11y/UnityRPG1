using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, Interface
{
    SpriteRenderer spriteRenderer;
    float flashtime = .3f;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Text()
    {
        throw new System.NotImplementedException();
    }

    public void ThingToDo()
    {
        StartCoroutine(enumerator());
    }

    InterType Interface.GetType()
    {
        return InterType.elsespot;
    }
    private IEnumerator enumerator()
    {
        Color origin = spriteRenderer.color;

        spriteRenderer.color = new Color(origin.r, origin.g, origin.b, .5f);
        for (int i = 0; i <= 3; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashtime);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashtime + 0.1f);
            //flashtime -= .03f;
            //Debug.Log(flashtime);
        }
        Destroy(gameObject);
    }
}
