using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterdiestate : EnemyState
{
    EnemyMaster enemy;
    
    public masterdiestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMaster enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {

        base.Enter();
        enemy.StartCoroutine("BusyFor");

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();

    }
    
}
