using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Ability
{
    protected override string AbilityInfoName => "BoostInfo";

    protected override void Use(Player player)
    {
        player.AddEffect(new ContinuousAcceleration(10, 4));
    }
}
