using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint: MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
    bool firstdialog = false;
    bool seconddialog = false;
    public string checkpointId = "checkpoint_001";
    public bool isActivated = false;
    public bool IsActivated => isActivated;
    private bool IsInRange = false;
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
        isActivated = true;
        IsInRange = false;
        if (CheckpointManager.instance != null)
        {
            CheckpointManager.instance.ActivateChecpoint(checkpointId, transform.position);
        }
    }
    public void SetActivated(bool activated)
    {
        isActivated = activated;
    }
}

