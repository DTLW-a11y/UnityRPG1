using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    //需要存储的游戏数据
    public int currency;
    public SerializableDictionary<string, int> inventory;
    public Vector3 playerPosition;
    public string currentCheckpoint;
    public SerializableDictionary<string, bool> activatedCheckpoints;
    public GameData()
    {
        this.currency = 0;
        inventory = new SerializableDictionary<string, int>();
        playerPosition = Vector3.zero;
        currentCheckpoint = "start";
        activatedCheckpoints = new SerializableDictionary<string, bool>();
    }
}


