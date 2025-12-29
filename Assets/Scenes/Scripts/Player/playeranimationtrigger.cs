using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimationtrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AnimationAttack()
    {
        Collider2D[] collision = Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackcheckdistance);
        foreach(var hit in collision)
        {
            if (hit.GetComponent<EnemyMaster>())
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.dodamage(_target);

                if(Inventory.Instance.getEquipment(EquipmentType.Weapon))//判断装备了武器
                Inventory.Instance.getEquipment(EquipmentType.Weapon).ExecuteItemEffect();//
            }
            else if (hit.GetComponent<EnemyFly>())
            {
                FlyEnemyStats _target = hit.GetComponent<FlyEnemyStats>();
                player.stats.dodamage(_target);

                if (Inventory.Instance.getEquipment(EquipmentType.Weapon))
                    Inventory.Instance.getEquipment(EquipmentType.Weapon).ExecuteItemEffect();//
            }
            else if (hit.GetComponent<Boss>())
            {
                BossStats _target = hit.GetComponent<BossStats>();
                player.stats.dodamage(_target);

                if (Inventory.Instance.getEquipment(EquipmentType.Weapon))//判断装备了武器
                    Inventory.Instance.getEquipment(EquipmentType.Weapon).ExecuteItemEffect();//
            }
        }
    }
}
