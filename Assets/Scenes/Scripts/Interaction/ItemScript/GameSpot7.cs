using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpot7 : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] Transform PlaceToGo;
    [SerializeField] string SceneName;
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
        if (TaskManager.instance.Find(8) == 1 && !first && TaskManager.instance.Find(9) == 0)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
            
            first = true;
        }
        else if(TaskManager.instance.Find(8) == 1 && first && TaskManager.instance.Find(9) == 0)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[1]);
            TaskManager.instance.AcceptTask(9);
        }
        
    }
}
