using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UI_Itemslot : MonoBehaviour
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

}
