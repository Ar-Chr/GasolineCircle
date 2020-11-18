using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    public new string name;

    private PlayerMovementRigidbody movementScript;
    private ControlsSet_SO controls;
    public Car car;


}
