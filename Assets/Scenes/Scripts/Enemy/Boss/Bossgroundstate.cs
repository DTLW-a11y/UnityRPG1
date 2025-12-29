using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossgroundstate : EnemyState
{
    protected Boss enemy;
    protected Transform player;
    protected BossStats enemystats;

    public Bossgroundstate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("player").transform;
        enemystats = enemy.GetComponent<BossStats>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemystats.currentHP <= enemystats.GetMaxHP() * .2f)
        {
            Debug.Log($"{enemystats.currentHP},{enemystats.GetMaxHP() * .2f}");
            stateMachine.ChangeState(enemy.enterairstate);
        }
        if (enemy.isplayerdetected() || Vector2.Distance(enemy.transform.position,player.position) < 2)
            stateMachine.ChangeState(enemy.battlestate);

        
    }
}
