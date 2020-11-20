using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpikes : Ability
{
    [SerializeField] private GameObject spikes;

    protected override void Use(Player player)
    {
        Object.Instantiate(spikes, player.objectsSpawnPosition, Quaternion.identity);
    }
}
