using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_craftslot : UI_Itemslot
{
    private void OnEnable()
    {
        UpdateSlotUI(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        itemData_equipment itemCraft = item.ItemData as itemData_equipment;
        Inventory.Instance.CanCraft(itemCraft , itemCraft.CraftMaterials);
    }
}
