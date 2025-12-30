using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EXPSystem : MonoBehaviour,ISaveManager//等级系统单例
{
    public static EXPSystem instance {get; private set;}

    public int level;
    public int manalevel;

    public int maxEXP;
    public int currentEXP;

    PlayerStats playerStats;
    public int currenthp;
    public int currentmana;

    public event Action onLevelUp;
    public event Action onmanaLevelUp;//事件，升级时通知订阅者
    public void Awake()
    {
        if(instance != null )
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayLevelUp());
        //playerStats =  GameObject.Find("player").GetComponent<PlayerStats>();
        //playerStats.currentHP = playerStats.GetMaxHP();
        //playerStats.Mana = playerStats.GetMaxMana();
    }
    private IEnumerator DelayLevelUp()
    {
        yield return null;//延迟一帧
        ToCurrentLevel();
    }
    private void ToCurrentLevel()
    {
        for (int i = 0; i < level; i++)
        {
            onLevelUp?.Invoke();
        }
        for(int i = 0;i < manalevel; i++)
        {
            onmanaLevelUp?.Invoke();
        }
    }//切换场景加载等级
    private void ToLastState()
    {

    }
    public void AddEXP(int experience)//增加经验
    {
        if (currentEXP + experience <= maxEXP)
        {
            currentEXP += experience;

        }
        else
        {
            currentEXP = currentEXP + experience - maxEXP;
            levelUP();
            changeMaxEXP();
        }

    }
    private void changeMaxEXP()//改变最大经验
    {
        maxEXP = Mathf.RoundToInt(maxEXP * 1.1f);
    }
    public void levelUP()//增加等级
    {
        level++;
        //AddModifiers();
        onLevelUp?.Invoke();
    }
    public void manalevelUP()//增加魔力等级
    {
        manalevel++;
        //ModifyMana(maxMana);
        onmanaLevelUp?.Invoke();
    }

    public void LoadData(GameData _data)
    {
        this.level = _data.level;
        this.manalevel = _data.manalevel;
        this.maxEXP = _data.maxEXP;
        this.currentEXP = _data.currentEXP;
    }

    public void SaveData(ref GameData _data)
    {
        _data.level = this.level;
        _data.manalevel = this.manalevel;
        _data.maxEXP = this.maxEXP;
        _data.currentEXP = this.currentEXP;
    }
}
