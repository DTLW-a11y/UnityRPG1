
using UnityEditor;
using UnityEngine;
public enum ItemType
{
    Material,
    Equipment
}
[CreateAssetMenu(fileName = "New Item Data", menuName ="Data/Item")]

public class ItemData : ScriptableObject
{
    public ItemType ItemType;
    public string itemname;
    public string itemId;
    public Sprite icon;
    [Range(0, 100)]
    public float dropChance;

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
