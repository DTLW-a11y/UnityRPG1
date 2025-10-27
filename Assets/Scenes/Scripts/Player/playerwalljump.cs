using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerwalljump : PlayerState
{
    public playerwalljump(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;
        player.SetVelocity(3 * -player.facingdir , player.jumpspeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.changeState(player.airstate);
        }
        if(player.IsGroundDetected())
            stateMachine.changeState(player.idlestate);
    }
}
