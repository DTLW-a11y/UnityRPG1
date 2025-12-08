using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 仓库
 * 单例，可在其他类随意调用
 * 
 * 
 */
public class Inventory : MonoBehaviour
{
    

    public static Inventory Instance;

    //装备
    public List<InventoryItem> equipments;
    public Dictionary<itemData_equipment, InventoryItem> equipmentDictionary;

    //
    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData,InventoryItem> inventoryDictionary;

    //材料
    public List<InventoryItem> StashItems;
    public Dictionary<ItemData,InventoryItem> StashDictionary;

    //初始物品
    public List<ItemData> startingItems;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventoryslotParent;
    [SerializeField] private Transform StashslotParent;
    [SerializeField] private Transform EquipmentSlotParent;
    private UI_Itemslot[] itemslots;
    private UI_Itemslot[] stashslots;
    private UI_equipmentslot[] equipmentslots;
    /*
     * 制作装备的材料在stash栏里
     * */

    //物品合成判断
    public bool CanCraft(itemData_equipment itemToCraft, List<InventoryItem> requirements) 
    {
        List<InventoryItem> itemsToRemove = new List<InventoryItem>();
        for (int i = 0; i < requirements.Count; i++)
        {
            if (StashDictionary.TryGetValue(requirements[i].ItemData, out var itemData))
            {
                if(itemData.stacksize < requirements[i].stacksize)
                {
                    Debug.Log("Not Enough Materials");
                    return false;
                }
                else
                {
                    itemsToRemove.Add(itemData);
                }
            }
            else
            {
                Debug.Log("Not Enough Materials");
                return false;
            }
        }
        for(int i=0; i < itemsToRemove.Count; i++)
        {
            RemoveItem(itemsToRemove[i].ItemData);
        }
        AddItem(itemToCraft);

        return true;
    }
    //更新ui
    private void UpdateUI()
    {
        for(int i=0; i<itemslots.Length; i++)
        {
            itemslots[i].ClanUpSlot();
        }
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            itemslots[i].UpdateSlotUI(inventoryItems[i]);
        }

        for (int i = 0; i < stashslots.Length; i++)
        {
            stashslots[i].ClanUpSlot();
        }
        for(int i=0; i< StashItems.Count; i++)
        {
            stashslots[i].UpdateSlotUI(StashItems[i]);
        }

        
        for(int i=0; i < equipmentslots.Length; i++)
        {
            foreach (KeyValuePair<itemData_equipment, InventoryItem> olditem in equipmentDictionary)
            {
                if (olditem.Key.equipmenttype == equipmentslots[i].slotType)
                {
                    equipmentslots[i].UpdateSlotUI(olditem.Value);
                }
            }
        }
    }
    private void Start()
    {
        equipments = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<itemData_equipment, InventoryItem>();
        equipmentslots = EquipmentSlotParent.GetComponentsInChildren<UI_equipmentslot>();

        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        itemslots = inventoryslotParent.GetComponentsInChildren<UI_Itemslot>();

        StashItems = new List<InventoryItem>();
        StashDictionary = new Dictionary<ItemData, InventoryItem>();
        stashslots = StashslotParent.GetComponentsInChildren<UI_Itemslot>();
        AddStartingItems();

    }

    private void AddStartingItems()
    {
        for (int i = 0; i < startingItems.Count; i++)
        {
            AddItem(startingItems[i]);
        }
    }

    public void equipitems(ItemData _item)
    {
        InventoryItem item = new InventoryItem(_item);
        itemData_equipment newequipment = _item as itemData_equipment;

        itemData_equipment itemToRemove = null;
        foreach(KeyValuePair<itemData_equipment,InventoryItem> olditem in equipmentDictionary)
        {
            if (olditem.Key.equipmenttype == newequipment.equipmenttype)
            {
                itemToRemove = olditem.Key;
                //Debug.Log(olditem.Key.ItemType.ToString() + newequipment.ItemType.ToString());
            }
        }
        if (itemToRemove != null)
        {
            Unequiped(itemToRemove);
            
            AddItem(itemToRemove);
        }
        equipments.Add(item);
        equipmentDictionary.Add(newequipment, item);
        newequipment.AddModifier();
        RemoveItem(_item);

        UpdateUI();
    }
    public void useitems(ItemData _item)
    {
        InventoryItem item = new InventoryItem(_item);

        RemoveItem(_item);//物品数量减少

        _item.ExecuteItemEffect();

        UpdateUI();
    }
    public void Unequiped(itemData_equipment itemToRemove)//取消装备物品，并且不回到仓库
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipments.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifier();
        }
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }   
    public void AddItem(ItemData _item)
    {
        if (_item.ItemType == ItemType.Equipment || _item.ItemType == ItemType.Item)//添加装备和消耗品
        {
        AddToInventory(_item);
        }
        else if( _item.ItemType == ItemType.Material)//添加材料
        {
        AddToStash(_item);
        }
        UpdateUI();
    }//添加物品并更新ui
    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.Addstack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            inventoryItems.Add(newitem);
            inventoryDictionary.Add(_item, newitem);
        }
    }
    private void AddToStash(ItemData _item)
    {
        if (StashDictionary.TryGetValue(_item, out InventoryItem stashvalue))
        {
            stashvalue.Addstack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            StashItems.Add(newitem);
            StashDictionary.Add(_item, newitem);
        }
    }
    public void RemoveItem(ItemData _item)
    {
        bool ismoved = false;
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if(value.stacksize <=1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            value.Minstack();
            ismoved = true;
        }
        if (StashDictionary.TryGetValue(_item, out InventoryItem stashvalue))
        {
            if (stashvalue.stacksize <= 1)
            {
                StashItems.Remove(stashvalue);
                StashDictionary.Remove(_item);
            }
            else
                stashvalue.Minstack();
            ismoved = true;
        }
        if(ismoved) 
        UpdateUI();
    }

    public List<InventoryItem> GetEquipmentList() => equipments;

    public List<InventoryItem> GetStashList() => StashItems;

    public itemData_equipment getEquipment(ItemType _type)//根据物品类型获得已装备物品
    {
        itemData_equipment equipedItem = null;
        foreach (var item in equipmentDictionary)
        {
            if (item.Key.ItemType == _type)
            {
                equipedItem = item.Key;
            }
        }
        return equipedItem;
    }
}
