using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyState 
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected Rigidbody2D rb;

    private string animBoolName;

    protected bool TriggerCalled;
    protected float stateTime;
    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTime -= Time.deltaTime;
    }
    public virtual void Enter()
    {
        TriggerCalled = false;
        enemyBase.anim.SetBool(animBoolName,true);
        rb = enemyBase.rb;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        TriggerCalled=true;
    }

}
//构造函数，其他状态需用到的字段属性方法

