using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerprimeattack : PlayerState
{
    private int comboCounter;
    private float lasttimeAttacked;
    private float comboWindow = 2;
    public playerprimeattack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter >2 ||Time.time >= lasttimeAttacked + comboWindow)
        {
            comboCounter = 0;
           // Debug.Log("++");
        }
        player.anim.SetInteger("ComboCounter",comboCounter);
        stateTimer = .1f;
        #region attackdir
        float attackdir = player.facingdir;
        if (xInput!= 0)
            attackdir = xInput;
        #endregion
        player.SetVelocity(attackdir * player.attackMovement[comboCounter] , rb.velocity.y);
    }

    public override void Exit()
    {
        comboCounter++;
        lasttimeAttacked = Time.time;

        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.ZeroVelocity();
        if (triggerCalled)
            stateMachine.changeState(player.idlestate);
    }
}
