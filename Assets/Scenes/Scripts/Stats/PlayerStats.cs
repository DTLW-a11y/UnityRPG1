using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
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
    }
}
