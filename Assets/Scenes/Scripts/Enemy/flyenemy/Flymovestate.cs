using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flymovestate : Flygroundstate
{
    public Flymovestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyFly enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
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
            if (!enemy.IsGroundDetected()) Debug.Log(1);
            enemy.ZeroVelocity();
            enemy.Flip();
            stateMachine.ChangeState(enemy.idlestate);
        }
    }
}

