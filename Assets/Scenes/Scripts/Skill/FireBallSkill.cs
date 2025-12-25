using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : Skill
{
    [SerializeField] GameObject prefab;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private int searchRadius = 200;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int fireManaCost;
    private PlayerStats playerStats;
    void Start()
    {
        playerStats = PlayerManager.instance.playerentity.GetComponent<PlayerStats>();
    }
    protected override void Update()
    {
        base.Update();
    }
    public override bool CastSkill(Transform caster)
    {
        //蓝量判断add

        if (cooldownTimer < 0 && playerStats.CanDecreaseMana(fireManaCost))
        {
            SkillType(caster);
            cooldownTimer = cooldown;
            return true;
        }
        Debug.Log("冷却");
        return false;
    }
    public override void SkillType(Transform caster)
    {
        base.SkillType(caster);
        //生成火球
        GameObject fireball = Instantiate(prefab , caster.position, Quaternion.identity);//默认朝向,利用预制体创建物品
        FireBall fireBallScript = fireball.GetComponent<FireBall>();

        Transform target = FindEnemy(caster.position , searchRadius , targetLayer);
        fireBallScript.SetTarget(target);

        //默认的速度，朝向，伤害，持续时间
        fireBallScript.Init(damage, lifeTime, moveSpeed, rotateSpeed,searchRadius, targetLayer);


    }
}
