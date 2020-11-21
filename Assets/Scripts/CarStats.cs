using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarStats
{
    private float maxDurability;
    private float maxFuel;

    [SerializeField] private float durability;
    [SerializeField] private float fuel;
    [SerializeField] private float fuelRate;

    public CarStats(float maxDurability, float maxFuel, float fuelRate)
    {
        durability = this.maxDurability = maxDurability;
        fuel = this.maxFuel = maxFuel;
        this.fuelRate = fuelRate;
    }

    public void BurnFuel(float time)
    {
        fuel -= fuelRate * time;
        fuel = fuel < 0 ? 0 : fuel;
        if (fuel < 0.05)
        {

            ;
        }
    }

    public void TakeDamage(float damage)
    {
        durability -= damage;
        durability = Mathf.Clamp(durability, 0, durability);
        if (durability < 0.05)
        {

            ;
        }
    }

    public void Repair(float repairAmount)
    {
        durability += repairAmount;
        durability = Mathf.Clamp(durability, durability, maxDurability);
    }

    public void Refuel(float refuelAmount)
    {
        fuel += refuelAmount;
        fuel = Mathf.Clamp(fuel, fuel, maxFuel);
    }
}
