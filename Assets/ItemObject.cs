using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField]ItemData itemdata;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = itemdata.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Inventory.Instance.AddItem(itemdata);
            Destroy(gameObject);
        }
    }
}
