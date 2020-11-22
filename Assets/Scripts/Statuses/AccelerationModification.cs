using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationModification : Status
{
    private float accelerationModifier;

    public AccelerationModification() : this(4f, 1.5f) { }
    public AccelerationModification(float duration, float accelerationModifier) : base(duration)
    {
        this.accelerationModifier = accelerationModifier;
    }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        player.movementScript.acceleration *= accelerationModifier;
        player.movementScript.reverseAcceleration *= accelerationModifier;
    }

    public override void RemoveEffect(Player player)
    {
        CarSpecs_SO specs = player.car.specs;

        player.movementScript.reverseAcceleration = 
            player.movementScript.airDrag * (specs.reverseTopSpeed * specs.reverseTopSpeed + 30 * specs.reverseTopSpeed);

        player.movementScript.acceleration = specs.acceleration;
    }
}
