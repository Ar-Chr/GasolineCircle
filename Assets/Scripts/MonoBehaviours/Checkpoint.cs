using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Track track;
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (track.PlayerThatNeedsToPass == null)
            return;

        if (other.gameObject != track.PlayerThatNeedsToPass)
            return;
        Debug.Log("Checkpoint passed by correct player");
        track.CheckpointPassed(id);
    }
}
