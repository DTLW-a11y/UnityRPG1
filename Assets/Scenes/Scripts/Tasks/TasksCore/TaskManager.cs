using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

#if UNITY_EDITOR
using UnityEditor.Build.Reporting;
#endif
public class TaskManager : MonoBehaviour,ISaveManager
{
    
    public static TaskManager instance;

    public event Action OntaskProgressChange;//任务进程变更
    public event Action OntaskStatuChange;//任务状态变更

    [Header("任务")]
    public Dictionary<int, TaskDetail> totaltasks;//总任务,可供查询任务细节
    public Dictionary<int, TaskData> currenttasks;//目前玩家已经接触的任务，可查询各种任务状态

    public void Awake()
    {
        
        if (instance == null )
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        totaltasks = new Dictionary<int, TaskDetail>();
        currenttasks = new Dictionary<int, TaskData>();
        Init();
    }

    #region//初始化
    public void Init()
    {
        TaskDetail[] taskDetails = Resources.LoadAll<TaskDetail>("Tasks");

        foreach (TaskDetail taskDetail in taskDetails)
        {
            if(totaltasks.ContainsKey(taskDetail.taskId))
            {
                Debug.Log("已存在相同任务");
                return;
            }
            else
            {
                totaltasks.Add(taskDetail.taskId, taskDetail);
            }
        }
    }
    #endregion

    #region//接取任务
    public void AcceptTask(int _taskid)
    {
        if(!totaltasks.ContainsKey(_taskid))
        {
            Debug.Log("不存在该任务");
            return;
        }
        if(currenttasks.ContainsKey(_taskid))
        {
            Debug.Log("已接取该任务");
            return;
        }
        TaskDetail task = totaltasks[_taskid];
        if(task.pretaskId!=-1)
        {
            if (!currenttasks.ContainsKey(task.pretaskId) || currenttasks[task.pretaskId].taskstatus != TaskStatu.taskstatus.received)//检查前置任务
            {
                Debug.Log("前置任务未完成");
                return;
            }
        }
        Debug.Log("ttt");
        TaskData newdata = new TaskData(_taskid, TaskStatu.taskstatus.inprogress, 0);
        currenttasks.Add(_taskid, newdata);
        OntaskStatuChange?.Invoke();//状态变更，通知ui
    }
    #endregion

    #region//任务进度更新
    public void UpdateProgress(tasktype tasktype, int targetid ,int addCount =1)
    {
        foreach (var task in currenttasks)//遍历已有任务
        {
            //int progress = task.Value.progress;
            int taskid = task.Key;
            TaskData taskData = currenttasks[taskid];
            TaskDetail taskDetail = totaltasks[taskid];
            int targetnum = taskDetail.taskCount;

            if (taskData.taskstatus != TaskStatu.taskstatus.inprogress)
                continue;

            if (taskDetail.targetId == targetid && taskDetail.taskType == tasktype)
            {
                //更新进度
                int tot = task.Value.progress + addCount;
                if (tot < targetnum)
                {
                    task.Value.progress += addCount;
                    OntaskProgressChange?.Invoke();
                    Debug.Log("任务进度更新");
                }
                else
                {
                    task.Value.progress = targetnum;
                    Complete(taskid);
                    OntaskProgressChange?.Invoke();
                }
            }
        }
    }
    #endregion

    #region//完成任务，获得奖励
    public void Complete(int _taskid)
    {
        if (!currenttasks.ContainsKey(_taskid)) return;
        currenttasks[_taskid].taskstatus = TaskStatu.taskstatus.completed;
        Reward(_taskid);
        OntaskStatuChange?.Invoke();
        Debug.Log("任务完成");
    }
    public void Reward(int _taskid)
    {
        if (!currenttasks.ContainsKey(_taskid)) return;
        TaskDetail taskDetail = totaltasks[_taskid];

        EXPSystem.instance.AddEXP(taskDetail.rewardEXP);//增加经验
        foreach(var ID in taskDetail.rewarditemId) //增加物品
        {
            Inventory.Instance.AddItem(ID);
        }
    }
    #endregion

    public void LoadData(GameData _data)
    {
        //加载总任务数据
        totaltasks.Clear();
        foreach (var saveData in _data.totalTasksSave)
        {
            var detail = new TaskDetail();
            detail.taskId = saveData.taskId;
            detail.taskType = (tasktype)saveData.taskType; 
            detail.taskName = saveData.taskName;
            detail.taskDesc = saveData.taskDesc;
            detail.pretaskId = saveData.pretaskId;
            detail.taskCount = saveData.taskCount;
            detail.targetId = saveData.targetId;
            detail.rewarditemId = saveData.rewarditemId;
            detail.rewardEXP = saveData.rewardEXP;
            totaltasks[saveData.taskId] = detail;
        }

        // 加载当前任务数据
        currenttasks.Clear();
        foreach (var saveData in _data.currentTasksSave)
        {
            var taskData = new TaskData(
                saveData.taskId,
                (TaskStatu.taskstatus)saveData.status,
                saveData.progress
            );
            currenttasks[saveData.taskId] = taskData;
        }

        OntaskStatuChange?.Invoke(); // 通知UI更新
    }

    public void SaveData(ref GameData _data)
    {
        // 清空已有数据
        _data.totalTasksSave.Clear();
        _data.currentTasksSave.Clear();

        // 保存总任务数据
        foreach (var task in totaltasks.Values)
        {
            _data.totalTasksSave.Add(new TaskDetailSave
            {
                taskId = task.taskId,
                taskType = (int)task.taskType,
                taskName = task.taskName,
                taskDesc = task.taskDesc,
                pretaskId = task.pretaskId,
                taskCount = task.taskCount,
                targetId = task.targetId,
                rewarditemId = task.rewarditemId,
                rewardEXP = task.rewardEXP,
            });
        }

        // 保存当前任务数据
        foreach (var task in currenttasks.Values)
        {
            _data.currentTasksSave.Add(new TaskDataSave
            {
                taskId = task.taskId,
                status = (int)task.taskstatus,
                progress = task.progress
            });
        }
    }
}
