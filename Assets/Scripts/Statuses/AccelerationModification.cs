using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationModification : Status
{
    private float accelerationModifier;

    public AccelerationModification(float duration, float accelerationModifier) : base(duration)
    {
        this.accelerationModifier = accelerationModifier;
    }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        player.movementScript.acceleration *= accelerationModifier;
    }

    public override void RemoveEffect(Player player)
    {
        player.movementScript.acceleration = player.car.specs.acceleration;
    }
}
