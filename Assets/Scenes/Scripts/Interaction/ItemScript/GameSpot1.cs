using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpot1 : MonoBehaviour,Interface
{
    [SerializeField] private string text;
    public InterType GetType()
    {
        return InterType.gamespot;
    }
    public void Text()
    {
       // Debug.Log("text");//显示的文字，和对话系统接轨
    }

    public void ThingToDo()
    {
        TaskManager.instance.UpdateProgress(tasktype.Toplace, 1, 1);
        TaskManager.instance.AcceptTask(2);
    }
}
