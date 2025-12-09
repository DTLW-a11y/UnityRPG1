using UnityEngine;

// 单句对话的数据结构
[System.Serializable]
// 整段剧情的配置文件
[CreateAssetMenu(fileName = "NewDialogue", menuName = "剧情/对话数据")]


public class DialogueLine
{
    public string characterName; // 角色名
    public Sprite characterIcon; // 角色立绘（可选）
    [TextArea(3, 10)] public string dialogueText; // 对话文本（多行）
    public Color textColor = Color.white; // 文本颜色
}

public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines; // 该段剧情的所有对话
    public bool autoNext = false; // 是否自动播放（默认手动点击下一步）
    public float autoNextDelay = 2f; // 自动播放延迟
}