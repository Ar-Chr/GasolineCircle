using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Spawn Obstacle")]
public class ObstacleSpawnAbility : Ability
{
    [SerializeField] protected GameObject obstacle;

    protected override void Use(Player player)
    {
        Instantiate(
            obstacle,
            player.transform.position + player.transform.TransformDirection(player.car.objectSpawnPosition),
            Quaternion.LookRotation(player.transform.TransformDirection(Vector3.forward), Vector3.up));
    }
}
