using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint: MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
    bool firstdialog = false;
    bool seconddialog = false;
    public string checkpointId = "checkpoint_001";
    public bool isActivated = false;
    public string SceneName;
    public bool IsActivated => isActivated;
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
        SceneName = SceneManager.GetActiveScene().name;
        if (CheckpointManager.instance != null)
        {
            CheckpointManager.instance.ActivateChecpoint(checkpointId, transform.position,SceneName);
        }
    }
    public void SetActivated(bool activated)
    {
        isActivated = activated;
    }
}

