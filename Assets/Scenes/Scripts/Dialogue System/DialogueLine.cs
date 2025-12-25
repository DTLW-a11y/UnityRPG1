using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "剧情/对话")]

public class DialogueLine : ScriptableObject
{
    public string characterName; // 角色名
    public Sprite characterIcon; // 角色立绘（可选）
    [TextArea(3, 10)] public string dialogueText; // 对话文本（多行）
    public Color textColor = Color.white; // 文本颜色
}
