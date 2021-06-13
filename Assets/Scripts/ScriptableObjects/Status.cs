using UnityEngine;

public abstract class Status : ScriptableObject
{
    [SerializeField] protected float duration;

    [HideInInspector] public float expireTime;

    public Status(float duration)
    {
        this.duration = duration;
    }

    public abstract void ApplyEffect(Player player);

    public abstract void RemoveEffect(Player player);
}
