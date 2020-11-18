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

    public void Initialize(string name, Car_SO car)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Destroy(child);
        }

        this.name = name;
        movementScript.SetSpecs(car.specs);
        Instantiate(car.planePrefab, transform);
        var capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = car.capsuleColliderRadius;
        capsuleCollider.height = car.capsuleColliderLength;
        capsuleCollider.center = new Vector3(0, 0, car.capsuleColliderZOffset);
    }
}
