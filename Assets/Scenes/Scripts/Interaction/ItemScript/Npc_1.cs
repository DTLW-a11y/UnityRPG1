using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_1 : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
    bool firstdialog = false;
    bool seconddialog = false;
    float time = 0;
    
    public  InterType GetType()
    {
        return InterType.npc;
    }
    
    public void Text()
    {
        
        //spriteRenderer.color = Color.yellow;
        // Debug.Log("text");//显示的文字，和对话系统接轨
    }

    public void ThingToDo()
    {

        if (!firstdialog && TaskManager.instance.Find(1) == 0)//第一次对话
        {
            //Debug.Log(0);
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
            firstdialog = true;
            TaskManager.instance.AcceptTask(1);//接取任务1
        }
        else if (TaskManager.instance.Find(1) == 1 && TaskManager.instance.Find(2) ==-1 && TaskManager.instance.Find(9) ==0)//已完成
        {
            
            int a = Random.Range(0, 10);
            if(a>=5)
                DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[1]);
            else
                DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[2]);
        }
        else if (TaskManager.instance.Find(9) == -1 )//9未完成
        {
            
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[3]);
            TaskManager.instance.UpdateProgress(tasktype.TalkToNPC, 801, 1);//完成任务9
            TaskManager.instance.AcceptTask(10);
        }
        else
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[4]);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer spriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (collision.GetComponent<Player>() != null) {
            Transform tip = transform.Find("Canvas");
            tip.gameObject.SetActive(true);
            spriteRenderer.color = Color.yellow;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        SpriteRenderer spriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (collision.GetComponent<Player>() != null)
        {
            Transform tip = transform.Find("Canvas");
            tip.gameObject.SetActive(false);
            spriteRenderer.color = Color.white;
        }
    }

}
