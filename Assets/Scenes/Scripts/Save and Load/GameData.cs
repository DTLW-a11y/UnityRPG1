using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    //需要存储的游戏数据
    public int currency;
    public SerializableDictionary<string, int> inventory;
    public GameData()
    {
        this.currency = 0;
        inventory = new SerializableDictionary<string, int>();
    }
}


