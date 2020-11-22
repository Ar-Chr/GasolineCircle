using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAcceleration : Status
{
    private float acceleration;

    public ContinuousAcceleration() : this(5f, 3f) { }
    public ContinuousAcceleration(float duration, float acceleration) : base(duration)
    {
        this.acceleration = acceleration;
    }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        player.StartCoroutine(Push(player));
    }

    public override void RemoveEffect(Player player)
    {

    }

    private IEnumerator Push(Player player)
    {
        float deltaTime = Time.fixedDeltaTime;
        while (Time.time < expireTime)
        {
            player.movementScript.Accelerate(deltaTime, Vector3.forward * acceleration);
            yield return new WaitForFixedUpdate();
        }
    }
}
