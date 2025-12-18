using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TreasureChest : MonoBehaviour, Interface
{
    public bool IsOpened = false;
    [SerializeField] Sprite opened;
    SpriteRenderer origin;

    public void Start()
    {
        origin = GetComponent<SpriteRenderer>();
    }
    public void Text()
    {
        throw new System.NotImplementedException();
    }

    public void ThingToDo()
    {
        if(!IsOpened)
        {
            ItemDrop itemDrop = gameObject.GetComponent<ItemDrop>();
            itemDrop.GenerateDrop();
            origin.sprite = opened;
            IsOpened = true;
        }
    }

    InterType Interface.GetType()
    {
        return InterType.elsespot;
    }
}
