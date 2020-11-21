using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    protected Ability_SO abilityInfo;
    public Ability_SO AbilityInfo
    {
        get
        {
            if (abilityInfo == null)
                abilityInfo = (Ability_SO)Resources.Load("ScriptableObjects/Abilities/" + AbilityInfoName);

            return abilityInfo;
        }
    }
    protected abstract string AbilityInfoName { get; }

    protected float nextUse;

    public void TryUse(Player player)
    {
        if (Time.time >= nextUse)
        {
            Use(player);
            nextUse = Time.time + AbilityInfo.cooldown;
        }
    }

    protected abstract void Use(Player player);
}
