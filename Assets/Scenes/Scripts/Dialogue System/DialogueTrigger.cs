using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("触发的剧情")]
    public DialogueData dialogueToTrigger;

    [Header("触发设置")]
    public bool triggerOnEnter = true; // 进入碰撞体触发
    public bool triggerOnClick = false; // 点击触发
    public bool oneTimeOnly = false; // 是否只触发一次

    private bool hasTriggered = false;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (triggerOnEnter && !hasTriggered && other.CompareTag("Player"))
    //    {
    //        TriggerDialogue();
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    if (triggerOnClick && !hasTriggered)
    //    {
    //        TriggerDialogue();
    //    }
    //}

    // 触发剧情
    public void TriggerDialogue(DialogueData dialogueData)
    {
        if (hasTriggered || DialogueManager.Instance.IsDialoguePlaying()) return;

        DialogueManager.Instance.StartDialogue(dialogueData);
        if (oneTimeOnly)
        {
            hasTriggered = true;
            gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // 重置触发状态（可选）
    public void ResetTrigger()
    {
        hasTriggered = false;
        GetComponent<Collider2D>().enabled = true;
    }
}