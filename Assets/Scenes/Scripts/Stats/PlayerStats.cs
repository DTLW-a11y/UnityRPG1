using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStats : CharacterStats,ISaveManager
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
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
