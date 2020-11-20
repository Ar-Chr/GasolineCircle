using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    [SerializeField] protected float cooldown;

    protected float nextUse;

    public void TryUse(Player player)
    {
        if (Time.time >= nextUse)
        {
            Use(player);
            nextUse = Time.time + cooldown;
        }
    }

    protected abstract void Use(Player player);
}
