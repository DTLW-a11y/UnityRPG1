using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpot3 : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] Transform PlaceToGo;
    [SerializeField] string SceneName;
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
        if (TaskManager.instance.Find(3) == -1)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
        }
        else if(TaskManager.instance.Find(5) == -1)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[1]);
        }
        else if(TaskManager.instance.Find(3) == 1)//完成某任务，向右走
        {
            SpawnManager.position = PlaceToGo.position;
            GameManager.Instance.NormalJumpToScene(SceneName);
        }
    }
}
