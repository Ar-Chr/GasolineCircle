using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleSpawnAbility : Ability
{
    protected abstract string ObstacleName { get; }

    protected GameObject obstacle;
    protected override void Use(Player player)
    {
        if (obstacle == null)
            obstacle = (GameObject)Resources.Load("Prefabs/Obstacles/" + ObstacleName);

        Object.Instantiate(
            obstacle,
            player.transform.position + player.transform.TransformDirection(player.car.objectSpawnPosition),
            Quaternion.LookRotation(player.transform.TransformDirection(Vector3.forward), Vector3.up));
    }
}
