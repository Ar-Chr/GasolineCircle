using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] [CreateAssetMenu(fileName = "CarInfo", menuName = "Scriptable Objects/Car Info")]
public class Car_SO : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public CarSpecs_SO specs;

    public GameObject planePrefab;

    public float capsuleColliderRadius;
    public float capsuleColliderLength;
}
