using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskType : ScriptableObject
{
    public enum tasktype//任务类型
    {
        Killenemy,//击败敌人
        Toplace,//到达地点
        Collectitem,//收集物品
        TalkToNPC,//和npc对话
        custom //其他
    }
}
