using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private GameObject playerThatNeedsToPass;
    public GameObject PlayerThatNeedsToPass => playerThatNeedsToPass;

    private int nextCheckpointToPassId;

    private void Start()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].track = this;
            checkpoints[i].id = i;
        }
    }

    public void CheckpointPassed(int checkpointId)
    {
        if (checkpointId != nextCheckpointToPassId)
            return;

        nextCheckpointToPassId = GetNextCheckpoint(nextCheckpointToPassId, checkpoints.Length - 1);

        if (nextCheckpointToPassId == 0)
            GameManager.Instance.OnPlayerPassedFinish.Invoke(playerThatNeedsToPass.GetComponent<Player>());
    }

    private int GetNextCheckpoint(int current, int maxId) => current == maxId ? 0 : current + 1;
}
