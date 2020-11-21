using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    public Sprite abilitySprite;
    [Space]
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
