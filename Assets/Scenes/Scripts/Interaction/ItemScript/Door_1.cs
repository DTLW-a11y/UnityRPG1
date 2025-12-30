using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_1 : MonoBehaviour,Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid; 
    [SerializeField] Transform PlaceToGo;
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
        if (TaskManager.instance.Find(1) == 1)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
        }
        else if(TaskManager.instance.Find(1) == -1)
        {
            SpawnManager.position = PlaceToGo.position;
            GameManager.Instance.NormalJumpToScene("Village");
        }
    }
}
