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
            //rb.velocity = new Vector2(xInput * player.movespeed *.8f , rb.velocity.y);
            player.SetVelocity(xInput * player.movespeed * .8f, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.changeState(player.primeattack);
        }
        //二段跳
        if (Inventory.Instance.SkillDictionary.TryGetValue(SkillManager.instance.abilityrequirements[0].ItemData, out var itemData) && JumpCount < 2) //如果有对应数据，解锁了技能
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.changeState(player.jumpstate);
            }

    }
}
