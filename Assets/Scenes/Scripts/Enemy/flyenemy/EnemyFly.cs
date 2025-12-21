using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : Enemy
{
    #region States
    public Flyidlestate idlestate {  get; private set; }
    public Flymovestate movestate { get; private set; }
    public Flybattlestate battlestate { get; private set; }
    public Flyattackstate attackstate { get; private set; }
    public Flystunnedstate stunnedstate { get; private set; }
    public Flydiestate diestate { get; private set; }
    public Flyelimination elimination { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idlestate = new Flyidlestate(this,stateMachine,"Idle", this);
        movestate = new Flymovestate(this, stateMachine, "Move", this);
        battlestate = new Flybattlestate(this, stateMachine, "Move", this);
        attackstate = new Flyattackstate(this, stateMachine, "Attack", this);
        stunnedstate = new Flystunnedstate(this, stateMachine, "Stunned", this);
        diestate = new Flydiestate(this, stateMachine, "Die", this);
        elimination = new Flyelimination(this, stateMachine, "Die", this);
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
