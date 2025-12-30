using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueUIManager : MonoBehaviour
{
    [Header("UI组件")]
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Image characterIcon;
    public Button nextButton;
    public GameObject dialoguePanel;
    public string playername;

    private void Awake()
    {
        //dialoguePanel.SetActive(false);
        //nextButton.onClick.AddListener(OnNextButtonClick);
        playername = "张大帅";
        dialoguePanel = gameObject;
    }
    private void Update()
    {
        // 只有对话面板显示时，才检测点击
        if (dialoguePanel.activeSelf && DialogueManager.Instance.IsDialoguePlaying())
        {
            // 检测鼠标左键点击（或手机触屏点击）
            if (Input.GetMouseButtonDown(0))
            {
                // 触发下一步对话（直接调用，绕开按钮事件）
                DialogueManager.Instance.NextDialogueLine();
                Debug.Log("点击屏幕触发下一步对话");
            }
        }
    }
    private void Start()
    {
        //dialoguePanel.SetActive(false);
    }

    // 显示单句对话（直接完整显示文本）
    public void ShowDialogue(DialogueLine line)
    {
        dialoguePanel.SetActive(true);Debug.Log(dialoguePanel.name);
        characterNameText.text = line.characterData.characterName;
        characterNameText.color = line.characterData.textColor;
        characterIcon.sprite = line.characterData.characterIcon;
        characterIcon.gameObject.SetActive(line.characterData.characterIcon != null);
        dialogueText.text = line.dialogueText.Replace("{主角}",playername); // 直接显示完整文本
        characterNameText.color = line.characterData.textColor.a < 0.1f ? Color.black : line.characterData.textColor;
    }

    // 隐藏对话框
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    // 下一步按钮点击
    public void OnNextButtonClick()
    {
        Debug.Log(0);
        DialogueManager.Instance.NextDialogueLine();
    }
}