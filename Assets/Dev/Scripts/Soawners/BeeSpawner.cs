using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _beePrefab; // Префаб пчелы
    [SerializeField] private Transform[] _spawnPoints; // Массив точек спауна

    private void Start()
    {
        if (_beePrefab == null)
        {
            Debug.LogError("Bee Prefab is not assigned!");
            return;
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        SpawnBee();
    }

    private void SpawnBee()
    {
        int randomIndex = Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[randomIndex];

        // Проверяем наличие объектов с тегами Obstacle и Food в месте спауна
        while (IsPositionOccupied(spawnPoint.position))
        {
            randomIndex = Random.Range(0, _spawnPoints.Length);
            spawnPoint = _spawnPoints[randomIndex];
        }

        // Перемещаем префаб пчелы на выбранную точку
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