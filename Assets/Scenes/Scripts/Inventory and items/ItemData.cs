
using UnityEditor;
using UnityEngine;
public enum ItemType
{
    Material,
    Equipment,
    Item
}
[CreateAssetMenu(fileName = "New Item Data", menuName ="Data/Item")]

public class ItemData : ScriptableObject
{
    public int itemId;
    public ItemEffect[] itemEffects;

    public ItemType ItemType;
    public string itemname;
    public Sprite icon;
    [Range(0, 100)]
    public float dropChance;

    public void ExecuteItemEffect()
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect();
        }
    }


    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
