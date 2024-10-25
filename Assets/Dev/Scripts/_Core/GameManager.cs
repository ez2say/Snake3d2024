using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FoodSpawner _foodSpawner;

    [SerializeField] private ObstacleSpawner _obstacleSpawner;

    [SerializeField] private InputManager _inputManager;


    private void Awake()
    {
        InitComponentSystem();

        StartGameplay();
    }

    private void InitComponentSystem()
    {
        _foodSpawner.Construct();

        _obstacleSpawner.Construct();
    }

    private void StartGameplay()
    {
        _foodSpawner.StartSpawn();

        _obstacleSpawner.StartSpawn();
    }
}