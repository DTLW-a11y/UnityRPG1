using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    #region States
    public Bossidlestate idlestate {  get; private set; }
    public Bossmovestate movestate { get; private set; }
    public Bossbattlestate battlestate { get; private set; }
    public Bossattackstate attackstate { get; private set; }
    public Bossstunnedstate stunnedstate { get; private set; }
    public Bossdiestate diestate { get; private set; }
    public Bosselimination elimination { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idlestate = new Bossidlestate(this,stateMachine,"Idle", this);
        movestate = new Bossmovestate(this, stateMachine, "Move", this);
        battlestate = new Bossbattlestate(this, stateMachine, "Move", this);
        attackstate = new Bossattackstate(this, stateMachine, "Attack", this);
        stunnedstate = new Bossstunnedstate(this, stateMachine, "Stunned", this);
        diestate = new Bossdiestate(this, stateMachine, "Die", this);
        elimination = new Bosselimination(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(movestate);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override bool canbestunned()
    {
        if (base.canbestunned())
        {
            stateMachine.ChangeState(stunnedstate);
            return true;
        }
        return false;
    }
}
