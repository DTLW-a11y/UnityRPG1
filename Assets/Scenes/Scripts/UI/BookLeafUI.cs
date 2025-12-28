using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookLeafUI : MonoBehaviour
{
    [SerializeField] GameObject[] leaf = new GameObject[5];
    private int p = 0;
    private bool ismoving = false;
    private RectTransform[] leafrec = new RectTransform[5];
    // Start is called before the first frame update
    void Start()
    {
        leafrec[0] = leaf[0].GetComponent<RectTransform>();
        leafrec[0].anchoredPosition = new Vector2(0.0f, 0.0f);
        for (int i = 1;  i < 5; i++)
        {
            leafrec[i] = leaf[i].GetComponent<RectTransform>();
            leafrec[i].anchoredPosition = new Vector2(2500.0f, 0.0f);
        }
    }
    public void RightButton()
    {
        if (ismoving) return;
        if (p == 4) return;
        ismoving = true;
        p ++;
        StartCoroutine(GoLeft());
    }
    public void LeftButton()
    {
        if (ismoving) return;
        if (p == 0) return;
        ismoving = true;
        p--;
        StartCoroutine(GoRight());
    }
    IEnumerator GoLeft()
    {
        float t = 0.0f;
        while (t <= 0.5f)
        {
            t += Time.deltaTime;
            leafrec[p].anchoredPosition = new Vector2(2500.0f - 10000.0f * t * t, 0.0f);
            leafrec[p - 1].anchoredPosition = new Vector2(- 10000.0f * t * t, 0.0f);
            yield return null;
        }
        leafrec[p].anchoredPosition = new Vector2(0.0f, 0.0f);
        leafrec[p - 1].anchoredPosition = new Vector2(-2500.0f, 2000.0f);
        ismoving = false;
        yield break;
    }
    IEnumerator GoRight()
    {
        float t = 0.0f;
        while (t <= 0.5f)
        {
            t += Time.deltaTime;
            leafrec[p].anchoredPosition = new Vector2(-2500.0f + 10000.0f * t * t, 0.0f);
            leafrec[p + 1].anchoredPosition = new Vector2(10000.0f * t * t, 0.0f);
            yield return null;
        }
        leafrec[p].anchoredPosition = new Vector2(0.0f, 0.0f);
        leafrec[p + 1].anchoredPosition = new Vector2(2500.0f, 2000.0f);
        ismoving = false;
        yield break;
    }
}
