using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapIsOnTrigger : MonoBehaviour
{
    public TilemapRenderer targetTilemap;

    // 角色进入触发区域时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetTilemap.enabled = false; // 隐藏Tilemap
        }
    }

    // 角色离开触发区域时调用
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetTilemap.enabled = true; // 恢复显示Tilemap
        }
    }
}
