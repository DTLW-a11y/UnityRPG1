using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/ItemEffect/LevelEffect")]
public class LevelEffect : ItemEffect
{
    [SerializeField] int healthToChange;
    public override void ExecuteEffect()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();//获得玩家血量

        //修改玩家血量
        playerStats.Increasehealthby(healthToChange);
    }
}
