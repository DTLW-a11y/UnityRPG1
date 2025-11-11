using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]ItemData itemdata;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemdata.icon;
        gameObject.name = "item object - " + itemdata.itemname;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            //Debug.Log("impicked");
            Inventory.Instance.AddItem(itemdata);
            Destroy(gameObject);
        }
    }
}
