using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counteringstate : PlayerState
{
    private float maxTime = 3;
    public counteringstate(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //
        if (stateTimer < 0 || triggerCalled)
        {
            player.anim.SetBool("SuccessfulCounter", false);
            stateMachine.changeState(player.idlestate);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if(time < maxTime)
            time += Time.deltaTime;
            else if (time >= maxTime)
                time = maxTime;

            stateMachine.changeState(player.counteringstate);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1)&& time > 1)
        {

            if (Inventory.Instance.SkillDictionary.TryGetValue(SkillManager.instance.abilityrequirements[1].ItemData, out var itemData)) //如果有对应数据，解锁了技能
                SkillManager.instance.CounterSkill.CastSkill(PlayerManager.instance.player.transform);

            time = 0;
            stateMachine.changeState(player.idlestate);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && time <= 1)
        {
            time = 0;
        }
    }
}
