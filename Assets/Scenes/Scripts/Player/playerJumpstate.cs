using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJumpstate : PlayerState
{
    public playerJumpstate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpspeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(rb.velocity.y < 0)
        {
            stateMachine.changeState(player.airstate);
        }
    }
}
