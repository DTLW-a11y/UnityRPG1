using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterdiestate : EnemyState
{
    EnemyMaster enemy;
    string enemyname;
    
    public masterdiestate(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMaster enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {

        base.Enter();
        enemyname = enemy.gameObject.name;
        Debug.Log("die");
        if (enemyname.StartsWith("Enemy_Goblin"))
        {
            TaskManager.instance.UpdateProgress(tasktype.Killenemy, 901, 1);
        }
        else if (enemyname.StartsWith("Enemy_Master"))
        {
            TaskManager.instance.UpdateProgress(tasktype.Killenemy, 902, 1);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()//根据inspector里的名字判断击杀的是什么类型
    {
        base.Update();
        enemy.ZeroVelocity();
        if (TriggerCalled)
        {
            enemy.StartCoroutine(enumerator(0f));
        }

    }
    private IEnumerator enumerator(float time)
    {
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState(enemy.elimination);
    }
}
