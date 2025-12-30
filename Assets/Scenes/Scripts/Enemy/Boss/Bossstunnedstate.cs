using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossstunnedstate : EnemyState
{
    Boss enemy;
    
    public Bossstunnedstate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1F);
        stateTime = enemy.stunnedduration;
        rb.velocity = new Vector2(-enemy.stunneddistance.x * enemy.facingdir, enemy.stunneddistance.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();
        if(stateTime < 0)
        {
            stateMachine.ChangeState(enemy.idlestate);
        }
    }
}
