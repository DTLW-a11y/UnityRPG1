using System;

[Serializable]

public class InventoryItem
{
    public ItemData ItemData;
    public int stacksize;

    public InventoryItem(ItemData _itemData)
    {
        ItemData = _itemData;
        Addstack();
    }
    public void Addstack() => stacksize++;

    public void Minstack() => stacksize--;
}
