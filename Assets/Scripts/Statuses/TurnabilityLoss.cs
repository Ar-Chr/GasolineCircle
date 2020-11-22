using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnabilityLoss : Status
{
    private float damage;

    public TurnabilityLoss() : this(2f, 10f) { }
    public TurnabilityLoss(float duration, float damage) : base(duration) 
    {
        this.damage = damage;
    }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        player.movementScript.rotationSpeed = 0;
        player.movementScript.brakesRotationSpeed = 0;
        player.carStats.TakeDamage(damage);
    }

    public override void RemoveEffect(Player player)
    {
        CarSpecs_SO specs = player.car.specs;
        player.movementScript.rotationSpeed = specs.rotationSpeed;
        player.movementScript.brakesRotationSpeed = specs.brakesRotationSpeed;
    }
}
