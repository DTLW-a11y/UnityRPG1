using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;


    public Skill Skill;
    public FireBallSkill FireBallSkill;
    public CounterSkill CounterSkill;
    private Transform player;

    public List<InventoryItem> requirements;//技能要求
    public List<InventoryItem> abilityrequirements;//能力要求

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        Skill = GetComponent<Skill>();
        FireBallSkill = GetComponent<FireBallSkill>();
        CounterSkill = GetComponent<CounterSkill>();

    }
    public void Update()//按键触发技能
    {
        if(Inventory.Instance.SkillDictionary.TryGetValue(requirements[0].ItemData, out var itemData))//如果有对应数据，解锁了技能
        if(Input.GetKeyDown(KeyCode.U))
        {
            FireBallSkill.CastSkill(PlayerManager.instance.playerentity.transform);
            
            Debug.Log("cast fireball");
        }
    }
}
