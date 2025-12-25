using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CounterSkill : Skill
{
    [SerializeField] GameObject prefab;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private LayerMask targetLayer;
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
            SkillType(caster);
            return true;
    }
    public override void SkillType(Transform caster)
    {
        base.SkillType(caster);
        //生成冲击波
        GameObject counterwall = Instantiate(prefab, caster.position, Quaternion.identity);//默认朝向,利用预制体创建物品
        CounterWall counterwallScript = counterwall.GetComponent<CounterWall>();

        //默认的速度，朝向，伤害，持续时间
        counterwallScript.Init(damage, lifeTime, moveSpeed,  targetLayer);


    }
}
