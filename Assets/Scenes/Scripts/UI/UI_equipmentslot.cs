using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_equipmentslot : UI_Itemslot
{
    public EquipmentType slotType;
    private void OnValidate()
    {
        gameObject.name = "EquipSlot - " + slotType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item != null)
        {
            Inventory.Instance.Unequiped(item.ItemData as itemData_equipment);

            Inventory.Instance.AddItem(item.ItemData);
            ClanUpSlot();
        }
    }
}
