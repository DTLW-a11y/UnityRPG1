using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/ItemEffect/LevelEffect")]
public class LevelEffect : ItemEffect
{
    [SerializeField] int AdddingEXP;
    public override void ExecuteEffect()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();//获得玩家

        //修改玩家经验
        EXPSystem.instance.AddEXP(AdddingEXP);
    }
}
