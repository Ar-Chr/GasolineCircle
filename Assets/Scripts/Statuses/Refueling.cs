﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refueling : Status
{
    private float fuelPerSecond;

    public Refueling() : this(-1, 15 / Time.fixedDeltaTime) { }
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
