using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : Enemy
{
    #region States
    public enemyidlestate idlestate {  get; private set; }
    public enemymovestate movestate { get; private set; }
    public enemybattlestate battlestate { get; private set; }
    public masterattackstate attackstate { get; private set; }
    public masterstunnedstate stunnedstate { get; private set; }
    public masterdiestate diestate { get; private set; }
    public elimination elimination { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idlestate = new enemyidlestate(this,stateMachine,"Idle", this);
        movestate = new enemymovestate(this, stateMachine, "Move", this);
        battlestate = new enemybattlestate(this, stateMachine, "Move", this);
        attackstate = new masterattackstate(this, stateMachine, "Attack", this);
        stunnedstate = new masterstunnedstate(this, stateMachine, "Stunned", this);
        diestate = new masterdiestate(this, stateMachine, "Die", this);
        elimination = new elimination(this, stateMachine, "Die", this);
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
