using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    public float expireTime;
    protected float duration;

    public Status(float duration)
    {
        this.duration = duration;
    }

    public abstract void ApplyEffect(Player player);

    public abstract void RemoveEffect(Player player);
}
