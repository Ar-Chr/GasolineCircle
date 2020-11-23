using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public new string name;
    [Space]
    public PlayerMovementRigidbody movementScript;
    [SerializeField] private ControlsSet_SO controls;
    [SerializeField] private float damageThreshold;
    [Space]
    [SerializeField] private Sound driveSound;
    [SerializeField] private Sound bumpSound;
    [SerializeField] public Sound breakSound;

    [HideInInspector] public Car_SO car;

    private Ability ability;
    public Ability Ability
    {
        get
        {
            if (ability == null)
            {
                Type abilityType = Type.GetType(car.abilityClassName);
                ability = (Ability)abilityType.GetConstructor(new Type[0]).Invoke(new object[0]);
            }

            return ability;
        }
    }

    private List<Status> statuses;

    [HideInInspector] public CarStats carStats;

    private int groundCollidersTouching;
    public bool IsOnGround => groundCollidersTouching > 0;

    public void Initialize(string name, Car_SO car)
    {
        this.name = name;
        this.car = car;

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

        movementScript.player = this;
        movementScript.SetSpecs(car.specs);
        movementScript.SetControls(controls);

        groundCollidersTouching = 0;

        AudioManager.Instance.Play(driveSound);
    }

    private void Start()
    {
        statuses = new List<Status>();
        GameManager.Instance.OnPlayerWon.AddListener(HandlePlayerWon);
    }

    private void FixedUpdate()
    {
        HandlePlayerMoving();
        RemoveExpiredStatuses();
    }

    private void HandlePlayerMoving()
    {
        if ((Input.GetKey(controls.forwardButton) || Input.GetKey(controls.backwardButton)) && movementScript.acceleration > 0.1f)
        {
            carStats.BurnFuel(Time.fixedDeltaTime * (IsOnGround ? 2 : 1));
            if (IsOnGround)
                carStats.TakeDamage(4f * Time.deltaTime);

            driveSound.source.volume += 0.6f * Time.fixedDeltaTime;
            driveSound.source.pitch += 0.4f * Time.fixedDeltaTime;
        }
        else
        {
            driveSound.source.volume -= 0.6f * Time.fixedDeltaTime;
            driveSound.source.pitch -= 0.4f * Time.fixedDeltaTime;
        }
        driveSound.source.volume = Mathf.Clamp(driveSound.source.volume, 0.2f, 0.8f);
        float maxPitch = 1f + Mathf.Abs(movementScript.ForwardVelocity) / movementScript.topSpeed;
        driveSound.source.pitch = Mathf.Clamp(driveSound.source.pitch, 1f, maxPitch);
    }

    private void HandlePlayerWon(Player arg0)
    {
        movementScript.acceleration = 0;
        movementScript.reverseAcceleration = 0;
    }

    private void RemoveExpiredStatuses()
    {
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
            Ability.TryUse(this);
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
            AudioManager.Instance.Play(bumpSound);
            bumpSound.source.pitch = UnityEngine.Random.Range(0.5f, 1.4f);
            bumpSound.source.volume = Mathf.Lerp(0.4f, 0.8f, (relativeVelocity - damageThreshold) / 16f);

            carStats.TakeDamage(relativeVelocity * 0.8f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
            groundCollidersTouching++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
            groundCollidersTouching--;
    }
}
