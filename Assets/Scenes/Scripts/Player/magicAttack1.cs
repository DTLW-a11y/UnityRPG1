using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicAttack1 : PlayerState
{
    Player player;
    public magicAttack1(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        this.player = _player;
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
        if (triggerCalled)
            stateMachine.changeState(player.idlestate);
    }
}
