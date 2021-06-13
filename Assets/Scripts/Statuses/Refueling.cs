using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Statuses/Refueling")]
public class Refueling : Status
{
    [SerializeField] private float fuelPerSecond;

    public Refueling(float duration, float fuelPerSecond) : base(duration)
    {
        this.fuelPerSecond = fuelPerSecond;
    }

    public override void ApplyEffect(Player player)
    {
        expireTime = Time.time + duration;
        player.StartCoroutine(Refuel(player));
    }

    public override void RemoveEffect(Player player)
    {

    }

    private IEnumerator Refuel(Player player)
    {
        do
        {
            player.carStats.Refuel(fuelPerSecond * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        } while (Time.time < expireTime);
    }
}
