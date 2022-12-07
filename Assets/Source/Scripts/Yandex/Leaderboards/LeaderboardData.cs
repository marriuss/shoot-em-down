using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class LeaderboardData : MonoBehaviour
{
    [SerializeField] private string _leaderboardName;
    [SerializeField] private LeaderboardView _leaderboardView;

    private const int TopPlayers = 15;

    private int _bestScore;

    private void Awake()
    {
        _bestScore = 0;
        LoadData();
    }

    public void SetScore(int score)
    {
        if (_bestScore < score)
        {
            _bestScore = score;

#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.SetScore(_leaderboardName, score);
#endif
            LoadData();
        }
    }

    private void LoadData()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();

        Leaderboard.GetPlayerEntry(_leaderboardName, onSuccessCallback: LoadPlayerEntry);
        Leaderboard.GetEntries(_leaderboardName, onSuccessCallback: LoadEntries, topPlayersCount: TopPlayers, competingPlayersCount: 0, includeSelf: true);
#endif
    }

    private void LoadPlayerEntry(LeaderboardEntryResponse entryResponse)
    {
        if (entryResponse == null)
            return;

        _bestScore = entryResponse.score;
        _leaderboardView.SetPlayerEntry(new LeaderboardEntry(entryResponse));
    }

    private void LoadEntries(LeaderboardGetEntriesResponse leaderboardEntryResponses)
    {
        List<LeaderboardEntry> _entries = new List<LeaderboardEntry>();

        foreach (LeaderboardEntryResponse entryResponse in leaderboardEntryResponses.entries)
            _entries.Add(new LeaderboardEntry(entryResponse));

        _leaderboardView.SetEntries(_entries);
    }
}
