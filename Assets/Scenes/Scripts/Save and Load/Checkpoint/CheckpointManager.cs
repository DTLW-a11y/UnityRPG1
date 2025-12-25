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
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void ActivateChecpoint(string checkpointId,Vector3 position)
    {
        GameData data = SaveManager.instance.GetCurrentGameData();
        data.currentCheckpoint = checkpointId;
        data.playerPosition = position;

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
                if (_data.activatedCheckpoints.ContainsKey(checkpoint.checkpointId))
                {
                    checkpoint.SetActivated(_data.activatedCheckpoints[checkpoint.checkpointId]);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.currentCheckpoint = currentActiveCheckpoint;
        _data.activatedCheckpoints.Clear();
        foreach(var checkpoint in checkpoints)
        {
            _data.activatedCheckpoints.Add(checkpoint.checkpointId, checkpoint.IsActivated);
        }
    }
    public void RestoreFromLastCheckpoint()
    {
        GameData _data = SaveManager.instance.GetCurrentGameData();
        PlayerManager.instance.player.transform.position = _data.playerPosition;
        PlayerState stats = PlayerManager.instance.player.GetComponent<PlayerState>();
    }
}
