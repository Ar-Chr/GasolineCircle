using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private Player playerThatNeedsToPass;
    public Player PlayerThatNeedsToPass => playerThatNeedsToPass;

    public Events.EventPlayerPassedFinish OnPlayerPassedFinish;

    private int nextCheckpointToPassId;

    private void Start()
    {
        OnPlayerPassedFinish.AddListener(GameManager.Instance.PlayerPassedFinish);
    }

    public void CheckpointPassed(int checkpointId)
    {
        if (checkpointId != nextCheckpointToPassId)
            return;

        OnPlayerPassedFinish.Invoke(playerThatNeedsToPass);
        nextCheckpointToPassId = GetNextCheckpoint(nextCheckpointToPassId, checkpoints.Length - 1);
    }

    private int GetNextCheckpoint(int current, int maxId) => current == maxId ? 0 : current + 1;
}
