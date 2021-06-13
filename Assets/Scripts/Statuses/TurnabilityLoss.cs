using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Statuses/Turnability Loss")]
public class TurnabilityLoss : Status
{
    public TurnabilityLoss(float duration) : base(duration) { }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        player.movementScript.rotationSpeed = 0;
        player.movementScript.brakesRotationSpeed = 0;
    }

    public override void RemoveEffect(Player player)
    {
        CarSpecs_SO specs = player.car.specs;
        player.movementScript.rotationSpeed = specs.rotationSpeed;
        player.movementScript.brakesRotationSpeed = specs.brakesRotationSpeed;
    }
}
