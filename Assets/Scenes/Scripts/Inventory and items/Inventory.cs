using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 仓库
 * 
 * 
 */
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> equipments;
    public Dictionary<itemData_equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData,InventoryItem> inventoryDictionary;

    public List<InventoryItem> StashItems;
    public Dictionary<ItemData,InventoryItem> StashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventoryslotParent;
    [SerializeField] private Transform StashslotParent;
    [SerializeField] private Transform EquipmentSlotParent;
    private UI_Itemslot[] itemslots;
    private UI_Itemslot[] stashslots;
    private UI_equipmentslot[] equipmentslots;

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
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData,InventoryItem>();
        itemslots = inventoryslotParent.GetComponentsInChildren<UI_Itemslot>();
        StashItems = new List<InventoryItem>();
        StashDictionary = new Dictionary<ItemData,InventoryItem>();
        stashslots = StashslotParent.GetComponentsInChildren<UI_Itemslot>();
        equipmentslots = EquipmentSlotParent.GetComponentsInChildren<UI_equipmentslot>();
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

    public void Unequiped(itemData_equipment itemToRemove)
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
        if (_item.ItemType == ItemType.Material)
        {
        AddToInventory(_item);

        }
        else if( _item.ItemType == ItemType.Equipment)
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ItemData newitem = inventoryItems[inventoryItems.Count-1].ItemData;
            RemoveItem(newitem);
        }
    }
}
