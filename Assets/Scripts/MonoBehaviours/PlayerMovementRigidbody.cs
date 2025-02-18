﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementRigidbody : MonoBehaviour
{
    private ControlsSet_SO controls;

    [HideInInspector] public float acceleration;
    [HideInInspector] public float topSpeed;
    [HideInInspector] public float reverseAcceleration;

    [HideInInspector] public float airDrag;
    [HideInInspector] public float rollingDrag;
    [HideInInspector] public float brakesDrag;
    [HideInInspector] public float sideDrag;
    [HideInInspector] public float brakesSideDrag;

    [HideInInspector] public float rotationSpeed;
    [HideInInspector] public float brakesRotationSpeed;

    private new Rigidbody rigidbody;

    [HideInInspector] public Player player;

    private float groundSpeedModifier = 0.6f;

    public float ForwardVelocity =>
        transform.InverseTransformDirection(rigidbody.velocity).z;

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

    public void SetControls(ControlsSet_SO controls) => this.controls = controls;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;
    }

    private void FixedUpdate()
    {
        float wheelDrag = rollingDrag;
        if (player != null && player.IsOnGround)
            wheelDrag = acceleration / (groundSpeedModifier * topSpeed) - airDrag * groundSpeedModifier * topSpeed;

        float sideDrag = this.sideDrag;
        float rotSpeed = rotationSpeed;

        Vector3 localSpaceVelocity = transform.InverseTransformDirection(rigidbody.velocity);
        float forwardVelocity = localSpaceVelocity.z;

        int rotation = (HoldingLeft ? -1 : 0) + (HoldingRight ? 1 : 0);

        if (HoldingForward)
            Accelerate(Time.fixedDeltaTime, Vector3.forward * acceleration);
        else if (HoldingBackward)
            Accelerate(Time.fixedDeltaTime, Vector3.back * reverseAcceleration);
        else
        {
            sideDrag = brakesSideDrag;
            rotSpeed = GetBrakesRotationSpeed(rotationSpeed, brakesRotationSpeed,
                                              rigidbody.velocity.magnitude, topSpeed);
            Accelerate(Time.fixedDeltaTime, Vector3.back * Mathf.Min(forwardVelocity, brakesDrag));
        }

        Rotate(Time.fixedDeltaTime, rotation * rotSpeed, forwardVelocity, topSpeed);

        ApplyComplexDrag(Time.fixedDeltaTime, localSpaceVelocity, airDrag, wheelDrag, sideDrag);
    }

    private bool Holding(KeyCode key) => Input.GetKey(key);
    private bool HoldingForward => Holding(controls.forwardButton);
    private bool HoldingLeft => Holding(controls.leftButton);
    private bool HoldingRight => Holding(controls.rightButton);
    private bool HoldingBackward => Holding(controls.backwardButton);

    public void Accelerate(float time, Vector3 acceleration)
    {
        rigidbody.AddRelativeForce(acceleration * time, ForceMode.VelocityChange);
    }

    private void Rotate(float time, float rotationSpeed, float velocity, float topSpeed)
    {
        rotationSpeed *= Mathf.Clamp(velocity / (topSpeed * 0.4f), -1, 1);

        gameObject.transform.Rotate(Vector3.up, rotationSpeed * time);
    }

    private void ApplyComplexDrag(float time, Vector3 velocity, float airDrag, float wheelDrag, float sideDrag)
    {
        Vector3 complexDrag = new Vector3();
        complexDrag.z = -velocity.z * wheelDrag - velocity.z * Math.Abs(velocity.z) * airDrag;
        complexDrag.x = -velocity.x * sideDrag;
        //complexDrag.x = -Mathf.Sign(velocity.x) * Mathf.Min(Mathf.Abs(velocity.x), sideDrag);
        Accelerate(time, complexDrag);
    }

    private float GetBrakesRotationSpeed(float normalRotSpeed, float brakesTopRotSpeed, float velocity, float topSpeed)
    {
        return Mathf.Lerp(normalRotSpeed, brakesTopRotSpeed, velocity / (topSpeed * 0.6f));
    }
}
