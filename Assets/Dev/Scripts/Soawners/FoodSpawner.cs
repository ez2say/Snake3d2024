using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FoodSpawner : MonoBehaviour
{
    [Header("Food Settings")]
    [SerializeField] private List<GameObject> _foodPrefabs;
    [SerializeField] private int _foodCount;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _minDistanceBetweenFood;

    [Header("Spawn Areas")]
    [SerializeField] private List<BoxCollider> _spawnAreas;

    [Header("Ground Settings")]
    [SerializeField] private float _groundHeight;

    private List<Vector3> _spawnedFoodPositions = new List<Vector3>();
    private int _currentFoodCount;
    private Dictionary<BoxCollider, Vector3> _previousPositions = new Dictionary<BoxCollider, Vector3>();

    public void Construct()
    {
        _currentFoodCount = 0;
        InitializePreviousPositions();
    }

    private void InitializePreviousPositions()
    {
        foreach (BoxCollider spawnArea in _spawnAreas)
        {
            _previousPositions[spawnArea] = spawnArea.transform.position;
        }
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood()
    {
        while (_currentFoodCount < _foodCount)
        {
            BoxCollider spawnArea = GetRandomSpawnArea();
            Vector3 spawnPosition = GetRandomPositionInArea(spawnArea);

            if (IsPositionValid(spawnPosition) && !IsSpawnAreaMoving(spawnArea))
            {
                InstantiateFood(spawnPosition, spawnArea);
            }

            yield return null;
        }
    }

    private BoxCollider GetRandomSpawnArea()
    {
        return _spawnAreas[Random.Range(0, _spawnAreas.Count)];
    }

    private Vector3 GetRandomPositionInArea(BoxCollider spawnArea)
    {
        Vector3 randomPosition = GetRandomPositionInBounds(spawnArea);

        randomPosition.y = GetGroundHeight(randomPosition);

        return randomPosition;
    }

    private Vector3 GetRandomPositionInBounds(BoxCollider spawnArea)
    {
        return new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            0.5f,
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
        );
    }

    private float GetGroundHeight(Vector3 position)
    {
        RaycastHit hit;

        if (Physics.Raycast(position, Vector3.down, out hit) && hit.collider.CompareTag("Ground"))
        {
            return hit.point.y;
        }

        return _groundHeight;
    }

    private bool IsPositionValid(Vector3 position)
    {
        return CheckDistanceBetweenFood(position) && CheckCollisionWithObstacles(position);
    }

    private bool CheckDistanceBetweenFood(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in _spawnedFoodPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < _minDistanceBetweenFood)
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckCollisionWithObstacles(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 2f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle") || collider.CompareTag("Bridge"))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsSpawnAreaMoving(BoxCollider spawnArea)
    {
        Vector3 currentPosition = spawnArea.transform.position;
        Vector3 previousPosition = _previousPositions[spawnArea];

        bool isMoving = currentPosition != previousPosition;

        // Обновляем предыдущую позицию
        _previousPositions[spawnArea] = currentPosition;

        return isMoving;
    }

    private void InstantiateFood(Vector3 spawnPosition, BoxCollider spawnArea)
    {
        int randomIndex = GetRandomFoodIndex();

        GameObject food = CreateFoodObject(randomIndex, spawnPosition);

        AddFoodPosition(spawnPosition);

        SubscribeToFoodEatenEvent(food);

        food.transform.SetParent(spawnArea.transform);
    }

    private int GetRandomFoodIndex()
    {
        return Random.Range(0, _foodPrefabs.Count);
    }

    private GameObject CreateFoodObject(int index, Vector3 position)
    {
        return Instantiate(_foodPrefabs[index], position, Quaternion.identity);
    }

    private void AddFoodPosition(Vector3 position)
    {
        _spawnedFoodPositions.Add(position);

        _currentFoodCount++;
    }

    private void SubscribeToFoodEatenEvent(GameObject food)
    {
        Food foodComponent = food.GetComponent<Food>();

        if (foodComponent != null)
        {
            foodComponent.OnFoodEaten += OnFoodEaten;
        }
    }

    private void OnFoodEaten()
    {
        _currentFoodCount--;

        SpawnFood();

        Debug.Log("+1 к еде на карте");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        foreach (BoxCollider spawnArea in _spawnAreas)
        {
            Gizmos.DrawWireCube(spawnArea.transform.position, new Vector3(spawnArea.size.x, 0, spawnArea.size.z));
        }
    }
}