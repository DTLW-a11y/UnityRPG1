using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipIntroButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject contenttext, background;
    [SerializeField] Sprite[] backgroundimage = new Sprite[3];
    TextMeshProUGUI text, selftext;
    Image backgroundimg;
    private float fsizemin = 20.0f, fsizemax = 24.0f;
    private float fsize = 20.0f, Speed = 15.0f;
    private int ci = -1;
    private bool istyping = false;
    private Coroutine coroutinet = null, coroutinei = null;
    private string[] content = {
    "      你生活在一个小村庄里，父亲是村庄的守卫，本来大家过着和平安定的生活。",
    "      但是突然有一天，村东的洞穴深处出现了一条时空裂缝，将一些来自另一个世界的魔物带到了这个世界。",
    "      魔物袭击了村庄并抢走了村里的许多物资，父亲也在与魔物的战斗中不幸身亡。",
    "      从此，村庄陷入了魔物的阴影中，它们时常来袭击村庄掠夺物资，搅得大家苦不堪言。"
    };
    // Start is called before the first frame update
    void Start()
    {
        selftext = GetComponent<TextMeshProUGUI>();
        text = contenttext.GetComponent<TextMeshProUGUI>();
        backgroundimg = background.GetComponent<Image>();
        NextText();
    }

    // Update is called once per frame
    void Update()
    {
        selftext.fontSize += (fsize - selftext.fontSize) * 7.2f * Time.deltaTime;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        fsize = fsizemax;
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        fsize = fsizemin;
    }
    public void NextText()
    {
        selftext.fontSize = fsizemin;
        if (selftext.text == "开始游戏")
        {
            SceneManager.LoadScene("InputNameScene");
            return;
        }
        if (istyping)
        {
            StopCoroutine(coroutinet);
            if (ci >= 0 && ci <= 2)
            {
                StopCoroutine(coroutinei);
                backgroundimg.sprite = backgroundimage[ci];
                backgroundimg.color = Color.white;
            }
            istyping = false;
            text.text = content[ci];
            if (ci == content.Length - 1)
            {
                selftext.text = "开始游戏";
            }
        }
        else
        {
            ci++;
            if (ci >= content.Length) return;
            coroutinet = StartCoroutine(Typing());
            if (ci >= 0 && ci <= 2) coroutinei = StartCoroutine(backgroundfade());
        }
    }
    IEnumerator Typing()
    {
        istyping = true;
        float t = 0;
        int charIndex = 0;
        while (charIndex < content[ci].Length)
        {
            t += Time.deltaTime * Speed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, content[ci].Length);
            text.text = content[ci].Substring(0, charIndex);
            yield return null;
        }
        text.text = content[ci];
        if (ci == content.Length - 1)
        {
            selftext.text = "开始游戏";
        }
        istyping = false;
        yield break;
    }
    IEnumerator backgroundfade()
    {
        float a = 300.0f;
        while (a >= 0.0f)
        {
            a -= Time.deltaTime * 200.0f;
            backgroundimg.color = new Color(1, 1, 1, Mathf.Clamp01(a / 300.0f));
            yield return null;
        }
        a = 0.0f;
        backgroundimg.sprite = backgroundimage[ci];
        while (a <= 300.0f)
        {
            a += Time.deltaTime * 100.0f;
            backgroundimg.color = new Color(1, 1, 1, Mathf.Clamp01(a / 300.0f));
            yield return null;
        }
        yield break;
    }
}
