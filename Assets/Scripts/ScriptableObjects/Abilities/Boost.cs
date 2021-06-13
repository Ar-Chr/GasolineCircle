using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Boost")]
public class Boost : Ability
{
    [SerializeField] private ContinuousAcceleration acceleration;

    protected override void Use(Player player)
    {
        player.AddEffect(acceleration);
    }
}
