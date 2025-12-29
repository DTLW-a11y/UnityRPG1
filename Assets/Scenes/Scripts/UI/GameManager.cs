using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject cloth, text, showword;
    private RectTransform clothrec, textrec, wordrec;
    private UnityEngine.UI.Image clothcolor;
    private TextMeshProUGUI texttext, showtexttext;
    private bool isshowing = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        clothrec = cloth.GetComponent<RectTransform>();
        textrec = text.GetComponent<RectTransform>();
        clothcolor = cloth.GetComponent<UnityEngine.UI.Image>();
        texttext = text.GetComponent<TextMeshProUGUI>();
        wordrec = showword.GetComponent<RectTransform>();
        showtexttext = showword.GetComponent<TextMeshProUGUI>();
    }
    public bool HasPlayerName()
    {
        return !string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName", ""));
    }
    public void NormalJumpToScene(string SceneName)
    {
        StartCoroutine(ClothFadeIn(true, SceneName));
    }
    public void WaitingThenJump(float seconds, string SceneName)
    {
        if (isshowing) return;
        isshowing = true;
        StartCoroutine(WaitingThen(seconds, SceneName));
    }
    /*
    public void JumpToSceneWithData(string SceneName)
    {
        StartCoroutine(ClothFadeIn(false, SceneName));
    }
    */
    IEnumerator WaitingThen(float seconds, string SceneName)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(ClothFadeIn(true, SceneName));
    }
    IEnumerator ClothFadeIn(bool isnormal, string SceneName)
    {
        clothrec.anchoredPosition = Vector2.zero;
        float alpha = 0.0f;
        do
        {
            alpha += Time.deltaTime * 200.0f;
            clothcolor.color = new Color(0, 0, 0, alpha / 255.0f);
            yield return null;
        }
        while (alpha <= 255.0f) ;
        if (isnormal) StartCoroutine(JustJumpToScene(SceneName));
    }
    IEnumerator JustJumpToScene(string SceneName)
    {
        textrec.anchoredPosition = new Vector2(-120, 50);
        AsyncOperation loadscene = SceneManager.LoadSceneAsync(SceneName);
        while (!loadscene.isDone)
        {
            if (loadscene.progress <= 0.1f)
                texttext.text = "正在加载";
            else if (loadscene.progress <= 0.25f)
                texttext.text = "正在加载.";
            else if (loadscene.progress <= 0.4f)
                texttext.text = "正在加载..";
            else if (loadscene.progress <= 0.55f)
                texttext.text = "正在加载...";
            else if (loadscene.progress <= 0.7f)
                texttext.text = "正在加载....";
            else if (loadscene.progress <= 0.85f)
                texttext.text = "正在加载.....";
            else
                texttext.text = "正在加载......";
            yield return null;
        }
        yield return StartCoroutine(ClothFadeOut());
    }
    IEnumerator ClothFadeOut()
    {
        textrec.anchoredPosition = new Vector2(5000, 0);
        float alpha = 255.0f;
        do
        {
            alpha -= Time.deltaTime * 300.0f;
            clothcolor.color = new Color(0, 0, 0, alpha / 255.0f);
            yield return null;
        }
        while (alpha >= 0.0f);
        clothrec.anchoredPosition = new Vector2(5000, 0);
    }
    public void BlockWayAndShow(string showtext)
    {
        showtexttext.text = showtext;
        StartCoroutine(ShowBackground());
        return;
    }
    IEnumerator ShowBackground()
    {
        clothrec.anchoredPosition = Vector2.zero;
        float t = 0.0f;
        while (t <= 0.2f)
        {
            t += Time.deltaTime;
            clothcolor.color = new Color(0, 0, 0, t);
            yield return null;
        }
        wordrec.anchoredPosition = Vector2.zero;
        while (t <= 0.52f)
        {
            t += Time.deltaTime;
            clothcolor.color = new Color(0, 0, 0, 2.0f * t - 0.2f);
            yield return null;
        }
        yield return new WaitForSeconds(0.42f);
        StartCoroutine(FadeBackground(t));
        yield break;
    }
    IEnumerator FadeBackground(float t)
    {
        while (t >= 0.2f)
        {
            t -= Time.deltaTime;
            clothcolor.color = new Color(0, 0, 0, 2.0f * t - 0.2f);
            yield return null;
        }
        wordrec.anchoredPosition = new Vector2(5000, 0);
        while (t >= 0.0f)
        {
            t -= Time.deltaTime;
            clothcolor.color = new Color(0, 0, 0, t);
            yield return null;
        }
        clothrec.anchoredPosition = new Vector2(5000, 0);
        isshowing = false;
        yield break;
    }
}
