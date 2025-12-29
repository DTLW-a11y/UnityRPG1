using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameSpot6 : MonoBehaviour,Interface
{
    private bool first = false;
    [SerializeField] private List<DialogueData> text;
    InterType Interface.GetType()
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
    public void ThingToDo()
    {
        if (TaskManager.instance.Find(4) == 1 && !first && TaskManager.instance.Find(7) == 0)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
            TaskManager.instance.AcceptTask(7);
            first = true;
        }
    }

    public void Text()
    {
        throw new System.NotImplementedException();
    }
}
