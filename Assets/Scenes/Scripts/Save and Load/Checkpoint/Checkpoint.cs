using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string checkpointId = "checkpoint_001";
    public bool isActivated = false;
    public bool IsActivated => isActivated;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isActivated)
        {
            ActivateCheckpoint();
        }
    }
    public void ActivateCheckpoint()
    {
        isActivated = true;
        if(CheckpointManager.instance != null)
        {
            CheckpointManager.instance.ActivateChecpoint(checkpointId, transform.position);
        }
    }
    public void SetActivated(bool activated)
    {
        isActivated = activated;
    }
}
