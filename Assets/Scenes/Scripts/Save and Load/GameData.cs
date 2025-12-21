using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    //需要存储的游戏数据
    public int currency;

    public SerializableDictionary<string, int> inventory;

    public Vector3 playerPosition;
    public string currentCheckpoint;
    public SerializableDictionary<string, bool> activatedCheckpoints;

    public CharacterAttributesData playerAttributes;

    public int level, manalevel, maxEXP, currentEXP;

    public SerializableDictionary<string, bool> openedChests;

    public class TaskDataSave
    {
        public int taskId;
        public int status;
        public int progress;
    }

    public class TaskDetailSave
    {
        public int taskId;
        public int taskType;
        public string taskName;
        public string taskDesc;
        public int pretaskId;

        public int taskCount;
        public int targetId;

        public int[] rewarditemId;
        public int rewardEXP;
    }

    public GameData()
    {
        this.currency = 0;
        inventory = new SerializableDictionary<string, int>();
        playerPosition = Vector3.zero;
        playerAttributes = new CharacterAttributesData();
        currentCheckpoint = "start";
        activatedCheckpoints = new SerializableDictionary<string, bool>();
        level = 0;
        manalevel = 0;
        maxEXP = 0;
        currentEXP = 0;
        openedChests = new SerializableDictionary<string, bool>();
    }
    public List<TaskDataSave> currentTasksSave = new List<TaskDataSave>();
    public List<TaskDetailSave>totalTasksSave = new List<TaskDetailSave>();
}


