using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player Drop")]
    [SerializeField] float chanceToLoseItems;
    [SerializeField] float chanceToLoseMaterials;
    [SerializeField] List<ItemData> PlayerDrop;

    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.Instance;
        List<InventoryItem> currentitems = inventory.GetEquipmentList();
        List<InventoryItem> currentstash = inventory.GetStashList();

        List<InventoryItem> itemsToUnequip = new List<InventoryItem>();
        List<InventoryItem> stashToUnequip = new List<InventoryItem>();
        foreach (InventoryItem item in currentitems)
        {
            if(Random.Range(0,100) <= chanceToLoseItems)
            {
                DropItem(item.ItemData);
                itemsToUnequip.Add(item);
            }
        }
        foreach (InventoryItem item in currentstash)
        {
            if (Random.Range(0, 100) <= chanceToLoseMaterials)
            {
                DropItem(item.ItemData);
                stashToUnequip.Add(item);
            }
        }
        for (int i = 0;i<itemsToUnequip.Count;i++)
        {
            inventory.Unequiped(itemsToUnequip[i].ItemData as itemData_equipment);

        }
        for (int i = 0; i < stashToUnequip.Count; i++)
        {
            inventory.RemoveItem(stashToUnequip[i].ItemData);

        }
    }
}
