using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScrapPile : Ability
{
    [SerializeField] private GameObject scrapPile;

    protected override void Use(Player player)
    {
        Object.Instantiate(scrapPile, player.objectsSpawnPosition, Quaternion.identity);
    }
}
