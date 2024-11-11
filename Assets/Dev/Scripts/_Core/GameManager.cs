using Root.GUI;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FoodSpawner _foodSpawner;

    [SerializeField] private ObstacleSpawner _obstacleSpawner;

    [SerializeField] private InputManager _inputManager;

    [SerializeField] private BeeSpawner _beeSpawner;

    [SerializeField] private SnakeController _snakeController;

    [SerializeField] private GUIInspector _inspectorGUI;

    [SerializeField] private IslandController _islandController;

    [DllImport("__Internal")]
    private static extern void GameReadyExtern();

    private void Awake()
    {
        InitComponentSystem();

        PreWork();

#if UNITY_WEBGL
        InitYandexAnalytics();
#endif

    }

    private static void InitYandexAnalytics()
    {
        GameReadyExtern();
    }

    private void InitComponentSystem()
    {
        _foodSpawner.Construct();

        _obstacleSpawner.Construct();

        _inspectorGUI.Construct();

        _snakeController.Construct(_inputManager);

        _islandController.Construct();
    }

    public void StartGameplay()
    {
        _foodSpawner.StartSpawn();

        _obstacleSpawner.StartSpawn();

        _inspectorGUI.OpenGameplayMenu();

        _islandController.StartEarthquake();

        _inputManager.IsActiveControl = true;
    }

    private void PreWork()
    {
        _inspectorGUI.OpenMainMenu();

        _inputManager.IsActiveControl = false;
    }
}