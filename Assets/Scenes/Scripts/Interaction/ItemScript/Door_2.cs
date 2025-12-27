using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_2 : MonoBehaviour,Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
    bool first = false;
    public InterType GetType()
    {
        return InterType.elsespot;
    }
    public void Text()
    {
        // Debug.Log("text");//显示的文字，和对话系统接轨
    }

    public void ThingToDo()
    {
        if (TaskManager.instance.Find(2) == 0)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
        }
        else if(TaskManager.instance.Find(2) == 1 && !first)//完成第二个任务,回到刘伯家
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[1]);

            //切换到刘伯家
            first = true;
        }
    }
}
