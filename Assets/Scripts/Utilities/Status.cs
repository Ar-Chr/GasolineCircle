using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    public float expireTime;

    public abstract void ApplyEffect(Player player);

    public abstract void RemoveEffect(Player player);
}
