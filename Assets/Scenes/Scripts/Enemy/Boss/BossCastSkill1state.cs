using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastSkill1state : EnemyState
{
    protected Boss enemy;
    protected Transform player;
    protected BossStats enemystats;
    public int movedir = 1;
    public float distance;
    int currenthp;

    public BossCastSkill1state(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemystats = enemy.GetComponent<BossStats>();
        currenthp = enemystats.currentHP;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        enemy.ZeroVelocity();
        enemystats.changehealthto(currenthp);
        if (TriggerCalled)
        {

            stateMachine.ChangeState(enemy.movestate);
        }
    }
}