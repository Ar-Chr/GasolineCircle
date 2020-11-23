using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] bonuses;
    [SerializeField] private float timeBetweenSpawns;

    private float nextSpawn;

    private void OnEnable()
    {
        nextSpawn = Time.time + timeBetweenSpawns;
    }

    private void Update()
    {
        if (Time.time > nextSpawn)
            Spawn();
    }

    private void Spawn()
    {
        nextSpawn = Time.time + timeBetweenSpawns;
        GameObject bonus = bonuses[Random.Range(0, bonuses.Length)];
        Vector3 spawnPoint = points[Random.Range(0, points.Length)].position;
        Instantiate(bonus, spawnPoint, Quaternion.LookRotation(Vector3.up));
    }
}
