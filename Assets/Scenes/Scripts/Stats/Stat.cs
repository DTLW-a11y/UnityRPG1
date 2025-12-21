using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//ϵͳ���ӻ�
public class Stat 
{
    [SerializeField] private int Value;
    private int FinalValue;
    [SerializeField] List<int> modifiers;//��̬����
    public int GetValue()//�õ��޸ĺ����ֵ
    {
        FinalValue = Value;
        foreach (var modifier in modifiers)
        {
            FinalValue += modifier;
        }
        return FinalValue;
    }
    public void addmodifier(int _equip)
    {
        modifiers.Add(_equip);
    }

    public void removemodifier(int _equip)
    {
        modifiers.Remove(_equip);
    }
    public void Setvalue(int value)
    {
        Value = value;
    }
}
