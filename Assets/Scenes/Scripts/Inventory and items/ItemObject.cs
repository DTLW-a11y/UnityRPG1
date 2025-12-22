using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]ItemData itemdata;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 velocity;

    private void OnValidate()
    {
        // ȷ��������SpriteRenderer�����û�����Զ����ӣ�
        if (GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }
        // �����Ӿ������߼�
        SetUpVisuals();

        // ��ѡ���Զ���rb��ֵ�������ֶ���ѡ��
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
    private void SetUpVisuals()
    {
        if (itemdata == null)
            return;
        GetComponent<SpriteRenderer>().sprite = itemdata.icon;
        gameObject.name = "item object - " + itemdata.itemname;
    }

    public void SetupItem(ItemData _itemdata, Vector2 _velocity)
    {
        itemdata = _itemdata;
        rb.velocity = _velocity;

        SetUpVisuals();
    }

    public void PickItem()
    {
        int itemId = Inventory.Instance.FindKeyByValue(itemdata);
        Inventory.Instance.AddItem(itemId);
        Destroy(gameObject);
    }
}
