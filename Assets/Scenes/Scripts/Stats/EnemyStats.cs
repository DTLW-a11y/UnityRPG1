using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private EnemyMaster enemy;
    private ItemDrop myDropSystem;

    [Header("Level Details")]//�ȼ�
    [SerializeField] private int level = 1;
    [Range(0f, 1f)]
    [SerializeField] private float percentage = .1f;
    protected override void Start()
    {
        AddModifiers();

        base.Start();
        enemy = GetComponent<EnemyMaster>();
        myDropSystem = GetComponent<ItemDrop>();

    }

    private void AddModifiers() // ��ȼ������޸ĵ�����
    {
        Modify(damage);
        Modify(strenth);

        Modify(armor);
        Modify(maxHP);
    }

    private void Modify(Stat _stat)//��ֵ��ȼ�ָ��������
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.GetValue() * percentage;
            _stat.addmodifier(Mathf.RoundToInt(modifier));
        }
    }
    public override void takedamage(int _damage)
    {
        base.takedamage(_damage);
        enemy.DamageEffect();
    }
    protected override void die()
    {
        base.die();
        enemy.stateMachine.ChangeState(enemy.diestate);

        myDropSystem.GenerateDrop();
    }
}
