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

    private List<Status> statuses = new List<Status>();
    private CarStats carStats;

    public void Initialize(string name, Car_SO car)
    {
        this.name = name;
        this.car = car;
        Type abilityType = Type.GetType(car.abilityClassName);
        ability = (Ability)abilityType.GetConstructor(new Type[0] ).Invoke(new object[0]);

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
                                car.specs.fuelRate);

        movementScript.SetSpecs(car.specs);
        movementScript.SetControls(controls);
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(controls.forwardButton) || Input.GetKey(controls.backwardButton))
        foreach (var status in statuses)
        {
            if (Time.time >= status.expireTime)
            {
                statuses.Remove(status);
                status.RemoveEffect(this);
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
        if (relativeVelocity > 2.7)
        {
            carStats.TakeDamage(relativeVelocity * 0.3f);
            Debug.Log($"Relative velocity = {relativeVelocity: 0.0}\n {relativeVelocity * 0.3f: 0.0} damage taken");
        }
        // Take damage = collision.relativeVelocity * 0.3;
    }
}
