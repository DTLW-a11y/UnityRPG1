using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    [Header("Level Details")]//等级
    //[SerializeField] private int level = 1;
    //[SerializeField] private int manalevel = 1;
    [Range(0f, 1f)]
    [SerializeField] private float percentage = .1f;


    //[SerializeField] int currentEXP;
    //[SerializeField] int maxEXP;
    protected override void Start()
    {
        base.Start();
       // AddModifiers();
        player = GetComponent<Player>();

        EXPSystem.instance.onLevelUp += levelup;
        EXPSystem.instance.onmanaLevelUp += manalevelup;
    }
    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);
        player.DamageEffect();
    }

    protected override void die()
    {
        base.die();
        player.stateMachine.changeState(player.diestate);

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    private void Modify(Stat _stat)//数值随等级指数级增长
    {
        
            float modifier = _stat.GetValue() * percentage;
            _stat.addmodifier(Mathf.RoundToInt(modifier));
       
    }
    private void ModifyMana(Stat _stat)//魔力增加，线性
    {
        
            float modifier = 0f;
            modifier += 20;
            _stat.addmodifier(Mathf.RoundToInt(modifier));
        
    }

    private void AddModifiers() // 随等级增长修改的属性
    {
        Modify(damage);
        Modify(strenth);

        Modify(armor);
        Modify(maxHP);

        //ModifyMana(intelligence);
    }

    private void levelup()
    {
        AddModifiers();
    }
    private void manalevelup()
    {
        ModifyMana(maxMana);
    }

    public void OnDestroy()//销毁物品时取消订阅，防止内存泄露
    {
        if(EXPSystem.instance != null)
        {
            EXPSystem.instance.onLevelUp -= levelup;
            EXPSystem.instance.onmanaLevelUp -= manalevelup;
        }
    }
}
