using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPUI : MonoBehaviour
{
    private bool ismana = false;
    [SerializeField] GameObject bar, value, num, title, icon;
    [SerializeField] Sprite expback, manaback, expico, manaico;
    TextMeshProUGUI valuetext, numtext ,titletext;
    RectTransform expbar;
    Image backimg, iconimg;
    // Start is called before the first frame update
    void Start()
    {
        expbar = bar.GetComponent<RectTransform>();
        valuetext = value.GetComponent<TextMeshProUGUI>();
        numtext = num.GetComponent<TextMeshProUGUI>();
        titletext = title.GetComponent<TextMeshProUGUI>();
        backimg = GetComponent<Image>();
        iconimg = icon.GetComponent<Image>();
        EXPSystem.instance.onEXPchange += updateexp;
        EXPSystem.instance.onmanaLevelUp += updatemana;
        updateexp();
        switchtoshow();
    }
    void updateexp()
    {
        expbar.sizeDelta = new Vector2(420.0f * EXPSystem.instance.currentEXP / EXPSystem.instance.maxEXP, 0.0f);
        expbar.anchoredPosition = new Vector2((420.0f - expbar.sizeDelta.x) / 2.0f - 81.0f, -260.0f);
        valuetext.text = EXPSystem.instance.currentEXP.ToString() + " / " + EXPSystem.instance.maxEXP.ToString();
        if (ismana) return;
        numtext.text = EXPSystem.instance.level.ToString();
    }
    void updatemana()
    {
        if (!ismana) return;
        numtext.text = EXPSystem.instance.manalevel.ToString();
    }
    void switchtoshow()
    {
        if (ismana)
        {
            backimg.sprite = manaback;
            iconimg.sprite = manaico;
            titletext.text = "魔力等级";
            updatemana();
        }
        else
        {
            backimg.sprite = expback;
            iconimg.sprite = expico;
            titletext.text = "等      级";
            updateexp();
        }
    }
    public void OnClick()
    {
        ismana = !ismana;
        switchtoshow();
    }
}
