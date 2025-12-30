using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceenHPUI : MonoBehaviour
{
    [SerializeField] GameObject player, topui, bookui, bookleaf;
    private CharacterStats characterStats;
    [SerializeField] GameObject hurtbar, nowbar, manabar, mananow;
    private RectTransform nowsize, hurtsize, mananowsize, manasize;
    private float maxhp, nowhp, lasthp, maxmana, nowmana, lastmana;
    private RectTransform selftrans, reftrans, booktrans, leaftrans;
    private UnityEngine.UI.Image barflashing;
    private float flashingalpha = 1.0f, deltaalpha = -2.0f;
    // Start is called before the first frame update
    void Start()
    {
        characterStats = player.GetComponent<CharacterStats>();
        selftrans = GetComponent<RectTransform>();
        reftrans = topui.GetComponent<RectTransform>();
        booktrans = bookui.GetComponent<RectTransform>();
        leaftrans = bookleaf.GetComponent<RectTransform>();
        maxhp = characterStats.GetMaxHP();
        nowhp = characterStats.currentHP;
        lasthp = maxhp;
        maxmana = characterStats.maxMana.GetValue();
        nowmana = characterStats.Mana;
        lastmana = maxmana;
        nowsize = nowbar.GetComponent<RectTransform>();
        hurtsize = hurtbar.GetComponent<RectTransform>();
        hurtsize.sizeDelta = new Vector2(lasthp / maxhp * 500.0f, 40.0f);
        hurtsize.anchoredPosition = new Vector2((hurtsize.sizeDelta.x - 500.0f) / 2.0f, 50.0f);
        mananowsize = mananow.GetComponent<RectTransform>();
        manasize = manabar.GetComponent<RectTransform>();
        manasize.sizeDelta = new Vector2(lastmana / maxmana * 500.0f, 30.0f);
        manasize.anchoredPosition = new Vector2((manasize.sizeDelta.x - 500.0f) / 2.0f, 0.0f);
        barflashing = nowbar.GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    private float anitime = 0.5f, anitimemana = 0.2f;
    public static float selftx = 0.0f;
    void Update()
    {
        selftrans.anchoredPosition = new Vector2(selftx, (reftrans.anchoredPosition.y / 10.0f) - 200.0f);
        booktrans.anchoredPosition = new Vector2(0, (reftrans.anchoredPosition.y / 10.0f) - 200.0f);
        leaftrans.anchoredPosition = new Vector2(leaftrans.anchoredPosition.x, reftrans.anchoredPosition.y / 1.7f  - (2000.0f / 1.7f - 50.0f));
        updatehp();
        flashinghp();
        updatemana();
        nowsize.sizeDelta = new Vector2(nowhp / maxhp * 500.0f, 40.0f);
        nowsize.anchoredPosition = new Vector2((nowsize.sizeDelta.x - 500.0f) / 2.0f, 50.0f);
        hurtsize.sizeDelta = new Vector2(lasthp / maxhp * 500.0f, 40.0f);
        hurtsize.anchoredPosition = new Vector2((hurtsize.sizeDelta.x - 500.0f) / 2.0f, 50.0f);
        mananowsize.sizeDelta = new Vector2(nowmana / maxmana * 500.0f, 30.0f);
        mananowsize.anchoredPosition = new Vector2((mananowsize.sizeDelta.x - 500.0f) / 2.0f, 0.0f);
        manasize.sizeDelta = new Vector2(lastmana / maxmana * 500.0f, 30.0f);
        manasize.anchoredPosition = new Vector2((manasize.sizeDelta.x - 500.0f) / 2.0f, 0.0f);
    }
    void updatehp()
    {
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
                if (anitime <= 0.0f)
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
    }
    void updatemana()
    {
        maxmana = characterStats.maxMana.GetValue();
        if (nowmana > characterStats.Mana)
        {
            anitimemana = 0.2f;
            nowmana -= 4.4f * (nowmana - characterStats.Mana + 0.9f) * Time.deltaTime;
        }
        else
        {
            nowmana = characterStats.Mana;
            if (lastmana > nowmana)
            {
                if (anitimemana <= 0.0f)
                {
                    anitimemana = 0.0f;
                    lastmana -= 5f * (lastmana - nowmana + 1.1f) * Time.deltaTime;
                }
                else
                {
                    anitimemana -= Time.deltaTime;
                }
            }
            else
            {
                lastmana = nowmana;
            }
        }
    }
    void flashinghp()
    {
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
    }
}
