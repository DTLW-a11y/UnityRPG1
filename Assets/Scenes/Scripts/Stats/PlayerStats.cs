using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStats : CharacterStats,ISaveManager
{
    private Player player;

    [Header("Level Details")]//�ȼ�
    //[SerializeField] private int level = 1;
    //[SerializeField] private int manalevel = 1;
    [Range(0f, 1f)]
    [SerializeField] private float percentage = .1f;


    //[SerializeField] int currentEXP;
    //[SerializeField] int maxEXP;
    protected override void Start()
    {
        base.Start();
       // AddModifiers();
        player = GetComponent<Player>();

        EXPSystem.instance.onLevelUp += levelup;
        EXPSystem.instance.onmanaLevelUp += manalevelup;
    }
    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);
        player.DamageEffect();
    }

    protected override void die()
    {
        base.die();
        player.stateMachine.changeState(player.diestate);

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
        SaveManager.instance.LoadFromLastCheckpoint();
    }

    private void Modify(Stat _stat)//��ֵ��ȼ�ָ��������
    {
        
            float modifier = _stat.GetValue() * percentage;
            _stat.addmodifier(Mathf.RoundToInt(modifier));
       
    }
    private void ModifyMana(Stat _stat)//ħ�����ӣ�����
    {
        
            float modifier = 0f;
            modifier += 20;
            _stat.addmodifier(Mathf.RoundToInt(modifier));
        
    }

    private void AddModifiers() // ��ȼ������޸ĵ�����
    {
        Modify(damage);
        Modify(strenth);

        Modify(armor);
        Modify(maxHP);

        //ModifyMana(intelligence);
    }

    private void levelup()
    {
        AddModifiers();
    }
    private void manalevelup()
    {
        ModifyMana(maxMana);
    }

    public void OnDestroy()//������Ʒʱȡ�����ģ���ֹ�ڴ�й¶
    {
        if(EXPSystem.instance != null)
        {
            EXPSystem.instance.onLevelUp -= levelup;
            EXPSystem.instance.onmanaLevelUp -= manalevelup;
        }
    }
    public void SaveData(ref GameData _data)
    {
        _data.playerAttributes.strength = strenth.GetValue();
        _data.playerAttributes.agility = agility.GetValue();
        _data.playerAttributes.intelligence = intelligence.GetValue();
        _data.playerAttributes.vitality = vitality.GetValue();

        _data.playerAttributes.maxHP = maxHP.GetValue();
        _data.playerAttributes.currentHP = currentHP;
        _data.playerAttributes.armor = armor.GetValue();
        _data.playerAttributes.evasion = evision.GetValue();

        _data.playerAttributes.damage = damage.GetValue();
        _data.playerAttributes.critChance = critchance.GetValue();
        _data.playerAttributes.critPower = critpower.GetValue();
    }

    public void LoadData(GameData _data)
    {
        strenth.Setvalue(_data.playerAttributes.strength);
        agility.Setvalue(_data.playerAttributes.agility);
        intelligence.Setvalue(_data.playerAttributes.intelligence);
        vitality.Setvalue(_data.playerAttributes.vitality);

        maxHP.Setvalue(_data.playerAttributes.maxHP);
        currentHP = _data.playerAttributes.currentHP;
        armor.Setvalue(_data.playerAttributes.armor);
        evision.Setvalue(_data.playerAttributes.evasion);

        damage.Setvalue(_data.playerAttributes.damage);
        critchance.Setvalue(_data.playerAttributes.critChance);
        critpower.Setvalue(_data.playerAttributes.critPower);
    }

}
