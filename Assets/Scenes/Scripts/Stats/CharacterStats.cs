using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Base info")]
    public Stat strenth;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;

    [Header("Defense info")]
    public Stat maxHP;
    public Stat armor;
    public Stat evision;

    [Header("Attack info")]
    public Stat damage;
    public Stat critchance;
    public Stat critpower;
    public bool isdied;
    public int currentHP;

    public System.Action Onhealthchange;

    protected virtual void Start()
    {
        isdied = false;
        critpower.Setvalue(150);
        currentHP = GetMaxHP();
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
        if(Onhealthchange != null)
        Onhealthchange();

    }
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
        Decreasehealthby(_damage);
        if (currentHP < 0)
        {
            die();
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
}
    
