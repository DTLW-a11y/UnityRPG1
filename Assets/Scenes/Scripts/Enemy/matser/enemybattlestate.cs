using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class enemybattlestate : EnemyState
{
    EnemyMaster enemy;
    private Transform player;
    private int movedir;
    private int combocounter;
    public enemybattlestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemyMaster enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }//����ʱʹ��

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isplayerdetected())
        {
            stateTime = enemy.battletime;
            if (enemy.isplayerdetected().distance < enemy.attackdistance)
            {
                if (!check())
                {
                    combocounter=(combocounter+1)%2;
                    enemy.anim.SetInteger("ComboCounter",combocounter);
                    stateMachine.ChangeState(enemy.attackstate);
                }
            }
        }
        else
        {
            if(stateTime < 0 || player.transform.position.x - enemy.transform.position.x > 10)
            {
                stateMachine.ChangeState(enemy.idlestate);
            }
        }

        if (Mathf.Abs(player.position.x - enemy.transform.position.x) > 1)
        {
            if (player.position.x >= enemy.transform.position.x)
                movedir = 1;
            else if (player.position.x < enemy.transform.position.x)
                movedir = -1;
        }

            enemy.SetVelocity(enemy.movespeed * movedir, rb.velocity.y);
        
    }
    private bool check()
    {
        if (Time.time >= enemy.lastattcktime + enemy.attckcooldown)
        {
            return false;
        }
        else
            return true;

    }
}
