using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flyattackstate : EnemyState
{
    EnemyFly enemy;
    public Flyattackstate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemyFly enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastattcktime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();

        if (TriggerCalled)
        {
            
            stateMachine.ChangeState(enemy.battlestate);
        }
    }
    
}
