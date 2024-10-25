using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    private const string LeaderboardKey = "Leaderboard";

    [System.Serializable]
    public class PlayerRecord
    {
        public string playerName;
        public int score;
    }

    private List<PlayerRecord> leaderboard;

    private List<PlayerRecord> beatenRecords;

    private void Start()
    {
        Instance = this;

        LoadLeaderboard();

        beatenRecords = new List<PlayerRecord>();
    }

    private void LoadLeaderboard()
    {
        InitializeLeaderboard();

        string leaderboardData = LoadLeaderboardData();

        string[] records = SplitLeaderboardData(leaderboardData);

        ParseAndAddRecords(records);

        SortLeaderboard();
    }

    private void InitializeLeaderboard()
    {
        leaderboard = new List<PlayerRecord>();
    }

    private string LoadLeaderboardData()
    {
        return PlayerPrefs.GetString(LeaderboardKey, "");
    }

    private string[] SplitLeaderboardData(string leaderboardData)
    {
        return leaderboardData.Split('\n');
    }

    private void ParseAndAddRecords(string[] records)
    {
        foreach (string record in records)
        {
            if (!string.IsNullOrEmpty(record))
            {
                string[] parts = record.Split(':');

                if (parts.Length == 2)
                {
                    string playerName = parts[0].Trim();
                    
                    int score = int.Parse(parts[1].Trim());

                    leaderboard.Add(new PlayerRecord { playerName = playerName, score = score });
                }
            }
        }
    }

    private void SortLeaderboard()
    {
        leaderboard.Sort((x, y) => x.score.CompareTo(y.score));
    }

    public bool CheckRecord(int score, out string recordMessage)
    {
        recordMessage = "";

        foreach (var record in leaderboard)
        {
            if (score > record.score && !beatenRecords.Contains(record))
            {
                recordMessage = $"You beat {record.playerName} record!";

                beatenRecords.Add(record);

                return true;
            }
        }
        return false;
    }
}