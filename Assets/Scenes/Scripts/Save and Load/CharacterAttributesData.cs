using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class CharacterAttributesData
{
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    public int maxHP;
    public int currentHP;
    public int armor;
    public int evasion;

    public int damage;
    public int critChance;
    public int critPower;

    public CharacterAttributesData()
    {
        strength = 10;
        agility = 10;
        intelligence = 10;
        vitality = 0;
        maxHP = 100;
        currentHP = 100;
        armor = 0;
        evasion = 5;
        damage = 20;
        critChance = 10;
        critPower = 150;
    }
}
