using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private int damage = 10;
    private float lifeTime = 3;
    private float moveSpeed = 1;
    private LayerMask targetLayer;
    private Transform targetEnemy;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }

    public void Init(int damage, float lifeTime, float moveSpeed, LayerMask targetLayer)
    {
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.moveSpeed = moveSpeed;
        this.targetLayer = targetLayer;

        Vector2 direction = (targetEnemy.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed; //初始速度

        // 超时销毁
        Destroy(gameObject, lifeTime);
    }

    // 碰撞伤害
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            stats.takedamage(damage);
            Destroy(gameObject);
        }
    }
}
