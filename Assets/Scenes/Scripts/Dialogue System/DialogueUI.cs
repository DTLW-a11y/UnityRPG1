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

    private void Awake()
    {
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    // 显示单句对话（直接完整显示文本）
    public void ShowDialogue(DialogueLine line)
    {
        dialoguePanel.SetActive(true);
        characterNameText.text = line.characterName;
        characterNameText.color = line.textColor;
        characterIcon.sprite = line.characterIcon;
        characterIcon.gameObject.SetActive(line.characterIcon != null);
        dialogueText.text = line.dialogueText; // 直接显示完整文本
        characterNameText.color = line.textColor.a < 0.1f ? Color.black : line.textColor;
    }

    // 隐藏对话框
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    // 下一步按钮点击
    private void OnNextButtonClick()
    {
        DialogueManager.Instance.NextDialogueLine();
    }
}