using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_craftslot : UI_Itemslot
{
    [SerializeField] public Image[] Materialimage = new Image[3];
    [SerializeField] public Image CanORnotIMG;
    [SerializeField] public Sprite cando, cannotdo;
    [SerializeField] public TextMeshProUGUI[] matnum = new TextMeshProUGUI[3];
    private void OnEnable()
    {
        UpdateSlotUI(item);
    }
    private void Start()
    {
        itemData_equipment materials = item.ItemData as itemData_equipment;
        int i = 0;
        foreach (InventoryItem matitem in materials.CraftMaterials)
        {
            Materialimage[i].color = Color.white;
            Materialimage[i].sprite = matitem.ItemData.icon;
            matnum[i].text = matitem.stacksize.ToString();
            i++;
        }
    }
    private void Update()
    {
        itemData_equipment itemCraft = item.ItemData as itemData_equipment;
        CanORnotIMG.color = Color.white;
        foreach (InventoryItem i in itemCraft.CraftMaterials)
        {
            if (Inventory.Instance.StashDictionary.TryGetValue(i.ItemData, out var itemdata))
            {
                if (itemdata.stacksize < i.stacksize)
                {
                    CanORnotIMG.sprite = cannotdo;
                    return;
                }
                else
                {
                    CanORnotIMG.color = Color.green;
                    CanORnotIMG.sprite = cando;
                }
            }
            else
            {
                CanORnotIMG.sprite = cannotdo;
                return;
            }
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        itemData_equipment itemCraft = item.ItemData as itemData_equipment;
        Inventory.Instance.CanCraft(itemCraft, itemCraft.CraftMaterials);
    }
}
