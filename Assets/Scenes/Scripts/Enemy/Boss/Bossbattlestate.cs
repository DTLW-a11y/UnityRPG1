using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossbattlestate : EnemyState
{
    Boss enemy;
    private float skilltime = 1;
    private float cooldown = 0;
    private float cooldown1 = 0;
    private Transform player;
    private int movedir =1;
    private int combocounter;
    float distance = Mathf.Infinity;
    protected BossStats enemystats;

    public Bossbattlestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }//创建时使用

    public override void Enter()
    {
        base.Enter();
        cooldown = 0;
        cooldown1 = 0;
        player = GameObject.Find("player").transform;
        enemystats = enemy.GetComponent<BossStats>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        cooldown += Time.deltaTime;
        cooldown1 += Time.deltaTime;
        base.Update();
        if (enemystats.currentHP <= enemystats.GetMaxHP() * .2f)
        {
            Debug.Log($"{enemystats.currentHP},{enemystats.GetMaxHP() * .2f}");
            stateMachine.ChangeState(enemy.enterairstate);
        }
        if (enemy.isplayerdetected())
        {
            stateTime = enemy.battletime;
            if (enemy.isplayerdetected().distance < enemy.attackdistance)
            {
                int num = Random.Range(0,10);
                if (!check() && num >3)
                {
                    combocounter=(combocounter+1)%2;
                    enemy.anim.SetInteger("ComboCounter",combocounter);
                    stateMachine.ChangeState(enemy.attackstate);
                }
                else if( num<=3 && cooldown1 >enemy.holecooldown)
                {
                    cooldown1 = 0;
                    stateMachine.ChangeState(enemy.skill1state);
                }
            }
        }
     
        if (enemy.isplayerdetected() && Vector2.Distance(player.position , enemy.gameObject.transform.position) > enemy.detectdistance && cooldown > skilltime)
        {
            cooldown = 0;
            stateMachine.ChangeState(enemy.dashprestate);
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
