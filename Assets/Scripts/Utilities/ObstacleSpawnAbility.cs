using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleSpawnAbility : Ability
{
    protected abstract string ObstacleName { get; }

    protected override void Use(Player player)
    {
        GameObject scrapPile = (GameObject)Resources.Load("Prefabs/Obstacles/" + ObstacleName);
        Object.Instantiate(scrapPile, player.transform.position + player.car.objectSpawnPosition, Quaternion.identity);
    }
}
