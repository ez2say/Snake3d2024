using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

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

    private const int MaxLeaderboardEntries = 15;

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

        // Сохраняем текущую позицию аудиодорожки
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveGameMusicTime();
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

    private void ShowLeaderboard()
    {
        List<LeaderboardEntry> leaderboardEntries = LoadLeaderboardData();

        UpdateLeaderboardText(leaderboardEntries);
    }

    private List<LeaderboardEntry> LoadLeaderboardData()
    {
        string leaderboardData = PlayerPrefs.GetString(LeaderboardKey, "");

        List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

        if (!string.IsNullOrEmpty(leaderboardData))
        {
            string[] lines = leaderboardData.Split('\n');
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        entries.Add(new LeaderboardEntry(parts[0], score));
                    }
                }
            }
        }

        return entries;
    }

    private void UpdateLeaderboardText(List<LeaderboardEntry> leaderboardEntries)
    {
        string leaderboardText = "Leaderboard:\n";
        foreach (var entry in leaderboardEntries)
        {
            leaderboardText += $"{entry.Nickname}: {entry.Score}\n";
        }
        _leaderboardText.text = leaderboardText;
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
            List<LeaderboardEntry> leaderboardEntries = LoadLeaderboardData();

            leaderboardEntries.Add(new LeaderboardEntry(nickname, _foodEatenCount));

            leaderboardEntries = leaderboardEntries.OrderByDescending(e => e.Score).Take(MaxLeaderboardEntries).ToList();
            
            SaveLeaderboardData(leaderboardEntries);

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

    private void SaveLeaderboardData(List<LeaderboardEntry> leaderboardEntries)
    {
        string leaderboardData = string.Join("\n", leaderboardEntries.Select(e => $"{e.Nickname}:{e.Score}"));

        PlayerPrefs.SetString(LeaderboardKey, leaderboardData);

        PlayerPrefs.Save();
    }

    public void ReturnToMenu()
    {
        UnPauseGame();

        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        UnPauseGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetLeaderboard()
    {
        PlayerPrefs.DeleteKey(LeaderboardKey);

        PlayerPrefs.Save();

        ShowLeaderboard();
    }

    private class LeaderboardEntry
    {
        public string Nickname { get; }

        public int Score { get; }

        public LeaderboardEntry(string nickname, int score)
        {
            Nickname = nickname;
            
            Score = score;
        }
    }
}