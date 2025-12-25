using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flybattlestate : EnemyState
{
    EnemyFly enemy;
    private Transform player;
    private int movedir =1;
    private int combocounter;
    float distance = Mathf.Infinity;
    public Flybattlestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemyFly enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }//创建时使用

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
        
        else if(!enemy.isplayerdetected()&& Vector2.Distance(enemy.transform.position, player.position)>distance)
        {
            enemy.Flip();
            movedir = -movedir;
        }
        distance = Vector2.Distance(enemy.transform.position, player.position);
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
