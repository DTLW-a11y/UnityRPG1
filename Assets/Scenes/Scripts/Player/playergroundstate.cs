using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playergroundstate : PlayerState
{
    public playergroundstate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            stateMachine.changeState(player.counterstate);
        }
        if(!player.IsGroundDetected())
        {
            stateMachine.changeState(player.airstate);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.changeState(player.jumpstate);
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
