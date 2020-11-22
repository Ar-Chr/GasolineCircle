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
        player.movementScript.reverseAcceleration *= accelerationModifier;
    }

    public override void RemoveEffect(Player player)
    {
        CarSpecs_SO specs = player.car.specs;

        float airDrag = specs.acceleration / (specs.topSpeed * specs.topSpeed + 30 * specs.topSpeed);
        float rollingDrag = 30 * airDrag;
        player.movementScript.reverseAcceleration = airDrag * (specs.reverseTopSpeed * specs.reverseTopSpeed + 30 * specs.reverseTopSpeed);

        player.movementScript.acceleration = specs.acceleration;
    }
}
