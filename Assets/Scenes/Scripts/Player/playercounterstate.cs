using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercounterstate : PlayerState
{
    public playercounterstate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.anim.SetBool("SuccessfulCounter",false);
        stateTimer = player.counterduration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackcheckdistance);
        foreach (var hit in colliders)
        {
             
            if (hit.GetComponent<Enemy>() != null)
            {
                if(hit.GetComponent<Enemy>().canbestunned())
                    {
                    stateTimer = 10;
                    player.anim.SetBool("SuccessfulCounter", true);
                    }
            }
        }
        if (stateTimer < 0 || triggerCalled)
        {
            player.anim.SetBool("SuccessfulCounter", false);
            stateMachine.changeState(player.idlestate);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            time += Time.deltaTime;

            stateMachine.changeState(player.counteringstate);
        }
    }
}
