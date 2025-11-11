using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_equipmentslot : UI_Itemslot
{
    public EquipmentType slotType;
    private void OnValidate()
    {
        gameObject.name = "EquipSlot - " + slotType.ToString();
    }
}
