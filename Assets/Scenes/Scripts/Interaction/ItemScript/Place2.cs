using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Place2 : MonoBehaviour, Interface
{
    bool first = false;
    InterType Interface.GetType()
    {
        return InterType.gamespot;
    }
    public void Text()
    {
        // Debug.Log("text");//显示的文字，和对话系统接轨
    }

    public void ThingToDo()
    {
        if (TaskManager.instance.Find(8) == -1 && !first && TaskManager.instance.Find(9) == 0)
        {
            //DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
            TaskManager.instance.UpdateProgress(tasktype.Toplace,2,1);
            Debug.Log("完成任务8");
            //TaskManager.instance.AcceptTask(9);
            first = true;
        }
        

    }
}
