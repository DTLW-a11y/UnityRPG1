using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour, Interface
{
    
    public void Text()
    {
        throw new System.NotImplementedException();
    }

    public void ThingToDo()
    {
        if(TaskManager.instance.Find(6) == 1)
        {
            Inventory.Instance.AddItem(101);
        }
    }

    InterType Interface.GetType()
    {
        return InterType.elsespot;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TaskManager.instance.Find(6) == 1 && TaskManager.instance.Find(4) == -1)
        {
            SpriteRenderer spriteRenderer;
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (collision.GetComponent<Player>() != null)
            {
                Transform tip = transform.Find("Canvas");
                tip.gameObject.SetActive(true);
                spriteRenderer.color = Color.yellow;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (TaskManager.instance.Find(6) == 1 )
        {
            SpriteRenderer spriteRenderer;
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (collision.GetComponent<Player>() != null)
            {
                Transform tip = transform.Find("Canvas");
                tip.gameObject.SetActive(false);
                spriteRenderer.color = Color.white;
            }
        }
    }
}
