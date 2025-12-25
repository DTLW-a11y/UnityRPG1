using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_1 : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
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
        bool firstdialog = false;
        bool seconddialog = false;
        if (!firstdialog)//第一次对话
        {
            //Debug.Log(0);
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
            firstdialog = true;
            TaskManager.instance.AcceptTask(1);//接取任务1
        }
        else if (TaskManager.instance.Find(taskid[0]) == 1)//已完成
        {
            Debug.Log(1);
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[1]);
        }
    }
}
