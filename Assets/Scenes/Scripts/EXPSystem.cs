using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EXPSystem : MonoBehaviour,ISaveManager//�ȼ�ϵͳ����
{
    public static EXPSystem instance {get; private set;}

    public int level;
    public int manalevel;

    public int maxEXP;
    public int currentEXP;

    public event Action onEXPchange;
    PlayerStats playerStats;
    public int currenthp;
    public int currentmana;

    public event Action onLevelUp;
    public event Action onmanaLevelUp;//�¼�������ʱ֪ͨ������
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
        yield return null;//�ӳ�һ֡
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
    }//�л��������صȼ�
    private void ToLastState()
    {

    }
    public void AddEXP(int experience)//���Ӿ���
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
        onEXPchange?.Invoke();
    }
    private void changeMaxEXP()//�ı������
    {
        maxEXP = Mathf.RoundToInt(maxEXP * 1.1f);
    }
    public void levelUP()//���ӵȼ�
    {
        level++;
        //AddModifiers();
        onLevelUp?.Invoke();
    }
    public void manalevelUP()//����ħ���ȼ�
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
