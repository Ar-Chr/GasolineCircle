using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    public new string name;
    [Space]
    [SerializeField] private Ability ability;
    [Space]
    public PlayerMovementRigidbody movementScript;
    [SerializeField] private ControlsSet_SO controls;
    [HideInInspector] public Car_SO car;
    [SerializeField] private float damageThreshold;

    private List<Status> statuses;
    public CarStats carStats;

    public void Initialize(string name, Car_SO car)
    {
        this.name = name;
        this.car = car;
        ability = car.Ability;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Destroy(child);
        }
        Instantiate(car.planePrefab, transform);

        var capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = car.capsuleColliderRadius;
        capsuleCollider.height = car.capsuleColliderLength;

        carStats = new CarStats(car.specs.durability,
                                car.specs.fuel,
                                car.specs.fuelRate,
                                this);

        movementScript.SetSpecs(car.specs);
        movementScript.SetControls(controls);
    }

    private void Start()
    {
        statuses = new List<Status>();
    }

    private void FixedUpdate()
    {
        if ((Input.GetKey(controls.forwardButton) || Input.GetKey(controls.backwardButton)) && movementScript.acceleration > 0.1f)
        {
            carStats.BurnFuel(Time.fixedDeltaTime);
        }
        for (int i = 0; i < statuses.Count; i++)
        {
            Status status = statuses[i];
            if (Time.time >= status.expireTime)
            {
                statuses.RemoveAt(i);
                status.RemoveEffect(this);
                i--;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(controls.abilityButton))
            ability.TryUse(this);
    }

    public void AddEffect(Status status)
    {
        var existingStatus = statuses.Find(s => s == status);
        if (existingStatus != null)
        {
            existingStatus.expireTime = Mathf.Max(existingStatus.expireTime, status.expireTime);
            return;
        }
        statuses.Add(status);
        status.ApplyEffect(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        float relativeVelocity = collision.relativeVelocity.magnitude;
        if (relativeVelocity > damageThreshold)
        {
            carStats.TakeDamage(relativeVelocity * 0.8f);
        }
    }
}
