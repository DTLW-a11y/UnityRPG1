using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyelimination : EnemyState
{
    EnemyFly enemy;
    SpriteRenderer spriteRenderer;
     float flashtime = .3f;
    public Flyelimination(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyFly enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy =enemy;
    }

    public override void Enter()
    {
        base.Enter();
        spriteRenderer = enemy.GetComponentInChildren<SpriteRenderer>();
        enemy.StartCoroutine(enumerator());
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
    private IEnumerator enumerator()
    {
        Color origin = spriteRenderer.color;
        
        yield return new WaitForSeconds(3f);

        spriteRenderer.color = new Color(origin.r, origin.g, origin.b, .5f);
        for (int i = 0; i <= 3; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashtime);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashtime+0.1f);
            //flashtime -= .03f;
            Debug.Log(flashtime);
        }
        enemy.gameObject.SetActive(false);
    }
}
