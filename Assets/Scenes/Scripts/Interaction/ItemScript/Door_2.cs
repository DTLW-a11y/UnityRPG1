using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_2 : MonoBehaviour,Interface
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
        if (TaskManager.instance.Find(2) == -1)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
        }
        else if(TaskManager.instance.Find(2) == 1 )//完成第二个任务,回到刘伯家
        {

            SpawnManager.position = PlaceToGo.position;
            GameManager.Instance.NormalJumpToScene("Home of Liu");
            first = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer spriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (collision.GetComponent<Player>() != null)
        {
            Transform tip = transform.Find("Canvas");
            tip.gameObject.SetActive(true);
            spriteRenderer.color = Color.yellow;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        SpriteRenderer spriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (collision.GetComponent<Player>() != null)
        {
            Transform tip = transform.Find("Canvas");
            tip.gameObject.SetActive(false);
            spriteRenderer.color = Color.white;
        }
    }
}
