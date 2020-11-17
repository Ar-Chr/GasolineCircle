using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Track track;
    private int checkpointId;

    private void OnTriggerEnter(Collider other)
    {
        if (track.PlayerThatNeedsToPass == null)
            return;

        if (other.gameObject.GetComponent<Player>() != track.PlayerThatNeedsToPass)
            return;

        track.CheckpointPassed(checkpointId);
    }
}
