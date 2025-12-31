using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using static UnityEditor.Progress;
#endif
using TMPro;
/*
 * �ֿ�
 * �����������������������
 * 
 * 
 */
public class Inventory : MonoBehaviour, ISaveManager
{
    

    public static Inventory Instance;

    //װ��
    public List<InventoryItem> equipments;
    public Dictionary<itemData_equipment, InventoryItem> equipmentDictionary;

    //
    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData,InventoryItem> inventoryDictionary;

    //����
    public List<InventoryItem> StashItems;
    public Dictionary<ItemData,InventoryItem> StashDictionary;
    //技能条件检索
    public List<InventoryItem> SkillItems;
    public Dictionary<ItemData, InventoryItem> SkillDictionary;

    //����Ʒ�������Ű�
    public Dictionary<int, ItemData> ItemDictionary;

    //��ʼ��Ʒ
    public List<ItemData> startingItems;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventoryslotParent;
    [SerializeField] private Transform StashslotParent;
    [SerializeField] private Transform EquipmentSlotParent;
    private UI_Itemslot[] itemslots;
    private UI_Itemslot[] stashslots;
    private UI_equipmentslot[] equipmentslots;

    [Header("Data base")]
    public List<ItemData> ItemDataBase;
    public List<InventoryItem> LoadedItems;
    public List<itemData_equipment> loadedEquipment;
    /*
     * ����װ���Ĳ�����stash����
     * */
    public void RefreshSceneReferences()
    {
        // 在新场景中查找ui
        inventoryslotParent = GameObject.Find("inventory").GetComponent<RectTransform>();
        StashslotParent = GameObject.Find("Stash").GetComponent<RectTransform>();
        EquipmentSlotParent = GameObject.Find("Equipment").GetComponent<RectTransform>();
        //清空ui槽
        itemslots = inventoryslotParent.GetComponentsInChildren<UI_Itemslot>();
        stashslots = StashslotParent.GetComponentsInChildren<UI_Itemslot>();
        equipmentslots = EquipmentSlotParent.GetComponentsInChildren<UI_equipmentslot>();
        UpdateUI();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        ItemDictionary = new Dictionary<int, ItemData>();
        LoadFileName();
    }   

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshSceneReferences();
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

        SkillItems = new List<InventoryItem>();
        SkillDictionary = new Dictionary<ItemData, InventoryItem>();
        AddStartingItems();

        

    }
    private void LoadFileName()
    {
        LoadAllFiles("ItemData/Consumables");
        LoadAllFiles("ItemData/Demands");
        LoadAllFiles("ItemData/Equipments");
        LoadAllFiles("ItemData/Materials");
    }
    private void LoadAllFiles(string file)
    {
        ItemData[] itemDatas = Resources.LoadAll<ItemData>(file);
        foreach (ItemData itemData in itemDatas)
        {
            if (ItemDictionary.ContainsKey(itemData.itemId))
            {
                Debug.Log("已有该物品");
                return;
            }
            else
            {
                Debug.Log(itemData.itemId);
                ItemDictionary.Add(itemData.itemId, itemData);
            }
        }
    }
    private void AddStartingItems()
    {
        foreach(itemData_equipment item in loadedEquipment)
        {
            AddItemByData(item);
        }
        for (int i = 0; i < startingItems.Count; i++)
        {
            AddItemByData(startingItems[i]);
        }
    }

    public bool CanCraft(itemData_equipment itemToCraft, List<InventoryItem> requirements)  //��Ʒ�ϳ��ж�
    {
        List<InventoryItem> itemsToRemove = new List<InventoryItem>();
        for (int i = 0; i < requirements.Count; i++)
        {
            if (StashDictionary.TryGetValue(requirements[i].ItemData, out var itemData))
            {
                if (itemData.stacksize < requirements[i].stacksize)
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
        for (int i = 0; i < itemsToRemove.Count; i++)
        {
            RemoveItem(itemsToRemove[i].ItemData);
        }
        AddItem(FindKeyByValue(itemToCraft));

        return true;
    }
    private void UpdateUI()    //����ui
    {
        for (int i = 0; i < itemslots.Length; i++)
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
        for (int i = 0; i < StashItems.Count; i++)
        {
            stashslots[i].UpdateSlotUI(StashItems[i]);
        }


        for (int i = 0; i < equipmentslots.Length; i++)
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
            
            AddItemByData(itemToRemove);
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

        RemoveItem(_item);//��Ʒ��������

        _item.ExecuteItemEffect();

        UpdateUI();
    }
    public void Unequiped(itemData_equipment itemToRemove)//ȡ��װ����Ʒ�����Ҳ��ص��ֿ�
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipments.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifier();
        }
    }
    public int FindKeyByValue(ItemData _item)
    {
        int resultkey;
        foreach(var kvp in ItemDictionary)
        {
            if(kvp.Value == _item)
            {
                resultkey = kvp.Key;
                return resultkey;
            }
        }
        return -1;
    }
    public void AddItemByData(ItemData _item)
    {
        //TaskManager.instance.UpdateProgress(tasktype.Collectitem, 201, 1);
        if (_item.ItemType == ItemType.Equipment || _item.ItemType == ItemType.Item)//����װ��������Ʒ
        {
            AddToInventory(_item);
        }
        else if (_item.ItemType == ItemType.Material)//���Ӳ���
        {
            AddToStash(_item);
        }
        else if (_item.ItemType == ItemType.Skill)//���Ӳ���
        {
            AddToSkill(_item);
        }
        UpdateUI();
    }//������Ʒ������ui
    public void AddItem(int itemID)
    {
        ItemData _item = ItemDictionary[itemID];
        TaskManager.instance.UpdateProgress(tasktype.Collectitem, itemID, 1);
        Debug.Log(_item.ItemType);
        if (_item.ItemType == ItemType.Equipment || _item.ItemType == ItemType.Item)
        {
            AddToInventory(_item);
        }
        else if (_item.ItemType == ItemType.Material)
        {
            AddToStash(_item);
        }
        else if (_item.ItemType == ItemType.Skill)//添加技能
        {
            Debug.Log(1);
            AddToSkill(_item);
        }
        UpdateUI();
    }
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
    private void AddToSkill(ItemData _item)
    {
        if (SkillDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.Addstack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            SkillItems.Add(newitem);
            SkillDictionary.Add(_item, newitem);
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

    public itemData_equipment getEquipment(EquipmentType _type)//根据装备类型查找装备
    {
        itemData_equipment equipedItem = null;
        foreach (var item in equipmentDictionary)
        {
            if (item.Key.equipmenttype == _type)
            {
                equipedItem = item.Key;
            }
        }
        return equipedItem;
    }

    public void LoadData(GameData _data)
    {
        //删除现有物品
        if (_data.inventory != null && _data.inventory.Count > 0)
        {
            //inventoryItems.Clear();
            //inventoryDictionary.Clear();
            //StashItems.Clear();
            //StashDictionary.Clear();
            //LoadedItems.Clear();
            // 加载存档物品
            foreach (KeyValuePair<string, int> pair in _data.inventory)
            {
                foreach (var item in ItemDataBase)
                {
                    if (item != null)
                    {
                        InventoryItem itemToLoad = new InventoryItem(item);
                        itemToLoad.stacksize = pair.Value;

                        LoadedItems.Add(itemToLoad);
                    }
                }
            }

            UpdateUI();
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        foreach(KeyValuePair<ItemData,InventoryItem>pair in inventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemId.ToString(), pair.Value.stacksize);
        }
    }
#if UNITY_EDITOR
    [ContextMenu("Fill up item data base")]
    private void FillUpItemDataBase() => ItemDataBase = new List<ItemData>(GetItemDataBase());
    private List<ItemData> GetItemDataBase()
    {
        List <ItemData>itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Resources/ItemData/Equipments" });
        
        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }
        return itemDataBase;
    }
#endif
}
