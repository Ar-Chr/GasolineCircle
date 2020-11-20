using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats
{
    public float durability;
    public float fuel;
    public float fuelRate;

    public CarStats(float durability, float fuel, float fuelRate)
    {
        this.durability = durability;
        this.fuel = fuel;
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
        durability = durability < 0 ? 0 : durability;
        if (durability < 0.05)
        {

            ;
        }
    }
}
