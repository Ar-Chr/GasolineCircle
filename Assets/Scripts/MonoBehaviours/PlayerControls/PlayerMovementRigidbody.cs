using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementRigidbody : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float topSpeed;
    [Space]
    [SerializeField] private float brakesDrag;
    [SerializeField] private float sideDrag;
    [Space]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float brakesRotationSpeed;

    [Space]
    [SerializeField] private ControlsSet_SO controls;

    private float airDrag;

    private new Rigidbody rigidbody;

    private void Start()
    {
        airDrag = acceleration / topSpeed;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;
    }

    private void FixedUpdate()
    {
        float drag = airDrag;
        float rotSpeed = rotationSpeed;

        //if (HoldingForward)
        //    Accelerate(Time.fixedDeltaTime, acceleration);
        //else
        //    drag = brakesDrag;

        //if (HoldingBackward)
        //    DropMine();

        if (HoldingForward)
            Accelerate(Time.fixedDeltaTime, Vector3.forward * acceleration);

        if (HoldingBackward)
        {
            drag = brakesDrag;
            rotSpeed = GetBrakesRotationSpeed(rotationSpeed, brakesRotationSpeed, 
                                              rigidbody.velocity.magnitude, topSpeed);
        }

        if (HoldingLeft)
            Rotate(Time.fixedDeltaTime, -rotSpeed);

        if (HoldingRight)
            Rotate(Time.fixedDeltaTime, rotSpeed);

        ApplyComplexDrag(Time.fixedDeltaTime, drag, sideDrag);
        //rigidbody.drag = drag;
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

    private void Rotate(float time, float rotationSpeed)
    {
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * time);
        //rigidbody.AddRelativeTorque(Vector3.up * rigidbody.velocity.magnitude * rotationSpeed * time, ForceMode.VelocityChange);
        //gameObject.transform.Rotate(Vector3.up, rotationSpeed * velocity * time);
    }

    private void ApplyComplexDrag(float time, float forwardDrag, float sideDrag)
    {
        var localSpaceVelocity = transform.InverseTransformDirection(rigidbody.velocity);
        Vector3 complexDrag = new Vector3();
        complexDrag.z = -localSpaceVelocity.z * forwardDrag;
        complexDrag.x = -localSpaceVelocity.x * sideDrag;
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
