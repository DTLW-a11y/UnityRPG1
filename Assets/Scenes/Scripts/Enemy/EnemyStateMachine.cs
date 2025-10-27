using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState currentstate {  get; private set; }
    public void Initialize(EnemyState _startstate)
    {
        currentstate = _startstate;
        currentstate.Enter();
    }
    public void ChangeState(EnemyState _enemystate)
    {
        currentstate.Exit();
        currentstate = _enemystate;
        currentstate.Enter();
    }
}
//负责初始化状态和改变状态
