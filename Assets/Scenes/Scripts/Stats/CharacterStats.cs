using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class CharacterStats : MonoBehaviour
{
    [Header("Base info")]//������ֵ
    public Stat strenth;
    public Stat agility;
    public Stat intelligence; //Ӱ��ħ����
    public Stat vitality;

    [Header("Defense info")]//������ֵ
    public Stat maxHP;
    public Stat armor; // ����
    public Stat evision; // ����

    [Header("Attack info")]
    public Stat damage;
    public Stat critchance;//������
    public Stat critpower;//��������
    public int currentHP;

    [Header("Mana info")]
    public Stat maxMana;
    public int Mana;//ħ����

    public System.Action onhealthchange; //ί�У�����ʱ֪ͨ������
    public System.Action Onmanachange;

    public bool isdied;

    

    protected virtual void Start()
    {

        isdied = false;
        critpower.Setvalue(150);
        currentHP = GetMaxHP();
        Mana = GetMaxMana();
    }

    public void dodamage(CharacterStats stats)//���ݹ��������������˺�
    {
        if (CanAvoidAttack(stats))
            return;

        int totaldamage = damage.GetValue() + strenth.GetValue();

        if (Cirtcheck())
        {
            totaldamage = Criticaldamage(totaldamage);
        }

        totaldamage = checktargetarmor(stats,totaldamage);
        stats.takedamage(totaldamage);
    }
    private bool Cirtcheck()
    {
        int totalcrit = critchance.GetValue() + agility.GetValue();
        if (Random.Range(0, 100) <= totalcrit) 
            return true;
        else return false;
    }
    private int Criticaldamage(int _damage)
    {
        float criticalpoint = (critpower.GetValue() + strenth.GetValue()) * .01f;
        float critdamage = _damage * criticalpoint;
        return Mathf.RoundToInt(critdamage);
    }
    protected virtual void Decreasehealthby(int _damage)
    {
        currentHP -=_damage;
        if(onhealthchange != null)
        onhealthchange();

    }
    public virtual void Increasehealthby(int _heal)
    {
        int maxhp = GetMaxHP();
        if (currentHP + _heal <= maxhp)
        {
            currentHP += _heal;
            if (onhealthchange != null)
                onhealthchange();
        }
        else
        {
            currentHP = maxhp;
            if (onhealthchange != null)
                onhealthchange();
        }

    }
    public bool CanDecreaseMana(int _cost)
    {
        if (Mana - _cost < 0)
        {
            Debug.Log("not enough mana");
            return false;
        }
        else
        {
            Decreasemana(_cost);
            return true;
        }
    }
    public void Decreasemana(int _cost)
    {
        Mana -=_cost;
        if(Onmanachange != null) Onmanachange();
    }//�ͷż���ʱ����ħ����
    private int checktargetarmor(CharacterStats stats,int totaldamage)
    {
        totaldamage -= stats.armor.GetValue();
        if (totaldamage <= 0)
            totaldamage = 1;
        return totaldamage;
    }

    private bool CanAvoidAttack(CharacterStats stats)
    {
        int totalevision = stats.agility.GetValue() + stats.evision.GetValue();
        if (Random.Range(0, 100) < totalevision)
        {
            Debug.Log("evided");
            return true;
        }
        else return false;
    }

    public virtual void takedamage(int _damage)
    {
        if(currentHP > 0)
        {

            Decreasehealthby(_damage);
            if (currentHP <= 0)
            {
                die();
            }
        }
    }
    
    protected virtual void die()
    {
        isdied = true;
    }
    public int GetMaxHP()
    {
        return maxHP.GetValue() + vitality.GetValue() * 5;
    }
    public int GetMaxMana()
    {
        return maxMana.GetValue() + intelligence.GetValue() * 5;
    }

    
    
}
    
