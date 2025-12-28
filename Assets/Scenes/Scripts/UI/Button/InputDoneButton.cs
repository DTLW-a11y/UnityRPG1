using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputDoneButton : MonoBehaviour
{
    [SerializeField] GameObject text, welcome, warning, input;
    RectTransform textrec, warnrec, welcomerec;
    TMP_InputField playername;
    // Start is called before the first frame update
    void Start()
    {
        textrec = text.GetComponent<RectTransform>();
        warnrec = warning.GetComponent<RectTransform>();
        welcomerec = welcome.GetComponent<RectTransform>();
        playername = input.GetComponent<TMP_InputField>();
        textrec.anchoredPosition = new Vector2(0, 5);
        warnrec.anchoredPosition = new Vector2(5000, -100);
        welcomerec.anchoredPosition = new Vector2(5000, -100);
    }
    public void OnClick()
    {
        textrec.anchoredPosition = new Vector2(0, -5);
        if (string.IsNullOrEmpty(playername.text))
        {
            warnrec.anchoredPosition = new Vector2(0, -100);
            welcomerec.anchoredPosition = new Vector2(5000, -100);
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", playername.text);
            warnrec.anchoredPosition = new Vector2(5000, -100);
            welcomerec.anchoredPosition = new Vector2(0, -100);
            //确定出生位置
            SpawnManager.position = new Vector2(-3, -7);
            GameManager.Instance.WaitingThenJump(0.6f, "Home");
        }
    }
    public void PointerUp()
    {
        textrec.anchoredPosition = new Vector2(0, 5);
    }
}
