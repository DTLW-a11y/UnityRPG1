using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private int damage = 30;
    private float lifeTime = 3;
    private float moveSpeed = 2;
    private LayerMask targetLayer;
    private Transform targetEnemy;
    private Rigidbody2D rb;
    private Vector2 moveToScale;
    private Vector2 newScale;
    private Vector2 currentScale;
    private float scalespeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        currentScale = gameObject.transform.localScale;
    }
    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }
    public void Update()
    {
        if (Vector2.Distance(moveToScale, currentScale) > .1f)
        {
            currentScale = gameObject.transform.localScale;
            newScale = Vector2.MoveTowards(currentScale, moveToScale, scalespeed * Time.deltaTime);
            gameObject.transform.localScale = newScale;
        }
    }

    public void Init(int damage, float lifeTime,  LayerMask targetLayer,Vector2 moveToscale , float speed)
    {
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.targetLayer = targetLayer;
        this.moveToScale = moveToscale;
        this.scalespeed = speed;

        // ³¬Ê±Ïú»Ù
        Destroy(gameObject, lifeTime);
    }

    // Åö×²ÉËº¦
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            stats.takedamage(damage);
            //Destroy(gameObject);
        }
    }
}
