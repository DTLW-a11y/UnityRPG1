using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] GameObject leftbutton, rightbutton, bookleaf;
    private RectTransform leftrec, rightrec, bookrec;
    private bool IsMoving = false;
    private Coroutine showing = null, fading = null;
    // Start is called before the first frame update
    void Start()
    {
        leftrec = leftbutton.GetComponent<RectTransform>();
        rightrec = rightbutton.GetComponent<RectTransform>();
        bookrec = bookleaf.GetComponent<RectTransform>();
        leftrec.anchoredPosition = Vector2.zero;
        rightrec.anchoredPosition = Vector2.zero;
        bookrec.anchoredPosition = new Vector2(5000, 50.0f);
    }
    public void ClickBook()
    {
        if (IsMoving)
        {
            if (showing == null)
            {
                StopCoroutine(fading);
                fading = null;
                showing = StartCoroutine(ShowTwoButton());
            }
            else
            {
                StopCoroutine(showing);
                showing = null;
                fading = StartCoroutine(FadeTwoButton());
            }
        }
        else
        {
            IsMoving = true;
            if (bookrec.anchoredPosition.x > 2000.0f)
            {
                showing = StartCoroutine(ShowTwoButton());
                fading = null;
            }
            else
            {
                showing = null;
                fading = StartCoroutine(FadeTwoButton());
            }
        }
    }
    IEnumerator ShowTwoButton()
    {
        float t = 1.0f;
        while (t >= 0.0f)
        {
            t -= Time.deltaTime;
            leftrec.anchoredPosition = new Vector2(-220.0f + 220.0f * t * t * t, 0);
            rightrec.anchoredPosition = new Vector2(-120.0f + 120.0f * t * t * t, 0);
            bookrec.anchoredPosition = new Vector2(2500.0f * t * t, 50.0f);
            yield return null;
        }
        leftrec.anchoredPosition = new Vector2(-220.0f, 0);
        rightrec.anchoredPosition = new Vector2(-120.0f, 0);
        bookrec.anchoredPosition = new Vector2(0.0f, 50.0f);
        IsMoving = false;
        showing = null;
        yield break;
    }
    IEnumerator FadeTwoButton()
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime;
            leftrec.anchoredPosition = new Vector2(-220.0f + 220.0f * t * t * t, 0);
            rightrec.anchoredPosition = new Vector2(-120.0f + 120.0f * t * t * t, 0);
            bookrec.anchoredPosition = new Vector2(2500.0f * t * t * t, 50.0f);
            yield return null;
        }
        leftrec.anchoredPosition = new Vector2(0.0f, 0);
        rightrec.anchoredPosition = new Vector2(0.0f, 0);
        bookrec.anchoredPosition = new Vector2(5000.0f, 50.0f);
        IsMoving = false;
        fading = null;
        yield break;
    }
}
