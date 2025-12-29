using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossdiestate : EnemyState
{
    Boss enemy;
    string enemyname;
    public Bossdiestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        enemyname = enemy.gameObject.name;
        TaskManager.instance.UpdateProgress(tasktype.Killenemy, 904, 1);
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
