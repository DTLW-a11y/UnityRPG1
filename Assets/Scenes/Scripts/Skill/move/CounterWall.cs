using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CounterWall : MonoBehaviour
{
    private int damage = 10;
    private float lifeTime = 3;
    private float moveSpeed = 1;
    private LayerMask targetLayer;
    private Rigidbody2D rb;
    private Player player;
    float time = PlayerState.time;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.instance.player;
        rb.gravityScale = 0;
    }

    public void Init(int damage, float lifeTime, float moveSpeed,  LayerMask targetLayer)
    {
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.moveSpeed = moveSpeed;
        this.targetLayer = targetLayer;

            rb.velocity = new Vector2( player.facingdir * moveSpeed , 0); //初始速度

        // 超时销毁
        Destroy(gameObject, lifeTime);
    }


    // 碰撞伤害
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<EnemyMaster>())
        {
            EnemyStats _target = hit.GetComponent<EnemyStats>();


            if (Inventory.Instance.getEquipment(EquipmentType.Armor))//有护盾
            {
                int damage = Inventory.Instance.getEquipment(EquipmentType.Armor).damage;
                //Debug.Log(damage);
                _target.takedamage(Mathf.RoundToInt(time * (damage + 30)));
            }//伤害等于蓄力时间*护盾
            else//无护盾
                _target.takedamage(Mathf.RoundToInt(time * 30));

        }
        else if (hit.GetComponent<EnemyFly>())
        {
            FlyEnemyStats _target = hit.GetComponent<FlyEnemyStats>();

            if (Inventory.Instance.getEquipment(EquipmentType.Armor))//有护盾
            {
                int damage = Inventory.Instance.getEquipment(EquipmentType.Armor).damage;
                //Debug.Log(damage);
                _target.takedamage(Mathf.RoundToInt(time * (damage + 30)));
            }//伤害等于蓄力时间*护盾
            else//无护盾
                _target.takedamage(Mathf.RoundToInt(time * 30));

        }
        else if (hit.GetComponent<Boss>())
        {
            BossStats _target = hit.GetComponent<BossStats>();
            Boss boss = hit.GetComponent<Boss>();

            if (Inventory.Instance.getEquipment(EquipmentType.Armor))//有护盾
            {
                int damage = Inventory.Instance.getEquipment(EquipmentType.Armor).damage;
                //Debug.Log(damage);
                _target.takedamage(Mathf.RoundToInt(time * (damage + 30)));
            }//伤害等于蓄力时间*护盾
            else//无护盾
                _target.takedamage(Mathf.RoundToInt(time * 30));

            boss.stateMachine.ChangeState(boss.stunnedstate);

        }
        else if ((hit.GetComponent<Rock>()))
            {
            Debug.Log("touch");
            Rock rock = hit.GetComponent<Rock>();
            rock.ThingToDo();
        }
    }
}
