using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float topSpeed;
    [SerializeField] private float brakesDrag;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float brakesRotationSpeed;

    [SerializeField] private ControlsSet_SO controls;

    private float airDrag;
    private Vector3 velocity;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;

        airDrag = acceleration / topSpeed;
    }

    private void FixedUpdate()
    {
        float drag = airDrag;
        float rotSpeed = rotationSpeed;

        //if (HoldingForward)
        //    Accelerate(Time.fixedDeltaTime, Vector3.forward * acceleration);
        //else
        //    drag = brakesDrag;

        //if (HoldingBackward)
        //    DropMine();

        if (HoldingForward)
            Accelerate(Time.fixedDeltaTime, Vector3.forward * acceleration);

        if (HoldingBackward)
        {
            drag = brakesDrag;
            rotSpeed = brakesRotationSpeed;
        }

        if (HoldingLeft)
            Rotate(Time.fixedDeltaTime, -rotSpeed);

        if (HoldingRight)
            Rotate(Time.fixedDeltaTime, rotSpeed);

        Move(Time.fixedDeltaTime, velocity);

        ApplyDrag(Time.fixedDeltaTime, drag, brakesDrag, velocity);
    }

    private bool Holding(KeyCode key) => Input.GetKey(key);
    private bool HoldingForward => Holding(controls.forwardButton);
    private bool HoldingLeft => Holding(controls.leftButton);
    private bool HoldingRight => Holding(controls.rightButton);
    private bool HoldingBackward => Holding(controls.backwardButton);

    private void Move(float time, Vector3 velocity)
    {
        gameObject.transform.Translate(velocity * time);
    }

    private void Accelerate(float time, Vector3 acceleration)
    {
        velocity += acceleration * time;
    }

    private void Rotate(float time, float rotationSpeed)
    {
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * time);
        //gameObject.transform.Rotate(Vector3.up, rotationSpeed * velocity * time);
    }

    private void ApplyDrag(float time, float forwardDrag, float sideDrag, Vector3 velocity)
    {
        Vector3 complexDrag = new Vector3();
        complexDrag.z = -velocity.z * forwardDrag;
        complexDrag.x = -velocity.x * sideDrag;
        //velocity -= velocity * drag * time;
        Accelerate(time, complexDrag);
    }

    private void DropMine() // Хочу вынести в отдельный класс
    {
        Debug.Log("Dropping mine!");
    }
}
