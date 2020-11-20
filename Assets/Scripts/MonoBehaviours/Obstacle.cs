using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Status inflictedStatus;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (inflictedStatus != null)
                player.AddEffect(inflictedStatus);
            Destroy(gameObject);
        }
    }
}
