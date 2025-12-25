using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Characters", menuName = "角色")] 
public class CharacterData: ScriptableObject
{
    public string characterName; // 角色名
    public Sprite characterIcon; // 角色立绘（可选）
    public Color textColor = Color.white; // 文本颜色
}
