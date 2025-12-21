using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpot : MonoBehaviour,Interface
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
        DialogueTrigger dialogueTrigger = gameObject.GetComponent<DialogueTrigger>();
        dialogueTrigger.TriggerDialogue();
    }
}
