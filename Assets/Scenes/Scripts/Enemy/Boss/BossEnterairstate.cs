using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterairstate : Bossgroundstate
{
    Boss Boss;
    public BossEnterairstate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
        Boss = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        rb = Boss.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        enemystats = enemy.GetComponent<BossStats>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        enemystats.Increasehealthby(enemystats.GetMaxHP());
       // Debug.Log($"{enemystats.currentHP},{enemystats.GetMaxHP()}");
        if (enemy.transform.position.y < enemy.height)
        {
            enemy.SetVelocity(0, enemy.upspeed);
        }
        else if (enemy.transform.position.y >= enemy.height)
        {
            enemy.SetVelocity(0, 0);
            stateMachine.ChangeState(enemy.airstate);
        }

    }
}
