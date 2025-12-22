using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Bag_UI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject hptext;
    [SerializeField] GameObject hpbar;
    [SerializeField] GameObject valuelist;

    private RectTransform recTrans;
    private CharacterStats playerstats;
    private RectTransform bartrans;
    private TextMeshProUGUI hptextmeshpro, value;
    private float hp = 100.0f, maxhp;
    // Start is called before the first frame update
    void Start()
    {
        playerstats = player.GetComponent<CharacterStats>();
        bartrans = hpbar.GetComponent<RectTransform>();
        hptextmeshpro = hptext.GetComponent<TextMeshProUGUI>();
        value = valuelist.GetComponent<TextMeshProUGUI>();
        recTrans = GetComponent<RectTransform>();
        recTrans.anchoredPosition = new Vector2(0, 2000);
    }
    private void UpdateHP()
    {
        if (Mathf.Abs(playerstats.currentHP - hp) < 1.0f)
        {
            hp = playerstats.currentHP;
        }
        else
        {
            hp += (playerstats.currentHP - hp) * 2.0f * Time.deltaTime;
        }
        maxhp = playerstats.GetMaxHP();
        hptextmeshpro.text = playerstats.currentHP.ToString() + " / " + maxhp.ToString();
        value.text = playerstats.damage.GetValue().ToString() + "\n" + ((float)(playerstats.critchance.GetValue())/100.0f).ToString() + "\n" + ((float)(playerstats.critpower.GetValue()) / 100.0f).ToString() + "\n" + playerstats.armor.GetValue().ToString() + "\n" + playerstats.evision.GetValue().ToString();
        if (hp < 0.0f) hp = 0.0f;
        bartrans.sizeDelta = new Vector2(584.0f * hp / maxhp, 36);
        bartrans.anchoredPosition = new Vector2((bartrans.sizeDelta.x - 584.0f) / 2.0f, -10.0f);
        return;
    }

    // Update is called once per frame
    private bool isMoving = false;
    private bool isShow = false, KeyEDown;
    private float anitime = 0.0f;
    void Update()
    {
        KeyEDown = Input.GetKeyDown(KeyCode.B);
        UpdateHP();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMoving = false;
            isShow = false;
            anitime = 0.0f;
            recTrans.anchoredPosition = new Vector2(0, 2000);
            return;
        }
        if (isMoving)
        {
            if (KeyEDown)
            {
                isMoving = false;
                isShow = false;
                anitime = 0.0f;
                recTrans.anchoredPosition = new Vector2(0, 2000);
                return;
            }
            anitime += Time.deltaTime;
            if (anitime >= 0.5f)
            {
                isMoving = false;
                anitime = 0.0f;
                return;
            }
            switch (isShow)
            {
                case true:
                    recTrans.anchoredPosition = new Vector2(0, 8000 * Mathf.Pow((anitime - 0.5f), 2.0f));
                    break;
                case false:
                    recTrans.anchoredPosition = new Vector2(0, 8000 * Mathf.Pow(anitime, 2.0f));
                    break;
            }
            return;
        }
        switch (isShow)
        {
            case true:
                if (KeyEDown)
                {
                    anitime = 0.0f;
                    isMoving = true;
                    isShow = false;
                }
                break;
            case false:
                if (KeyEDown)
                {
                    anitime = 0.0f;
                    isMoving = true;
                    isShow = true;
                }
                break;
        }
    }
}
