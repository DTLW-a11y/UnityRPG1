using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputTitleUI : MonoBehaviour
{
    string title = "风尘汞洞四海乱，英雄攘臂收功名";
    TextMeshProUGUI titletext;
    [SerializeField] GameObject titleline;
    RectTransform line;
    // Start is called before the first frame update
    void Start()
    {
        titletext = GetComponent<TextMeshProUGUI>();
        line = titleline.GetComponent<RectTransform>();
        line.sizeDelta = new Vector2(0, 10);
        titletext.text = " ";
        StartCoroutine(Typing());
        StartCoroutine(stretchline());
    }
    IEnumerator Typing()
    {
        yield return new WaitForSeconds(0.5f);
        float t = 0;
        int charIndex = 0;
        while (charIndex < title.Length)
        {
            t += Time.deltaTime * 7.0f;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, title.Length);
            titletext.text = title.Substring(0, charIndex);
            yield return null;
        }
        titletext.text = title;
        yield break;
    }
    IEnumerator stretchline()
    {
        yield return new WaitForSeconds(0.5f);
        float linelen = 0.0f;
        while (linelen <= 600.0f)
        {
            linelen += Time.deltaTime * 350.0f;
            line.sizeDelta = new Vector2(linelen, 10);
            yield return null;
        }
        line.sizeDelta = new Vector2 (600, 10);
        yield break;
    }
}
