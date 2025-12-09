using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//系统可视化
public class Stat 
{
    [SerializeField] private int Value;
    private int FinalValue;
    [SerializeField] List<int> modifiers;//动态数组
    public int GetValue()
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
