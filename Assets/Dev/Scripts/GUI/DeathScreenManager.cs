using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    public static DeathScreenManager Instance { get; private set; }

    [Header("UI Elements")]

    [SerializeField] private GameObject _deathScreenPanel;

    [SerializeField] private TextMeshProUGUI _foodEatenText;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private GameObject _pauseButton;

    [Header("Game Data")]

    private int _foodEatenCount;

    [Header("Snake Controller")]

    [SerializeField] private SnakeController _snakeController;
    
    [SerializeField] private GameObject _view;

    public void Construct()
    {
        Instance = this;

        _pauseButton.SetActive(true);
    }

    public void Open()
    {
        _view.SetActive(true);
    }

    public void Close()
    {
        _view.SetActive(false);
    }

    public void ShowDeathScreen(int foodEaten)
    {
        UpdateFoodEatenCount(foodEaten);

        DisplayFoodEatenCount();

        ActivateDeathScreenPanel();

        PauseGame();

        AudioManager.Instance.SaveGameMusicTime();

        _pauseButton.SetActive(false);

        _foodEatenText.gameObject.SetActive(false);

        // Отображаем количество очков
        if (_scoreText != null)
        {
            _scoreText.text = "Счет: " + _foodEatenCount;
        }
    }

    private void UpdateFoodEatenCount(int foodEaten)
    {
        _foodEatenCount = foodEaten;
    }

    private void DisplayFoodEatenCount()
    {
        _foodEatenText.text = "Food Eaten: " + _foodEatenCount;
    }

    private void ActivateDeathScreenPanel()
    {
        _deathScreenPanel.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        UnPauseGame();

        SceneManager.LoadScene((int) IDScene.GAMEPLAY);
    }

    public void RestartGame()
    {
        UnPauseGame();

        SceneManager.LoadScene((int) IDScene.GAMEPLAY);

        _pauseButton.SetActive(true);
    }
}