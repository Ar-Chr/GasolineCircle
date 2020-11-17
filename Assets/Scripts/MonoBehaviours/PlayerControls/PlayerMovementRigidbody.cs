﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementRigidbody : MonoBehaviour
{
    [SerializeField] private CarSpecs_SO specs;
    [Space]
    [SerializeField] private ControlsSet_SO controls;

    private float airDrag;
    private float reverseAcceleration;

    private new Rigidbody rigidbody;

    private void Start()
    {
        airDrag = specs.acceleration / specs.topSpeed;
        reverseAcceleration = airDrag * specs.reverseTopSpeed;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(controls.abilityButton))
            DropMine();
    }

    private void FixedUpdate()
    {
        float drag = airDrag;
        float sideDrag = specs.sideDrag;
        float rotSpeed = specs.rotationSpeed;

        if (HoldingForward)
            Accelerate(Time.fixedDeltaTime, Vector3.forward * specs.acceleration);

        if (HoldingBackward)
            Accelerate(Time.fixedDeltaTime, Vector3.back * reverseAcceleration);

        if (HoldingBrakes)
        {
            sideDrag = specs.brakesSideDrag;
            drag = specs.brakesDrag;
            rotSpeed = GetBrakesRotationSpeed(specs.rotationSpeed, specs.brakesRotationSpeed, 
                                              rigidbody.velocity.magnitude, specs.topSpeed);
        }

        Vector3 localSpaceVelocity = transform.InverseTransformDirection(rigidbody.velocity);
        float forwardVelocity = localSpaceVelocity.z;

        if (HoldingLeft)
            Rotate(Time.fixedDeltaTime, forwardVelocity, -rotSpeed);

        if (HoldingRight)
            Rotate(Time.fixedDeltaTime, forwardVelocity, rotSpeed);

        ApplyComplexDrag(Time.fixedDeltaTime, localSpaceVelocity, drag, sideDrag);
    }

    private bool Holding(KeyCode key) => Input.GetKey(key);
    private bool HoldingForward => Holding(controls.forwardButton);
    private bool HoldingLeft => Holding(controls.leftButton);
    private bool HoldingRight => Holding(controls.rightButton);
    private bool HoldingBackward => Holding(controls.backwardButton);
    private bool HoldingBrakes => Holding(controls.brakesButton);

    private void Accelerate(float time, Vector3 acceleration)
    {
        rigidbody.AddRelativeForce(acceleration * time, ForceMode.VelocityChange);
    }

    private void Rotate(float time, float rotationSpeed, float velocity)
    {
        if (-1 < velocity && velocity < 1)
            velocity *= Mathf.Sin(velocity * Mathf.PI / 2);

        gameObject.transform.Rotate(Vector3.up, rotationSpeed * velocity * time);
    }

    private void ApplyComplexDrag(float time, Vector3 velocity, float forwardDrag, float sideDrag)
    {
        Vector3 complexDrag = new Vector3();
        complexDrag.z = -velocity.z * forwardDrag;
        complexDrag.x = -velocity.x * sideDrag;
        Accelerate(time, complexDrag);
    }

    private float GetBrakesRotationSpeed(float normalRotSpeed, float brakesTopRotSpeed, float velocity, float topSpeed)
    {
        return Mathf.Lerp(normalRotSpeed, brakesTopRotSpeed, velocity / topSpeed);
    }

    private void DropMine() // Хочу вынести в отдельный класс
    {
        Debug.Log("Dropping mine!");
    }
}
