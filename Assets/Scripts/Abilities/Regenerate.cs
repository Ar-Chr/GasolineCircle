using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : Ability
{
    protected override string AbilityInfoName => "RegenerateInfo";

    private Regeneration regeneration = new Regeneration(10, 2);
    private Refueling refueling = new Refueling(10, 2);

    protected override void Use(Player player)
    {
        player.AddEffect(regeneration);
        player.AddEffect(refueling);
    }
}
