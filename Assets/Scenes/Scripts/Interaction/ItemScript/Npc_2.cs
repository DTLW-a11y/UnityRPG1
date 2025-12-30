using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_2 : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
    bool firstdialog = false;
    bool seconddialog = false;
    public InterType GetType()
    {
        return InterType.npc;
    }
    public void Text()
    {
        // Debug.Log("text");//显示的文字，和对话系统接轨
    }

    public void ThingToDo()
    {

        if (!firstdialog&& TaskManager.instance.Find(3) == -1)//任务3未完成
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
            firstdialog = true;
            TaskManager.instance.UpdateProgress(tasktype.TalkToNPC, 802, 1);//完成任务3
            TaskManager.instance.AcceptTask(4);//接取任务4
        }
        else if(TaskManager.instance.Find(3) == -1 && TaskManager.instance.Find(4) == -1)//对话过1次
        {
            int a = Random.Range(0, 10);
            if (a >= 5)
                DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[1]);
            else
                DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[2]);
        }
        else if(TaskManager.instance.Find(7) == -1 )//拿取工具后
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[3]);
            TaskManager.instance.UpdateProgress(tasktype.TalkToNPC, 802, 1);//完成任务7
            TaskManager.instance.AcceptTask(8);//接取任务8
        }
    }
}

