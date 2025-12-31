using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public event Action AfterChange;

    public static GameManager Instance;
    [SerializeField] GameObject cloth, text;
    private RectTransform clothrec, textrec;
    private UnityEngine.UI.Image clothcolor;
    private TextMeshProUGUI texttext;
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
        textrec.anchoredPosition = new Vector2(-150, 30);
        AsyncOperation loadscene = SceneManager.LoadSceneAsync(SceneName);
        while (!loadscene.isDone)
        {
            texttext.text = $"ÕýÔÚ¼ÓÔØ£º{loadscene.progress * 100.0f}%";
            yield return null;
        }
        yield return StartCoroutine(ClothFadeOut());
    }
    IEnumerator ClothFadeOut()
    {
        AfterChange?.Invoke();
        AfterChange = null;
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
}
