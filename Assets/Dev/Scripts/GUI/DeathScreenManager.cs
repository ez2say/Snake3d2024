using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class DeathScreenManager : MonoBehaviour
{
    public static DeathScreenManager Instance { get; private set; }

    [Header("UI Elements")]

    [SerializeField] private GameObject _deathScreenPanel;

    [SerializeField] private TextMeshProUGUI _foodEatenText;

    [SerializeField] private TextMeshProUGUI _leaderboardText;

    [SerializeField] private TMP_InputField _nicknameInputField;

    [SerializeField] private GameObject _nicknameInputPanel;

    [Header("Game Data")]

    private int _foodEatenCount;

    private const string LeaderboardKey = "Leaderboard";

    [Header("Snake Controller")]

    [SerializeField] private SnakeController _snakeController;


    private void Start()
    {
        Instance = this;

        _nicknameInputPanel.SetActive(true);
    }

    public void ShowDeathScreen(int foodEaten)
    {
        UpdateFoodEatenCount(foodEaten);

        DisplayFoodEatenCount();

        ActivateDeathScreenPanel();

        PauseGame();

        ShowLeaderboard();
        
        ShowNicknameInput();
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

    private void ShowLeaderboard()
    {
        string leaderboardData = LoadLeaderboardData();

        UpdateLeaderboardText(leaderboardData);
    }

    private string LoadLeaderboardData()
    {
        return PlayerPrefs.GetString(LeaderboardKey, "");
    }

    private void UpdateLeaderboardText(string leaderboardData)
    {
        _leaderboardText.text = "Leaderboard:\n" + leaderboardData;
    }

    private void ShowNicknameInput()
    {
        _nicknameInputPanel.SetActive(true);
    }

    public void SubmitNickname()
    {
        string nickname = GetNicknameFromInputField();
        if (IsValidNickname(nickname))
        {
            SaveScore(nickname, _foodEatenCount);

            HideNicknameInputPanel();

            ShowLeaderboard();
        }
    }
    
    private string GetNicknameFromInputField()
    {
        return _nicknameInputField.text;
    }

    private bool IsValidNickname(string nickname)
    {
        return !string.IsNullOrEmpty(nickname);
    }

    private void HideNicknameInputPanel()
    {
        _nicknameInputPanel.SetActive(false);
    }

    private void SaveScore(string nickname, int score)
    {
        string leaderboardData = LoadLeaderboardData();

        string updatedLeaderboardData = AppendScoreToLeaderboard(leaderboardData, nickname, score);

        SaveLeaderboardData(updatedLeaderboardData);

        CommitPlayerPrefs();
    }

    private string AppendScoreToLeaderboard(string leaderboardData, string nickname, int score)
    {
        return leaderboardData + nickname + ": " + score + "\n";
    }

    private void SaveLeaderboardData(string leaderboardData)
    {
        PlayerPrefs.SetString(LeaderboardKey, leaderboardData);
    }

    private void CommitPlayerPrefs()
    {
        PlayerPrefs.Save();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        UnPauseGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}