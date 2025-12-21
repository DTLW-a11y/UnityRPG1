using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceenHPUI : MonoBehaviour
{
    [SerializeField] GameObject player, topui;
    private CharacterStats characterStats;
    [SerializeField] GameObject hurtbar, nowbar;
    private RectTransform nowsize, hurtsize;
    private float maxhp, nowhp, lasthp;
    private RectTransform selftrans, reftrans;
    private UnityEngine.UI.Image barflashing;
    private float flashingalpha = 1.0f, deltaalpha = -2.0f;
    // Start is called before the first frame update
    void Start()
    {
        characterStats = player.GetComponent<CharacterStats>();
        selftrans = GetComponent<RectTransform>();
        reftrans = topui.GetComponent<RectTransform>();
        maxhp = characterStats.GetMaxHP();
        nowhp = characterStats.currentHP;
        lasthp = maxhp;
        nowsize = nowbar.GetComponent<RectTransform>();
        hurtsize = hurtbar.GetComponent<RectTransform>();
        hurtsize.sizeDelta = new Vector2(lasthp / maxhp * 500.0f, 50.0f);
        hurtsize.anchoredPosition = new Vector2((hurtsize.sizeDelta.x - 500.0f) / 2.0f, 0);
        barflashing = nowbar.GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    private float anitime = 0.3f;
    public static float selftx = 0.0f;
    void Update()
    {
        selftrans.anchoredPosition = new Vector2(selftx, (reftrans.anchoredPosition.y / 10.0f) - 200.0f);
        maxhp = characterStats.GetMaxHP();
        if (nowhp > characterStats.currentHP)
        {
            anitime = 0.5f;
            nowhp -= 4.4f * (nowhp - characterStats.currentHP + 0.9f) * Time.deltaTime;
        }
        else
        {
            nowhp = characterStats.currentHP;
            if (lasthp > nowhp)
            {
                if(anitime <= 0.0f)
                {
                    anitime = 0.0f;
                    lasthp -= 5.01f * (lasthp - nowhp + 1.02f) * Time.deltaTime;
                }
                else
                {
                    anitime -= Time.deltaTime;
                }
            }
            else
            {
                lasthp = nowhp;
            }
        }
        if (nowhp <= maxhp * 0.2f)
        {
            flashingalpha += deltaalpha * Time.deltaTime;
            if (flashingalpha > 1.0f)
            {
                flashingalpha = 1.0f;
                deltaalpha = -2.0f;
            }
            else if (flashingalpha < 0.0f)
            {
                flashingalpha = 0.0f;
                deltaalpha = 2.0f;
            }
        }
        else
        {
            flashingalpha = 1.0f;
            deltaalpha = -2.0f;
        }
        barflashing.color = new Color(1.0f, 1.0f, 1.0f, flashingalpha);
        nowsize.sizeDelta = new Vector2(nowhp / maxhp * 500.0f, 40.0f);
        nowsize.anchoredPosition = new Vector2((nowsize.sizeDelta.x - 500.0f) / 2.0f, 50.0f);
        hurtsize.sizeDelta = new Vector2(lasthp / maxhp * 500.0f, 40.0f);
        hurtsize.anchoredPosition = new Vector2((hurtsize.sizeDelta.x - 500.0f) / 2.0f, 50.0f);
    }
}
