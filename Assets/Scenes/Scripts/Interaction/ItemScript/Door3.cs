using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
    [SerializeField] Transform PlaceToGo;
    bool first = false;
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
        
        
        if (TaskManager.instance.Find(3) == 1)
        {
            SpawnManager.position = PlaceToGo.position;
            GameManager.Instance.NormalJumpToScene("Village");
        }
    }
}