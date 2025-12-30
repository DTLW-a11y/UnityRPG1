using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bossairstate : EnemyState
{
    protected Boss enemy;
    protected Transform player;
    protected BossStats enemystats;
    public int movedir = 1;
    public float distance;

    public Bossairstate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            if (!enemy.IsGroundDetected()) Debug.Log(1);
            enemy.ZeroVelocity();
            enemy.Flip();
            //stateMachine.ChangeState(enemy.idlestate);
        }
        if (detected())
        {
            //Debug.Log($"{enemy.count},{enemy.time}");
            if (enemy.count < 3 && enemy.time <= 0)
            {
                enemy.count++;
                stateMachine.ChangeState(enemy.skillstate);
            }
            else if (enemy.count == 3)
            {
                enemy.count = 0;
                enemy.time = enemy.skillcooldown;
            }
        }

        else if (!detected() )
        {
            int a = Random.Range(0, 10);
            if (a > 5)
            {
                enemy.Flip();
                movedir = -movedir;
            }
        }
        distance = Vector2.Distance(enemy.transform.position, player.position);
        enemy.SetVelocity(enemy.movespeed * movedir, rb.velocity.y);
        enemy.time -= Time.deltaTime;
    }
    bool detected()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(enemy.detectcenter.position, enemy.detectdistance);
        foreach (var col2 in col)
        {
            if (col2.GetComponent<Player>() != null)
            {
                return true;
            }
        }
        return false;
    }
}
