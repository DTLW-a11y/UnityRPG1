using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymovestate : enemygroundstate
{
    public enemymovestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMaster enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.movespeed * enemy.facingdir, enemy.rb.velocity.y);
        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.ZeroVelocity();
            enemy.Flip();
            stateMachine.ChangeState(enemy.idlestate);
        }
    }
}

