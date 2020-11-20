using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementRigidbody : MonoBehaviour
{
    private CarSpecs_SO specs;
    private ControlsSet_SO controls;

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

    private void Accelerate(float time, Vector3 acceleration)
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
