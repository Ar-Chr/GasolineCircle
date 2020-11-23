using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] bonuses;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float retryCooldown;

    private float nextSpawn;
    
    private GameObject[] spawnedBonuses;

    private void OnEnable()
    {
        spawnedBonuses = new GameObject[points.Length];
        nextSpawn = Time.time + timeBetweenSpawns;
    }

    private void Update()
    {
        if (Time.time > nextSpawn)
            Spawn();
    }

    private void Spawn()
    {
        GameObject bonus = bonuses[Random.Range(0, bonuses.Length)];
        int pointNumber = Random.Range(0, points.Length);
        Vector3 spawnPoint = points[pointNumber].position;
        if (spawnedBonuses[pointNumber] == null)
        {
            nextSpawn = Time.time + timeBetweenSpawns;
            spawnedBonuses[pointNumber] =
                Instantiate(bonus, spawnPoint, Quaternion.LookRotation(Vector3.up), transform);
        }
        else
        {
            nextSpawn = Time.time + retryCooldown;
        }
    }
}
