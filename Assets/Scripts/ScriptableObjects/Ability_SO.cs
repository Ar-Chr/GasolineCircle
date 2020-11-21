using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityInfo", menuName = "Scriptable Objects/Ability Info")]
public class Ability_SO : ScriptableObject
{
    public new string name;
    public Sprite abilitySprite;
    [Space]
    public float cooldown;
}
