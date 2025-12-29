using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpot5 : MonoBehaviour, Interface
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
        if (TaskManager.instance.Find(8) == 0)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
        }
        else if (TaskManager.instance.Find(7) == 1 )
        {
            SpawnManager.position = PlaceToGo.position;
            GameManager.Instance.NormalJumpToScene(SceneName);
        }
    }
}