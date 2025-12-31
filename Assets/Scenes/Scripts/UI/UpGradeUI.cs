using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeUI : MonoBehaviour
{
    [SerializeField] GameObject expup, manaup;
    RectTransform exprec, manarec;
    Coroutine showexpcor = null, showmanacor = null;
    // Start is called before the first frame update
    void Start()
    {
        exprec = expup.GetComponent<RectTransform>();
        manarec = manaup.GetComponent<RectTransform>();
        EXPSystem.instance.onLevelUp += expshow;
        EXPSystem.instance.onmanaLevelUp += manashow;
    }
    private void OnDestroy()
    {
        EXPSystem.instance.onLevelUp -= expshow;
        EXPSystem.instance.onmanaLevelUp -= manashow;
    }
    void expshow()
    {
        if (showexpcor != null) StopCoroutine(showexpcor);
        showexpcor = StartCoroutine(ShowExp());
    }
    IEnumerator ShowExp()
    {
        float t = 0.0f;
        while (t <= 0.5f)
        {
            t += Time.deltaTime;
            exprec.anchoredPosition = new Vector2(0, - 260.0f + (0.5f - t) * (0.5f - t) * 1040.0f);
            yield return null;
        }
        t = 0.5f;
        yield return new WaitForSeconds(t);
        while (t >= 0.0f)
        {
            t -= Time.deltaTime;
            exprec.anchoredPosition = new Vector2(0, - 260.0f + (0.5f - t) * (0.5f - t) * 1040.0f);
            yield return null;
        }
        showexpcor = null;
    }
    void manashow()
    {
        if (showmanacor != null) StopCoroutine(showmanacor);
        showmanacor = StartCoroutine(ShowMana());
    }
    IEnumerator ShowMana()
    {
        float t = 0.0f;
        while (t <= 0.5f)
        {
            t += Time.deltaTime;
            manarec.anchoredPosition = new Vector2(0, -260.0f + (0.5f - t) * (0.5f - t) * 1040.0f);
            yield return null;
        }
        t = 0.5f;
        yield return new WaitForSeconds(t);
        while (t >= 0.0f)
        {
            t -= Time.deltaTime;
            manarec.anchoredPosition = new Vector2(0, -260.0f + (0.5f - t) * (0.5f - t) * 1040.0f);
            yield return null;
        }
        showmanacor = null;
    }
}
