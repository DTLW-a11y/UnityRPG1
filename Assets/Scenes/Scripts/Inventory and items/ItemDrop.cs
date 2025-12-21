using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����ڵ������ϣ�����������Ʒ
 * Inspector ����possibledrop
 */
public class ItemDrop : MonoBehaviour
{
    [Header("��������Ʒ����")]
    [SerializeField] private int possibleitemDrop;
    [Header("�ɵ�����Ʒ�б�")]
    [SerializeField] private ItemData[] possibledrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        for (int i = 0; i < possibledrop.Length; i++)//�ӿ��ܵ�����Ʒ��������ɵ����б�
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
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);//��Ԥ���崴���µ���Ϸ����

        Vector2 randomVelocity = new Vector2(Random.Range(-5 , 5) , Random.Range(12 , 15));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
