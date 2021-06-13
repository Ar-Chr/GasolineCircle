using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;
    public Sprite abilitySprite;
    [Space]
    [SerializeField] private float cooldown;

    protected float nextUse;

    public float RemainingCooldown =>
        Mathf.Clamp((nextUse - Time.time) / cooldown, 0f, 1f);

    public void TryUse(Player player)
    {
        if (Time.time >= nextUse)
        {
            Use(player);
            nextUse = Time.time + cooldown;
        }
    }

    protected abstract void Use(Player player);
}
