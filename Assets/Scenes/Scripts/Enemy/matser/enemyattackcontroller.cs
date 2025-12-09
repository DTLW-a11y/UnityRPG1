using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattackcontroller : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();
    

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AnimationAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackcheckdistance);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
               // hit.GetComponent<Player>().Damage();
                PlayerStats _target = hit.GetComponent<PlayerStats>();
                enemy.stats.dodamage(_target);
            }
        }

    }
    private void CanStunWindowOpen()
    {
        enemy.CanStunnedWindowOpen();
    }
    private void CanStunWindowClosed()
    {
        enemy.CanStunnedWindowClosed();
    }
}
