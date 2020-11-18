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

    private float acceleration;
    private float topSpeed;
    private float reverseAcceleration;

    private float airDrag;
    private float rollingDrag;
    private float brakesDrag;
    private float sideDrag;
    private float brakesSideDrag;

    private float rotationSpeed;
    private float brakesRotationSpeed;

    private new Rigidbody rigidbody;

    public void SetSpecs(CarSpecs_SO specs)
    {
        acceleration = specs.acceleration;
        topSpeed = specs.topSpeed;
        brakesDrag = specs.brakesDrag;
        sideDrag = specs.sideDrag;
        brakesSideDrag = specs.brakesSideDrag;
        rotationSpeed = specs.rotationSpeed;
        brakesRotationSpeed = specs.brakesRotationSpeed;

        airDrag = acceleration / (topSpeed * topSpeed + 30 * topSpeed);
        rollingDrag = 30 * airDrag;
        reverseAcceleration = airDrag * (specs.reverseTopSpeed * specs.reverseTopSpeed + 30 * specs.reverseTopSpeed);
    }

    private void Start()
    {
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
        float sideDrag = this.sideDrag;
        float rotSpeed = rotationSpeed;

        Vector3 localSpaceVelocity = transform.InverseTransformDirection(rigidbody.velocity);
        float forwardVelocity = localSpaceVelocity.z;

        if (HoldingForward)
            Accelerate(Time.fixedDeltaTime, Vector3.forward * acceleration);

        if (HoldingBackward)
        {
            if (forwardVelocity > 0.1)
            {
                sideDrag = brakesSideDrag;
                drag = brakesDrag;
                rotSpeed = GetBrakesRotationSpeed(rotationSpeed, brakesRotationSpeed,
                                                  rigidbody.velocity.magnitude, topSpeed);
            }
            else
            {
                Accelerate(Time.fixedDeltaTime, Vector3.back * reverseAcceleration);
            }
        }

        Rotate(Time.fixedDeltaTime, 
               (HoldingLeft ? -rotSpeed : 0) + 
               (HoldingRight ? rotSpeed : 0), 
               forwardVelocity, 
               topSpeed);

        ApplyComplexDrag(Time.fixedDeltaTime, localSpaceVelocity, drag, sideDrag);
    }

    private bool Holding(KeyCode key) => Input.GetKey(key);
    private bool HoldingForward => Holding(controls.forwardButton);
    private bool HoldingLeft => Holding(controls.leftButton);
    private bool HoldingRight => Holding(controls.rightButton);
    private bool HoldingBackward => Holding(controls.backwardButton);

    private void Accelerate(float time, Vector3 acceleration)
    {
        rigidbody.AddRelativeForce(acceleration * time, ForceMode.VelocityChange);
    }

    private void Rotate(float time, float rotationSpeed, float velocity, float topSpeed)
    {
        rotationSpeed *= Mathf.Clamp(velocity / (topSpeed * 0.4f), -1, 1);

        gameObject.transform.Rotate(Vector3.up, rotationSpeed * time);
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