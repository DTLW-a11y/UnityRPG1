using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]ItemData itemdata;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 velocity;


    private void SetUpVisuals()
    {
        if (itemdata == null)
            return;
        GetComponent<SpriteRenderer>().sprite = itemdata.icon;
        gameObject.name = "item object - " + itemdata.itemname;
    }

    public void SetupItem(ItemData _itemdata, Vector2 _velocity)
    {
        itemdata = _itemdata;
        rb.velocity = _velocity;

        SetUpVisuals();
    }

    public void PickItem()
    {
        Inventory.Instance.AddItem(itemdata);
        Destroy(gameObject);
    }
}
