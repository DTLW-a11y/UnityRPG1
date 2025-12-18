using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();


    private void AnimationTrigger()//动画播放完成
    {
        enemy.AnimationFinishTrigger();
    }
    private void AnimationFire()
    {
        
    }
}
