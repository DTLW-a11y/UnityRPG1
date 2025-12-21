using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;


    public Skill Skill;
    public FireBallSkill FireBallSkill;
    private Transform player;

    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    public void Start()
    {
        Skill = GetComponent<Skill>();
        FireBallSkill = GetComponent<FireBallSkill>();

        player = PlayerManager.instance.playerentity.transform; //角色管理器获得角色
    }
    public void Update()//按键触发技能
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            FireBallSkill.CastSkill(player);
            
            Debug.Log("cast fireball");
        }
    }
}
