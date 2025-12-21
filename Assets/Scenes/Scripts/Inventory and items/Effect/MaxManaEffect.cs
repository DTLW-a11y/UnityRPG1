using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "maxmanaEffect" , menuName = "Data/ItemEffect/Manaeffect/MaxManaeffect")]
public class MaxManaEffect : ItemEffect
{
    public override void ExecuteEffect()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        //manaµÈ¼¶ÌáÉý
        EXPSystem.instance.manalevelUP();
    }
}
