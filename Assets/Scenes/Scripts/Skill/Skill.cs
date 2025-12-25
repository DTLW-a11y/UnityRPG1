using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CastSkill(Transform caster)//释放技能
    {
        if(cooldownTimer < 0)
        {
            SkillType(caster);
            cooldownTimer = cooldown;
            return true;
        }
        Debug.Log("冷却");
        return false;
    }
    public virtual void SkillType(Transform caster)//技能类型，子类覆写
    {

    }
    public Transform FindEnemy(Vector2 player , int radius , LayerMask layer)//索敌,寻找最近的敌人
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player, radius, layer);
        float closedistance = Mathf.Infinity;
        Transform target = null;
        foreach (Collider2D enemy in colliders)
        {
            float distance = Vector2.Distance(player, enemy.transform.position);
            if (enemy.GetComponent<EnemyStats>())
            {
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                if (distance < closedistance && !enemyStats.isdied)
                {
                    closedistance = distance;
                    target = enemy.transform;
                }
            }
            else if (enemy.GetComponent<FlyEnemyStats>())
            {
                FlyEnemyStats enemyStats = enemy.GetComponent<FlyEnemyStats>();
                if (distance < closedistance && !enemyStats.isdied)
                {
                    closedistance = distance;
                    target = enemy.transform;
                }
            }
        }
        return target;
    }
}
