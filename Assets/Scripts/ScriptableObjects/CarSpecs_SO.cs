using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSpecs", menuName = "Scriptable Objects/Car Specs")]
public class CarSpecs_SO : ScriptableObject
{
    public float acceleration;
    public float topSpeed;
    public float reverseTopSpeed;
    [Space]
    public float brakesDrag;
    public float sideDrag;
    public float brakesSideDrag;
    [Space]
    public float rotationSpeed;
    public float brakesRotationSpeed;
    [Space]
    [Space]
    public float durability;
    public float fuel;
    public float fuelRate;
}
