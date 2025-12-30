using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("dashstat")]
    public int dashdistance;
    public int dashspeed;
    public float dashduration;
    [Header("airstat")]
    public int height;
    public float upspeed;
    public Transform detectcenter;
    public int detectdistance;
    [Header("skillstat")]
    public float skillcooldown;
    public float holecooldown;

    public int count = 0;
    public float time = 0;
    #region States
    public Bossidlestate idlestate {  get; private set; }
    public BossDashPreparestate dashprestate { get; private set; }
    public BossDashingstate dashstate { get; private set; }
    public Bossmovestate movestate { get; private set; }
    public Bossbattlestate battlestate { get; private set; }
    public Bossattackstate attackstate { get; private set; }
    public Bossstunnedstate stunnedstate { get; private set; }
    public Bossdiestate diestate { get; private set; }
    public Bosselimination elimination { get; private set; }
    public Bossairstate airstate { get; private set; }
    public BossEnterairstate enterairstate { get; private set; }
    public BossCastSkillstate skillstate { get; private set; }
    public BossCastSkill1state skill1state { get; private set; }
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
        dashstate = new BossDashingstate(this, stateMachine, "Dash", this);
        dashprestate = new BossDashPreparestate(this, stateMachine, "DashPre", this);
        airstate = new Bossairstate(this, stateMachine, "Air", this);
        enterairstate = new BossEnterairstate(this, stateMachine, "EnterAir", this);
        skillstate = new BossCastSkillstate(this, stateMachine, "Skill", this);
        skill1state = new BossCastSkill1state(this, stateMachine, "Skill1", this);
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
    protected override void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(detectcenter.position, detectdistance);
    }
}
