using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour, Interface
{
    [SerializeField] private List<DialogueData> text;
    [SerializeField] private List<int> taskid;
    [SerializeField] Transform PlaceToGo;
    [SerializeField] string SceneName;
    InterType Interface.GetType()
    {
        return InterType.elsespot;
    }
    public void Text()
    {
        // Debug.Log("text");//显示的文字，和对话系统接轨
    }
    public void Awake()
    {
        DialogueManager.Instance.OnDialogueEnd += ThingDo;
    }
    public void OnDestroy()
    {
        DialogueManager.Instance.OnDialogueEnd -= ThingDo;
    }
    public void ThingDo()
    {
            SpawnManager.position = PlaceToGo.position;
            GameManager.Instance.NormalJumpToScene(SceneName);
        
    }

    public void ThingToDo()
    {
        if (TaskManager.instance.Find(10) == 1)
        {
            DialogueManager.Instance.dialogueTrigger.TriggerDialogue(text[0]);
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TaskManager.instance.Find(10) == 1)
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
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (TaskManager.instance.Find(10) == 1)
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
}
