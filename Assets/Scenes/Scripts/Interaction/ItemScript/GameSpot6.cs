using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameSpot6 : MonoBehaviour
{
    private bool first = false;
    [SerializeField] private List<DialogueData> text;
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
}
