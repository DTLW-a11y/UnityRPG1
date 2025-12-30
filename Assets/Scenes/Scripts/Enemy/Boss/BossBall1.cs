using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall1: MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Vector2 targetScale;
    [SerializeField] private float speed;
    private Enemy enemy => GetComponentInParent<Enemy>();


    private void AnimationTrigger()//动画播放完成
    {
        enemy.AnimationFinishTrigger();
    }
    private void AnimationFire1()
    {
        //生成火球
        GameObject fireball = Instantiate(prefab, enemy.transform.position, Quaternion.identity);//默认朝向,利用预制体创建物品
        BlackHole MagicBallScript = fireball.GetComponent<BlackHole>();

        Transform target = PlayerManager.instance.playerentity.transform;
        MagicBallScript.SetTarget(target);

        //默认的速度，朝向，伤害，持续时间
        MagicBallScript.Init(damage, lifeTime,  targetLayer , targetScale , speed);
    }
}
