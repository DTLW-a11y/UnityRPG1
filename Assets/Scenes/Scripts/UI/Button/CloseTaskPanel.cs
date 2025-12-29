using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTaskPanel : MonoBehaviour
{
    [SerializeField] GameObject taskui;
    private TaskUI UI;
    // Start is called before the first frame update
    void Start()
    {
        UI = taskui.GetComponent<TaskUI>();
    }
    public void OnClick()
    {
        UI.targetx = -420;
    }
}
