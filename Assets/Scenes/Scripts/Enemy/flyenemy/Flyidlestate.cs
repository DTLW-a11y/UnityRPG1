using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyidlestate : Flygroundstate
{
    private float flystateTime;
    public Flyidlestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyFly enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        flystateTime = enemy.idletime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        flystateTime -= Time.deltaTime;
        base.Update();
        if (flystateTime < 0f)
        {
            stateMachine.ChangeState(enemy.movestate);
        }
    }
}
