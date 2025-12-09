using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAirstate : PlayerState
{
    public playerAirstate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
        {
            stateMachine.changeState(player.wallsliderstate);
        }
        if (player.IsGroundDetected())
            stateMachine.changeState(player.idlestate);
        if(xInput!=0)
        {
            rb.velocity = new Vector2(xInput * player.movespeed *.8f , rb.velocity.y); 
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.changeState(player.primeattack);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.changeState(player.magicattack1);
        }
    }
}
