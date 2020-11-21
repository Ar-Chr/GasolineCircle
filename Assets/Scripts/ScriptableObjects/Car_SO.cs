using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] [CreateAssetMenu(fileName = "CarInfo", menuName = "Scriptable Objects/Car Info")]
public class Car_SO : ScriptableObject
{
    public new string name;
    public string decription;
    [Space]
    public Sprite sprite;
    public GameObject planePrefab;
    [Space]
    public CarSpecs_SO specs;
    public string abilityClassName;
    public Vector3 objectSpawnPosition;
    [Space]
    public float capsuleColliderRadius;
    public float capsuleColliderLength;

    private Ability ability;
    [HideInInspector]
    public Ability Ability
    {
        get
        {
            if (ability == null)
            {
                Type abilityType = Type.GetType(abilityClassName);
                ability = (Ability)abilityType.GetConstructor(new Type[0]).Invoke(new object[0]);
            }

            return ability;
        }
    }
        
}
