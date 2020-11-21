using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] [CreateAssetMenu(fileName = "CarInfo", menuName = "Scriptable Objects/Car Info")]
public class Car_SO : ScriptableObject
{
    public new string name;
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
}
