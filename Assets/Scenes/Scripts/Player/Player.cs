using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [Header("Attack details")]
    public float[] attackMovement;
    public float counterduration;

    [Header("move info")]
    public float movespeed = 12f;
    public float jumpspeed;

    [Header("dash info")]
    [SerializeField] private float dashcooldown;
    [SerializeField] private float dashusetime;
    public float dashspeed;
    public float dashduration;
    public float dashdir {  get; private set; }

    public Transform countercheck;
    public Vector2 boxsize;
    
    

    #region States
    public PlayerStateMachine stateMachine { get; private set; }//
    public playeridlestate idlestate {  get; private set; }//
    public playermovestate movestate { get; private set; }//
    public playerJumpstate jumpstate { get; private set; }
    public playercounterstate counterstate { get; private set; }
    public playerAirstate airstate { get; private set; }
    public playerdashstate dashstate { get; private set; }
    public playerwallsliderstate wallsliderstate { get; private set; }
    public playerwalljump walljumpstate { get; private set; }
    public playerprimeattack primeattack { get; private set; }
    public playerdiestate diestate { get; private set; }
    public eliminationplayer elimination { get; private set; }

    public magicAttack1 magicattack1 { get; private set; }

    public counteringstate counteringstate { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();//
        idlestate = new playeridlestate(this, stateMachine , "Idle");
        movestate = new playermovestate(this, stateMachine, "Move");
        jumpstate = new playerJumpstate(this, stateMachine, "Jump");
        airstate  = new playerAirstate(this, stateMachine, "Jump");
        dashstate = new playerdashstate(this, stateMachine, "Dash");
        wallsliderstate = new playerwallsliderstate(this, stateMachine, "WallSlide");
        walljumpstate = new playerwalljump(this, stateMachine, "WallJump");
        primeattack = new playerprimeattack(this, stateMachine, "Attack");
        counterstate = new playercounterstate(this, stateMachine, "CounterAttack");
        diestate = new playerdiestate(this, stateMachine, "Die");
        elimination = new eliminationplayer(this, stateMachine, "Die");
        magicattack1 = new magicAttack1(this, stateMachine, "MagicAttack1");
        counteringstate = new counteringstate(this, stateMachine, "CounterAttack");

        transform.position = SpawnManager.position;//切换场景时出生在位置


    }
    public void CheckDash()
    {
        if(IsWallDetected())
            { return; }
        dashusetime -= Time.deltaTime;  
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashusetime < 0)
        {
            dashusetime = dashcooldown;
            dashdir = Input.GetAxisRaw("Horizontal");
            if (dashdir == 0)
                dashdir = facingdir;
            stateMachine.changeState(dashstate);
        }
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idlestate);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckDash();

        //if (Input.GetKeyDown(KeyCode.Y))
        //    player.Decreasemana(20);
        
    }
    //CharacterStats player;
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
    }
}

