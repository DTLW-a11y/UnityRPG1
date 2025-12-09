using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 拾取物品功能
 * 挂载游戏对象层级应设为default
 */
public class ObjectTrigger : MonoBehaviour
{
    ItemObject ItemObject => GetComponentInParent<ItemObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isdied)
                return;
            ItemObject.PickItem();
        }
    }
}
