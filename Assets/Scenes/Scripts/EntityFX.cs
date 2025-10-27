using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    SpriteRenderer spriteRendererSR;
    [SerializeField] Material flashmat;
     Material originmat;

    private void Start()
    {
        spriteRendererSR = GetComponentInChildren<SpriteRenderer>();
        originmat = spriteRendererSR.material;
    }

    private IEnumerator FlashFX()
    {
        spriteRendererSR.material = flashmat;
        yield return new WaitForSeconds(.2f);
        spriteRendererSR.material = originmat;
    }
    private void RedColorBlink()
    {
        if(spriteRendererSR.color != Color.red) 
            spriteRendererSR.color = Color.red;
        else 
            spriteRendererSR.color =Color.white;
    }
    private void CancelRedBlink()
    {
        CancelInvoke();
        spriteRendererSR.color=Color.white;
    }
}
