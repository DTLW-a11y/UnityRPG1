using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;

    public Player player;

    public int currency;
    public void Awake()
    {
        instance = this;
    }

    public int Getcurrency() => currency;

    public void LoadData(GameData _data)
    {
        this.currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency =this.currency;
    }
}
