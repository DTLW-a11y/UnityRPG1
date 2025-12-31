using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour,ISaveManager
{
    public static CheckpointManager instance;
    [SerializeField] private List<Checkpoint> checkpoints;
    private string currentActiveCheckpoint;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public void Start()
    {
        checkpoints = new List<Checkpoint>(FindObjectsOfType<Checkpoint>());
    }

    public void ActivateChecpoint(string checkpointId,Vector3 position,string SceneName)
    {
        GameData data = SaveManager.instance.GetCurrentGameData();
        data.currentCheckpoint = checkpointId;
        data.playerPosition = position;
        data.playerScene = SceneName;

        if (!data.activatedCheckpoints.ContainsKey(checkpointId))
            data.activatedCheckpoints.Add(checkpointId, true);
        else
            data.activatedCheckpoints[checkpointId] = true;
        SaveManager.instance.SaveAtCheckpoint();
    }

    public void SaveAtCheckpoint()
    {
        if(currentActiveCheckpoint != null)
        {
            SaveManager.instance.SaveGame();
            Debug.Log($"Game saved at {currentActiveCheckpoint}");
        }
    }
    public void LoadData(GameData _data)
    {
        if(_data.activatedCheckpoints != null)
        {
            foreach(var checkpoint in checkpoints)
            {
                if (_data.activatedCheckpoints.TryGetValue(checkpoint.checkpointId,out bool isActive))
                {
                    checkpoint.SetActivated(isActive);
                    if(isActive)currentActiveCheckpoint = checkpoint.checkpointId;
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.currentCheckpoint = currentActiveCheckpoint;
        if (_data.activatedCheckpoints != null)
        {
            _data.activatedCheckpoints.Clear();
        }
        foreach(var checkpoint in checkpoints)
        {
            _data.activatedCheckpoints.Add(checkpoint.checkpointId, checkpoint.IsActivated);
        }
    }
    public void RestoreFromLastCheckpoint()
    {
        GameData _data = SaveManager.instance.GetCurrentGameData();
        GameManager.Instance.NormalJumpToScene(_data.playerScene);
        GameManager.Instance.AfterChange += Transport;
        
    }
    public void Transport()
    {
        GameData _data = SaveManager.instance.GetCurrentGameData();
        PlayerManager.instance.player.transform.position = _data.playerPosition;
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.currentHP = _data.playerAttributes.currentHP;
            stats.isdied = false;
            stats.onhealthchange?.Invoke();
        }
        Player player = PlayerManager.instance.player;
        if (player != null && player.stateMachine != null)
        {
            player.stateMachine.changeState(player.idlestate);
        }
    }
}
