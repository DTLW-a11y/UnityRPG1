using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashingstate : Bossgroundstate
{
    Boss Boss;
    float time;
    int movedir;
    public BossDashingstate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
        Boss = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        time = 0;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        time += Time.deltaTime;
        Boss.SetVelocity(Boss.dashspeed * Boss.facingdir, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Boss.attackCheck.position, Boss.attackcheckdistance);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                // hit.GetComponent<Player>().Damage();
                PlayerStats _target = hit.GetComponent<PlayerStats>();
                Boss.stats.dodamage(_target);
            }
        }
        if (time > Boss.dashduration || enemy.IsWallDetected())
            stateMachine.ChangeState(Boss.battlestate);
    }
}
