using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TreasureChest : MonoBehaviour, Interface,ISaveManager
{
    public bool IsOpened = false;
    [SerializeField]public string chestId;
    [SerializeField] Sprite opened;
    SpriteRenderer origin;

    public void LoadData(GameData _data)
    {
        if (_data.openedChests.TryGetValue(chestId, out bool opened))
        {
            IsOpened = opened;
        }
    }
    public void SaveData(ref GameData _data)
    {
        if (_data.openedChests.ContainsKey(chestId))
            _data.openedChests[chestId] = IsOpened;
        else
            _data.openedChests.Add(chestId, IsOpened);
    }

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
