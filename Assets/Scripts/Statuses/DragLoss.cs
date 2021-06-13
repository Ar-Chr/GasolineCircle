using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Statuses/Drag Loss")]
public class DragLoss : Status
{
    public DragLoss (float duration) : base(duration) { }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        float airDrag = player.movementScript.airDrag;
        player.movementScript.brakesDrag = airDrag;
        player.movementScript.brakesSideDrag = airDrag;
        player.movementScript.sideDrag = airDrag;
    }

    public override void RemoveEffect(Player player)
    {
        CarSpecs_SO specs = player.car.specs;
        player.movementScript.brakesDrag = specs.brakesDrag;
        player.movementScript.brakesSideDrag = specs.brakesSideDrag;
        player.movementScript.sideDrag = specs.sideDrag;
    }
}
