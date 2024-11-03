using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{

    [Header("Spawn Settings")]

    [SerializeField] private GameObject _snake;

    [SerializeField] private List<GameObject> _obstaclePrefabs;

    [SerializeField] private List<BoxCollider> _spawnAreas;

    [SerializeField] private int _obstacleCount;

    [SerializeField] private float _minDistanceBetweenObstacles;

    [SerializeField] private float _minDistanceFromSnake;

    [Header("Ground Settings")]

    [SerializeField] private float _groundHeight;

    private List<Vector3> _spawnedObstaclePositions = new List<Vector3>();

    public void Construct()
    {
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        for (int i = 0; i < _obstacleCount; i++)
        {
            BoxCollider spawnArea = GetRandomSpawnArea();

            Vector3 spawnPosition = GetRandomPositionInArea(spawnArea);

            if (IsPositionValid(spawnPosition))
            {
                InstantiateObstacle(spawnPosition);
            }

            yield return null;
        }
    }

    private BoxCollider GetRandomSpawnArea()
    {
        return _spawnAreas[Random.Range(0, _spawnAreas.Count)];
    }

    private Vector3 GetRandomPositionInArea(BoxCollider area)
    {
        Vector3 extents = area.size / 2f;

        Vector3 point = new Vector3(
            Random.Range(-extents.x, extents.x),
            1000f,
            Random.Range(-extents.z, extents.z)
        );

        point = area.transform.TransformPoint(point);

        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                point.y = hit.point.y;
            }
            else
            {
                point.y = _groundHeight;
            }
        }
        else
        {
            point.y = _groundHeight;
        }

        return point;
    }

    private bool IsPositionValid(Vector3 position)
    {
        if (Vector3.Distance(position, _snake.transform.position) < _minDistanceFromSnake)
        {
            return false;
        }

        foreach (Vector3 spawnedPosition in _spawnedObstaclePositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < _minDistanceBetweenObstacles)
            {
                return false;
            }
        }

        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Bridge") || collider.CompareTag("Food"))
            {
                return false;
            }
        }

        return true;
    }

    private void InstantiateObstacle(Vector3 spawnPosition)
    {
        int randomIndex = GetRandomObstacleIndex();

        GameObject obstacle = CreateObstacleObject(randomIndex, spawnPosition);

        AddObstaclePosition(spawnPosition);
    }

    private int GetRandomObstacleIndex()
    {
        return Random.Range(0, _obstaclePrefabs.Count);
    }

    private GameObject CreateObstacleObject(int index, Vector3 position)
    {
        return Instantiate(_obstaclePrefabs[index], position, Quaternion.identity);
    }

    private void AddObstaclePosition(Vector3 position)
    {
        _spawnedObstaclePositions.Add(position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        foreach (BoxCollider area in _spawnAreas)
        {
            if (area != null)
            {
                Gizmos.DrawWireCube(area.transform.position, area.size);
            }
        }
    }
}