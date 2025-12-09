using System.Collections.Generic;
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
    //public ItemEffect[] itemEffects;
    public EquipmentType equipmenttype;

    [Header("Base info")]
    public int strenth;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Defense info")]
    public int maxHP;
    public int armor;
    public int evision;

    [Header("Attack info")]
    public int damage;
    public int critchance;
    public int critpower;

    [Header("Magic info")]
    public int firedamage;
    public int icedamage;
    public int lightingdamage;

    [Header("Craft Requirements")]
    public List<InventoryItem> CraftMaterials;

    public void AddModifier()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.strenth.addmodifier(strenth);
        playerStats.agility.addmodifier(agility);
        playerStats.intelligence.addmodifier(intelligence);
        playerStats.vitality.addmodifier(vitality);

        playerStats.maxHP.addmodifier(maxHP);
        playerStats.armor.addmodifier(armor);
        playerStats.evision.addmodifier(evision);

        playerStats.damage.addmodifier(damage);
        playerStats.critpower.addmodifier(critpower);
        playerStats.critchance.addmodifier(critchance);
    }
    public void RemoveModifier()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.strenth.removemodifier(strenth);
        playerStats.agility.removemodifier(agility);
        playerStats.intelligence.removemodifier(intelligence);
        playerStats.vitality.removemodifier(vitality);

        playerStats.maxHP.removemodifier(maxHP);
        playerStats.armor.removemodifier(armor);
        playerStats.evision.removemodifier(evision);

        playerStats.damage.removemodifier(damage);
        playerStats.critpower.removemodifier(critpower);
        playerStats.critchance.removemodifier(critchance);
    }
    //public void ExecuteItemEffect()
    //{
    //    foreach (var item in itemEffects)
    //    {
    //        item.ExecuteEffect();
    //    }
    //}
}
