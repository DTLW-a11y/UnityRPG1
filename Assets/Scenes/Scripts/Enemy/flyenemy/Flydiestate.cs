using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flydiestate : EnemyState
{
    EnemyFly enemy;
    
    public Flydiestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyFly enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
        if (TriggerCalled)
        {
            enemy.gameObject.SetActive(false);
        }

    }
}
