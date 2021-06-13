using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Regenerate")]
public class Regenerate : Ability
{
    [Space]
    [SerializeField] private Regeneration regenerationStatus;
    [SerializeField] private Refueling refuelingStatus;

    protected override void Use(Player player)
    {
        player.AddEffect(regenerationStatus);
        player.AddEffect(refuelingStatus);
    }
}
