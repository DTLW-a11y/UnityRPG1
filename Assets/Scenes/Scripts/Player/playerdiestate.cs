using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdiestate : PlayerState
{
    public playerdiestate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
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
        player.ZeroVelocity();
        if (triggerCalled)
        {
            //Debug.Log("die");
            player.StartCoroutine(enumerator(0f));
        }
    }
    private IEnumerator enumerator(float time)
    {
        yield return new WaitForSeconds(time);
        stateMachine.changeState(player.elimination);
    }
}
