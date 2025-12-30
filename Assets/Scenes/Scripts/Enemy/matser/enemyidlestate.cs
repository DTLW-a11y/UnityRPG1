using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyidlestate : enemygroundstate
{
    public enemyidlestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMaster enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTime = enemy.idletime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTime < 0f)
        {
            stateMachine.ChangeState(enemy.movestate);
        }
    }
}
