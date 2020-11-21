using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Status
{
    private float durabilityPerSecond;
    private float fuelPerSecond;

    public Regeneration(float duration, float durabilityPerSecond, float fuelPerSecond) : base(duration)
    {
        this.durabilityPerSecond = durabilityPerSecond;
        this.fuelPerSecond = fuelPerSecond;
    }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        player.StartCoroutine(Regenerate(player));
    }

    public override void RemoveEffect(Player player)
    {

    }

    private IEnumerator Regenerate(Player player)
    {
        float deltaTime = Time.fixedDeltaTime;
        while(Time.time < expireTime)
        {
            player.carStats.Repair(durabilityPerSecond * deltaTime);
            player.carStats.Refuel(fuelPerSecond * deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
