using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Itemslot : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] Image itemimage;
    [SerializeField] TextMeshProUGUI itemtext;

    public InventoryItem item;
    public void UpdateSlotUI(InventoryItem _item)
    {
        item = _item;
        itemimage.color = Color.white;
        if (item != null)
        {
            //Debug.Log("1");
            itemimage.sprite = item.ItemData.icon;
            if (item.stacksize > 1)
            {
                itemtext.text = item.stacksize.ToString();
            }
            else
            { itemtext.text = ""; }
        }
    }
    public void ClanUpSlot()
    {
        item = null;
        itemimage.color = Color.clear;
        itemimage.sprite = null;
        itemtext.text = "";   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item.ItemData.ItemType == ItemType.Equipment)
        {
            Inventory.Instance.equipitems(item.ItemData);
        }
    }
}
