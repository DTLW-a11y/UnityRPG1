using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData 
{
    public int taskId;
    public TaskStatu.taskstatus taskstatus;
    public int progress;

    public TaskData(int taskId , TaskStatu.taskstatus taskstatus,int progress = 0)
    {
        this.taskId = taskId;
        this.taskstatus = taskstatus;
        this.progress = progress;
    }
}
