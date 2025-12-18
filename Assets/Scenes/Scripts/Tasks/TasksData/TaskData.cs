using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData 
{
    public int taskid;
    public TaskStatu.taskstatus taskstatus;
    public int progress;

    public TaskData(int taskid , TaskStatu.taskstatus taskstatus,int progress = 0)
    {
        this.taskid = taskid;
        this.taskstatus = taskstatus;
        this.progress = progress;
    }
}
