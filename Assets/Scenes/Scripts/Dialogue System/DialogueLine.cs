using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "剧情/对话")]

public class DialogueLine : ScriptableObject
{
    public CharacterData characterData;

    [TextArea(3, 10)] public string dialogueText; // 对话文本（多行）
}
