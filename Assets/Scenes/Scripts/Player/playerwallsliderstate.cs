using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerwallsliderstate : PlayerState
{
    public playerwallsliderstate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.changeState(player.walljumpstate);
            return;
        }

        if(yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y );
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .85f);

        if (xInput != 0 && player.facingdir != xInput)
        {
            stateMachine.changeState(player.idlestate);
        }
        if(player.IsGroundDetected())
        {
            stateMachine.changeState(player.idlestate);
        }
    }
}
