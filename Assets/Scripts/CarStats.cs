using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarStats
{
    private float maxDurability;
    private float maxFuel;

    private float durability;
    private float fuel;
    private float fuelRate;

    private float repairDuration = 16;

    private Player player;

    public CarStats(float maxDurability, float maxFuel, float fuelRate, Player player)
    {
        durability = this.maxDurability = maxDurability;
        fuel = this.maxFuel = maxFuel;
        this.fuelRate = fuelRate;
        this.player = player;
    }

    public void BurnFuel(float time)
    {
        fuel -= fuelRate * time;
        fuel = fuel < 0 ? 0 : fuel;
        if (fuel < 0.05)
        {
            player.AddEffect(new AccelerationModification(repairDuration, 0));
            player.AddEffect(new Regeneration(repairDuration, 0, maxFuel / repairDuration));
        }
        FuelChanged();
    }

    public void Refuel(float refuelAmount)
    {
        fuel += refuelAmount;
        fuel = Mathf.Clamp(fuel, fuel, maxFuel);
        FuelChanged();
    }

    public void TakeDamage(float damage)
    {
        durability -= damage;
        durability = Mathf.Clamp(durability, 0, durability);
        if (durability < 0.05)
        {
            player.AddEffect(new AccelerationModification(repairDuration, 0));
            player.AddEffect(new Regeneration(repairDuration, maxDurability / repairDuration, 0));
        }
        DurabilityChanged();
    }

    public void Repair(float repairAmount)
    {
        durability += repairAmount;
        durability = Mathf.Clamp(durability, durability, maxDurability);
        DurabilityChanged();
    }

    private void FuelChanged() =>
        UIManager.Instance.OnFuelChanged.Invoke(player, fuel / maxFuel);

    private void DurabilityChanged() =>
        UIManager.Instance.OnDurabilityChanged.Invoke(player, durability / maxDurability);
}
