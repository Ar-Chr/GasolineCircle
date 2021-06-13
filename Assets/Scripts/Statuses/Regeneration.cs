using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Statuses/Regeneration")]
public class Regeneration : Status
{
    [SerializeField] private float durabilityPerSecond;

    public Regeneration(float duration, float durabilityPerSecond) : base(duration)
    {
        this.durabilityPerSecond = durabilityPerSecond;
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
        do
        {
            player.carStats.Repair(durabilityPerSecond * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        } while (Time.time < expireTime);
    }
}
