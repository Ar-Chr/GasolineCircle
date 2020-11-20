using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    public new string name;

    [SerializeField] private PlayerMovementRigidbody movementScript;
    [SerializeField] private ControlsSet_SO controls;

    public void Initialize(string name, Car_SO car)
    {
        this.name = name;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Destroy(child);
        }
        Instantiate(car.planePrefab, transform);

        var capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = car.capsuleColliderRadius;
        capsuleCollider.height = car.capsuleColliderLength;

        movementScript.SetSpecs(car.specs);
        movementScript.SetControls(controls);
    }
}
