using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class CharacterStats : MonoBehaviour
{
    [Header("Base info")]//基础数值
    public Stat strenth;
    public Stat agility;
    public Stat intelligence; //影响魔力量
    public Stat vitality;

    [Header("Defense info")]//防御数值
    public Stat maxHP;
    public Stat armor;
    public Stat evision;//闪避

    [Header("Attack info")]
    public Stat damage;
    public Stat critchance;//暴击率
    public Stat critpower;//暴击倍率
    public int currentHP;

    [Header("Mana info")]
    public Stat maxMana;
    public int Mana;//魔力量

    public System.Action onhealthchange; //委托，发生时通知订阅者
    public System.Action Onmanachange;

    public bool isdied;

    

    protected virtual void Start()
    {

        isdied = false;
        critpower.Setvalue(150);
        currentHP = GetMaxHP();
        Mana = GetMaxMana();
    }

    public void dodamage(CharacterStats stats)//根据攻击者条件计算伤害
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
    }//释放技能时减少魔力量
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
    
