using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float destructionThreshold;
    [Space]
    [SerializeField] private string inflictedStatusClassName;
    private Status inflictedStatus;

    private void Start()
    {
        inflictedStatus = (Status)Type
            .GetType(inflictedStatusClassName)
            ?.GetConstructor(new Type[0])
            .Invoke(new object[0]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (collision.relativeVelocity.magnitude > destructionThreshold)
            {
                if (inflictedStatus != null)
                    player.AddEffect(inflictedStatus);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (inflictedStatus != null)
                player.AddEffect(inflictedStatus);
            Destroy(gameObject);
        }
    }
}
