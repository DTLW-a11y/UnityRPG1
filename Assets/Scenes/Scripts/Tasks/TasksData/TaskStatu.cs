using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskStatu : ScriptableObject
{
    
    public enum taskstatus//任务状态
    {
        unaccepted,//未接受
        inprogress,//正在进行
        completed,//已完成
        received //已获取奖励
    }
}
