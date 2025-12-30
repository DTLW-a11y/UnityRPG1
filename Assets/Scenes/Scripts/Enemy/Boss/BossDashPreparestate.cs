using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashPreparestate : Bossgroundstate
{
    Boss Boss;
    public BossDashPreparestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
        Boss = enemy;
    }
        public override void Enter()
    {
        base.Enter();
        
    }
    public override void Update()
    {
        enemy.ZeroVelocity();

        if (TriggerCalled)
        {

            stateMachine.ChangeState(Boss.dashstate);
        }
    }

    public override void Exit()
    {
        
        base.Exit();
    }
}


