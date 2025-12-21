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
        currentActiveCheckpoint = checkpointId;

        if (SaveManager.instance != null)
        {
            var gameData = SaveManager.instance.GetCurrentGameData();
            if (gameData != null)
            {
                gameData.playerPosition = position;
                gameData.currentCheckpoint = checkpointId;
            }

            if (gameData.activatedCheckpoints != null)
            {
                if (gameData.activatedCheckpoints.ContainsKey(checkpointId))
                    gameData.activatedCheckpoints[checkpointId] = true;
                else gameData.activatedCheckpoints.Add(checkpointId, true);
            }
            Debug.Log($"gameData updated:playerPositon = {position},activatedCheckpoint = {checkpointId}");
        }
        else Debug.Log("Failed to activate checkpoint!");
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
        Checkpoint targetCheckpoint = checkpoints.Find(cp => cp.checkpointId == currentActiveCheckpoint);
        if(targetCheckpoint == null)
        {
            Debug.Log($"cannot find checkpoint with id {currentActiveCheckpoint}");
        }
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.Log("cannot find object \"Player");
            player.transform.position = targetCheckpoint.transform.position;
        }
        else
        {

        }
    }

   
}
