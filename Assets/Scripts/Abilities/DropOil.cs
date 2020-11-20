using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOil : Ability
{
    [SerializeField] private GameObject oil;

    protected override void Use(Player player)
    {
        Object.Instantiate(oil, player.objectsSpawnPosition, Quaternion.identity);
    }
}
