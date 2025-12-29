using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastSkillstate : EnemyState
{
    protected Boss enemy;
    protected Transform player;
    protected BossStats enemystats;
    public int movedir = 1;
    public float distance;

    public BossCastSkillstate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        enemy.ZeroVelocity();

        if (TriggerCalled)
        {

            stateMachine.ChangeState(enemy.airstate);
        }
    }
}