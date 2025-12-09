using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdashstate : PlayerState
{
    public playerdashstate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashduration ;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if(!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.changeState(player.wallsliderstate);
        player.SetVelocity(player.dashspeed * player.dashdir , 0);
        if (stateTimer < 0)
        {
            stateMachine.changeState(player.idlestate);
        }
    }
}
