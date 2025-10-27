using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private EnemyMaster enemy;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<EnemyMaster>();
    }
    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);
        enemy.DamageEffect();
    }
    protected override void die()
    {
        base.die();
        enemy.stateMachine.ChangeState(enemy.diestate);
    }
}
