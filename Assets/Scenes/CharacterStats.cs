using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHP;
    public Stat damage;
    public Stat strenth;
    public bool isdied;
    [SerializeField] private int currentHP;
    
    protected virtual void Start()
    {
        isdied = false;
        currentHP = maxHP.GetValue();
    }

    public void dodamage(CharacterStats stats)//根据攻击者条件计算伤害
    {
        int totaldamage = damage.GetValue() + strenth.GetValue();

        stats.takedamage(totaldamage);
    }
    public virtual void takedamage(int _damage)
    {
        currentHP -= _damage;
        if (currentHP < 0)
        {
            die();
        }
    }
    
    protected virtual void die()
    {
        isdied = true;
    }
}
