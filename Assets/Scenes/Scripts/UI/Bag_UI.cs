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

    private RectTransform recTrans;
    private CharacterStats playerstats;
    private RectTransform bartrans;
    private TextMeshProUGUI hptextmeshpro;
    // Start is called before the first frame update
    void Start()
    {
        playerstats = player.GetComponent<CharacterStats>();
        bartrans = hpbar.GetComponent<RectTransform>();
        hptextmeshpro = hptext.GetComponent<TextMeshProUGUI>();
        recTrans = GetComponent<RectTransform>();
        recTrans.anchoredPosition = new Vector2(0, 2000);
    }

    private void UpdateHP()
    {
        float hp = playerstats.currentHP;
        float maxhp = playerstats.GetMaxHP();
        hptextmeshpro.text = "Your HP:  " + hp.ToString() + " / " + maxhp.ToString();
        if (hp < 0.0f) hp = 0.0f;
        bartrans.sizeDelta = new Vector2(590.0f * hp / maxhp, 36);
        bartrans.anchoredPosition = new Vector2((bartrans.sizeDelta.x - 590.0f) / 2.0f, 0);
        return;
    }

    // Update is called once per frame
    private bool isMoving = false;
    private bool isShow = false;
    private float anitime = 0.0f;
    void Update()
    {
        UpdateHP();
        if (isMoving)
        {
            if (Input.anyKeyDown)
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
                if (Input.anyKeyDown)
                {
                    anitime = 0.0f;
                    isMoving = true;
                    isShow = false;
                }
                break;
            case false:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anitime = 0.0f;
                    isMoving = true;
                    isShow = true;
                }
                break;
        }
    }
}
