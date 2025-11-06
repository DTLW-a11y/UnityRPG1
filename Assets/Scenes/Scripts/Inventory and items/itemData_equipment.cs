using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask

}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class itemData_equipment : ItemData
{
    public EquipmentType equipmenttype;
}
