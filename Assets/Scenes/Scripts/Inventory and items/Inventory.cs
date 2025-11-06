using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData,InventoryItem> inventoryDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventoryslotParent;
    private UI_Itemslot[] itemslots;

    private void UpdateUI()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            itemslots[i].UpdateSlotUI(inventoryItems[i]);
        }
    }
    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData,InventoryItem>();
        itemslots = inventoryslotParent.GetComponentsInChildren<UI_Itemslot>();
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
        if(inventoryDictionary.TryGetValue(_item,out InventoryItem value))
        {
            value.Addstack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            inventoryItems.Add(newitem);
            inventoryDictionary.Add(_item,newitem);
        }
        Debug.Log("add" + _item.name);
        UpdateUI();
    }
    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if(value.stacksize <=1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            value.Minstack();
        }
        else
        {
            return;
        }
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
