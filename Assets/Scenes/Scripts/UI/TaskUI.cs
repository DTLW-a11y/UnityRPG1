using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    [SerializeField] GameObject[] singletask = new GameObject[4];
    [SerializeField] GameObject[] taskstatusimg = new GameObject[4];
    [SerializeField] Sprite doneimg, notdoneimg;
    [SerializeField] GameObject warningimg;
    private RectTransform selfrec, warningrec;
    private RectTransform[] taskrec = new RectTransform[4];
    private Image[] status = new Image[4];
    public float targetx = -320.0f;
    private int[] findindex = { 5, 6, 8, 9 };
    private float warningx = -100.0f;
    // Start is called before the first frame update
    void Start()
    {
        selfrec = GetComponent<RectTransform>();
        warningrec = warningimg.GetComponent<RectTransform>();
        for (int i = 0; i < 4; i++)
        {
            taskrec[i] = singletask[i].GetComponent<RectTransform>();
            status[i] = taskstatusimg[i].GetComponent<Image>();
        }
        selfrec.anchoredPosition = new Vector2(-320.0f, 0);
        TaskManager.instance.OntaskProgressChange += updatetaskui;
        TaskManager.instance.OntaskStatuChange += updatetaskui;
        updatetaskui();
    }
    private void OnDestroy()
    {
        TaskManager.instance.OntaskProgressChange -= updatetaskui;
        TaskManager.instance.OntaskStatuChange -= updatetaskui;
    }
    void updatetaskui()
    {
        warningx = -100.0f;
        for (int i = 0; i < 4; i++)
        {
            switch(TaskManager.instance.Find(findindex[i]))
            {
                case 0:
                    taskrec[i].anchoredPosition = new Vector2(-500.0f, -260.0f * i - 130.0f);
                    break;
                case 1:
                    taskrec[i].anchoredPosition = new Vector2(320.0f, -260.0f * i - 130.0f);
                    status[i].sprite = doneimg;
                    break;
                default:
                    taskrec[i].anchoredPosition = new Vector2(320.0f, -260.0f * i - 130.0f);
                    status[i].sprite = notdoneimg;
                    warningx = 60.0f;
                    break;
            }
        }
        warningrec.anchoredPosition = new Vector2(warningx, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            targetx = (targetx == -320.0f) ? 340.0f : -320.0f;
        }
        selfrec.anchoredPosition = new Vector2(selfrec.anchoredPosition.x + (targetx - selfrec.anchoredPosition.x) * 2.6f * Time.deltaTime, 0);
    }
}
