using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("依赖组件")]
    public DialogueUIManager dialogueUIManager;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    private bool isDialoguePlaying = false;

    [Header("需要隐藏的其他ui")]
    public GameObject[] gameObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 启动剧情
    public void StartDialogue(DialogueData dialogue)
    {
        if (isDialoguePlaying) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialoguePlaying = true;
        Time.timeScale = 0f; // 暂停游戏
        foreach (GameObject ui in gameObjects)
        {
            ui.SetActive(false);
        }
        PlayCurrentLine();
    }

    // 播放当前行
    private void PlayCurrentLine()
    {
        if (currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];
        dialogueUIManager.ShowDialogue(currentLine);

        // 自动下一步逻辑（保留）
        if (currentDialogue.autoNext)
        {
            StartCoroutine(AutoNextLineCoroutine(currentDialogue.autoNextDelay));
        }
    }

    // 自动下一步协程
    private IEnumerator AutoNextLineCoroutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        NextDialogueLine();
    }

    // 下一步对话
    public void NextDialogueLine()
    {
        if (!isDialoguePlaying) return;

        currentLineIndex++;
        PlayCurrentLine();
    }

    // 结束剧情
    private void EndDialogue()
    {
        isDialoguePlaying = false;
        dialogueUIManager.HideDialogue();
        Time.timeScale = 1f; // 恢复游戏
        foreach (GameObject ui in gameObjects)
        {
            ui.SetActive(true);
        }
        Debug.Log("剧情结束");
    }

    // 跳过剧情
    public void SkipDialogue()
    {
        if (isDialoguePlaying)
        {
            currentLineIndex = currentDialogue.dialogueLines.Length;
            EndDialogue();
        }
    }

    // 获取剧情播放状态
    public bool IsDialoguePlaying()
    {
        return isDialoguePlaying;
    }
}