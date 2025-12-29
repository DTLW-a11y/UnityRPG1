using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TaskUI : MonoBehaviour
{
    [SerializeField] GameObject[] singletask = new GameObject[4];
    [SerializeField] GameObject[] taskstatusimg = new GameObject[4];
    [SerializeField] Sprite doneimg;
    private RectTransform selfrec;
    private RectTransform[] taskrec = new RectTransform[4];
    private Image[] status = new Image[4];
    public float targetx = -420;
    // Start is called before the first frame update
    void Start()
    {
        selfrec = GetComponent<RectTransform>();
        for (int i = 0; i < 4; i ++)
        {
            taskrec[i] = singletask[i].GetComponent<RectTransform>();
            status[i] = taskstatusimg[i].GetComponent<Image>();
        }
        selfrec.anchoredPosition = new Vector2(-420.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            targetx = (targetx == -420.0f) ? 340.0f : -420.0f;
        }
        selfrec.anchoredPosition = new Vector2(selfrec.anchoredPosition.x + (targetx - selfrec.anchoredPosition.x) * 2.6f * Time.deltaTime, 0);
    }
}
