using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 挂载在敌人身上，决定掉落物品
 * Inspector 调整possibledrop
 */
public class ItemDrop : MonoBehaviour
{
    [Header("最大掉落物品数量")]
    [SerializeField] private int possibleitemDrop;
    [Header("可掉落物品列表")]
    [SerializeField] private ItemData[] possibledrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        for (int i = 0; i < possibledrop.Length; i++)//从可能掉落物品里随机生成掉落列表
        {
            if(Random.Range(0,100) < possibledrop[i].dropChance)
                dropList.Add(possibledrop[i]);
        }
        
        for(int i = 0;i < possibleitemDrop; i++)
        {
            if (dropList.Count > 0)
            {
                ItemData randomitem = dropList[Random.Range(0, dropList.Count - 1)];
                dropList.Remove(randomitem);
                DropItem(randomitem);
            }
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);//用预制体创建新的游戏对象

        Vector2 randomVelocity = new Vector2(Random.Range(-5 , 5) , Random.Range(12 , 15));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
