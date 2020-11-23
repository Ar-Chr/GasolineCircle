using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Status
{
    private float durabilityPerSecond;

    public Regeneration() : this(0, 30 / Time.fixedDeltaTime) { }
    public Regeneration(float duration, float durabilityPerSecond) : base(duration)
    {
        this.durabilityPerSecond = durabilityPerSecond;
    }

    public override void ApplyEffect(Player player)
    {
        Debug.Log("[Regeneration] ApplyEffect");
        expireTime = Time.time + duration;
        player.StartCoroutine(Regenerate(player));
    }

    public override void RemoveEffect(Player player)
    {

    }

    private IEnumerator Regenerate(Player player)
    {
        do
        {
            Debug.Log("[Regeneration] Repair called");
            player.carStats.Repair(durabilityPerSecond * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        } while (Time.time < expireTime);
    }
}
