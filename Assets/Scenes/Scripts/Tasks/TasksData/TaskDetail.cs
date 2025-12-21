using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tasktype//任务类型
{
    Killenemy,//击败敌人
    Toplace,//到达地点
    Collectitem,//收集物品
    TalkToNPC,//和npc对话
    custom //其他
}
[CreateAssetMenu(fileName = "task" , menuName ="Task/TaskConfig")]//可创建多个任务
public class TaskDetail : ScriptableObject
{

    [Header("base info")]
    public int taskId;//任务id
    public tasktype taskType;//任务类型
    public string taskName;     // 任务名称
    [TextArea] public string taskDesc; // 任务描述
    public int pretaskId;//前置任务id

    [Header("aim info")]
    public int taskCount;//任务id
    public int targetId;

    [Header("reward info")]
    public int[] rewarditemId;//奖励物品id
    public int rewardEXP;


}
