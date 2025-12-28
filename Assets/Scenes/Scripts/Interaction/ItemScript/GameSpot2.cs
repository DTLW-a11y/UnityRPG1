using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpot2 : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    bool first = false;
    public InterType GetType()
    {
        return InterType.gamespot;
    }
    public void Awake()
    {
        TaskManager.instance.OntaskStatuChange += ThingToDo;
    }
    public void OnDestroy()
    {
        TaskManager.instance.OntaskStatuChange -= ThingToDo;
    }
    public void Text()
    {
        // Debug.Log("text");//显示的文字，和对话系统接轨
    }

    public void ThingToDo()
    {
        if (TaskManager.instance.Find(2) == 1 && !first && TaskManager.instance.Find(3) == 0)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
            TaskManager.instance.AcceptTask(3);
            first = true;
        }
    }
}
