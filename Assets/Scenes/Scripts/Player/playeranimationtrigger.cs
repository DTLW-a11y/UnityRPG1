using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimationtrigger : MonoBehaviour
{
    private     Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AnimationAttack()
    {
        Collider2D[] collision = Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackcheckdistance);
        foreach(var hit in collision)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                //hit.GetComponent<Enemy>().Damage();
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.dodamage(_target);
            }
                
        }
    }
}
