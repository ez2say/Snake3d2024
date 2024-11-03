using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _beePrefab; 
    [SerializeField] private Transform[] _spawnPoints; 

    private void Start()
    {
        SpawnBee();
    }

    private void SpawnBee()
    {
        int randomIndex = Random.Range(0, _spawnPoints.Length);

        Transform spawnPoint = _spawnPoints[randomIndex];

        while (IsPositionOccupied(spawnPoint.position))
        {
            randomIndex = Random.Range(0, _spawnPoints.Length);

            spawnPoint = _spawnPoints[randomIndex];
        }

        _beePrefab.transform.position = spawnPoint.position;

        _beePrefab.transform.rotation = spawnPoint.rotation;
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle") || collider.CompareTag("Food"))
            {
                return true;
            }
        }

        return false;
    }
}