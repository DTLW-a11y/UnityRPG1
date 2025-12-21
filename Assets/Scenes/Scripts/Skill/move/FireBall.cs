using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private int damage =10;
    private float lifeTime =3;
    private float moveSpeed =1;
    private float rotateSpeed = 1;
    private float searchRadius;
    private LayerMask targetLayer;
    private Transform targetEnemy;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void Init(int damage, float lifeTime, float moveSpeed, float rotateSpeed, float searchRadius, LayerMask targetLayer)
    {
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;
        this.searchRadius = searchRadius;
        this.targetLayer = targetLayer;

        Vector2 direction = (targetEnemy.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed; //初始速度

        // 超时销毁
        Destroy(gameObject, lifeTime);
    }

    // 设置追踪目标
    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }

    private void FixedUpdate()
    {
        SmoothTrackTarget();
    }
    // 平滑转向追踪
    private void SmoothTrackTarget()
    {
        Vector2 targetDir = (targetEnemy.position - transform.position).normalized;
        Vector2 newDir = Vector2.Lerp(rb.velocity.normalized, targetDir, rotateSpeed * Time.fixedDeltaTime);//线性插值修改速度朝向
        rb.velocity = newDir * moveSpeed;

        float angle = Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg - 90f;//弧度转化为角度 调整物体朝向
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 碰撞伤害
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMaster>())
        {
            EnemyStats stats = other.GetComponent<EnemyStats>();
            stats.takedamage(damage);
            Destroy(gameObject);
        }
        else if (other.GetComponent<EnemyFly>())
        {
            FlyEnemyStats stats = other.GetComponent<FlyEnemyStats>();
            stats.takedamage(damage);
            Destroy(gameObject);
        }
    }
}
