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
            itemimage.sprite = item.ItemData.icon;
            if (item.stacksize > 1)
            {
                itemtext.text = item.stacksize.ToString();
            }
            else
            { itemtext.text = ""; }
        }
    }
    public void ClanUpSlot() //���ui��
    {
        item = null;
        itemimage.color = Color.clear;
        itemimage.sprite = null;
        itemtext.text = "";   
    }

    public virtual void OnPointerDown(PointerEventData eventData)//�������Ʒ��
    {
        if (Input.GetKey(KeyCode.LeftControl)) {
            Inventory.Instance.RemoveItem(item.ItemData);
                return;
        }
        if (item.ItemData.ItemType == ItemType.Equipment)//װ��
        {
            Inventory.Instance.equipitems(item.ItemData);
        }
        if (item.ItemData.ItemType == ItemType.Item)//ʹ������Ʒ
        {
            Inventory.Instance.useitems(item.ItemData);
        }
    }
}
