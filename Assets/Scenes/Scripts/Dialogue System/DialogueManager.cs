using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public DialogueTrigger dialogueTrigger;

    [Header("依赖组件")]
    public DialogueUIManager dialogueUIManager;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    private bool isDialoguePlaying = false;

    [Header("需要隐藏的其他ui")]
    public GameObject[] gameObjects;

    public void RefreshSceneReferences()
    {
        // 在新场景中查找ui
        gameObjects[0] = GameObject.Find("HealthUI");
        gameObjects[1] = GameObject.Find("BagUI");
        //dialogpanel
        dialogueUIManager = GameObject.Find("UICanvas/DialoguePanel").GetComponent<DialogueUIManager>();
        dialogueUIManager.gameObject.SetActive(false);//默认隐藏
        //dialoguetrigger
        dialogueTrigger = GameObject.Find("dialoguetrigger").GetComponent<DialogueTrigger>();
    }

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void Start()
    {
        dialogueTrigger = GameObject.Find("dialoguetrigger").GetComponent<DialogueTrigger>();
        dialogueUIManager.gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshSceneReferences();
    }

    // 启动剧情
    public void StartDialogue(DialogueData dialogue)
    {
        Debug.Log("kaishi");
        if (isDialoguePlaying) return;
        Debug.Log("jieshu");

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
        Debug.Log("bofang");
        if (currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];
        Debug.Log("show");
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