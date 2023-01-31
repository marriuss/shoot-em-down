using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System;
using UnityEngine.Events;

public static class LeaderboardData
{
    private const string LeaderboardName = "LeaderboardPoints";
    private const int TopPlayers = 15;

    private static int bestScore;
    private static LeaderboardEntry playerEntry;
    private static List<LeaderboardEntry> entries = new();

    public static event UnityAction<LeaderboardEntry> PlayerEntryLoaded;
    public static event UnityAction<List<LeaderboardEntry>> EntriesLoaded;

    static LeaderboardData()
    {
        bestScore = 0;
    }

    public static void SetScore(int score)
    {
        if (playerEntry == null)
            return;

        if (bestScore < score)
        {
            bestScore = score;

#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.SetScore(LeaderboardName, score);
#endif
        }
    }

    public static void LoadData()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();

        Leaderboard.GetPlayerEntry(LeaderboardName, onSuccessCallback: LoadPlayerEntry);
        Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: LoadEntries, topPlayersCount: TopPlayers, competingPlayersCount: 0, includeSelf: true);
#endif
    }

    private static void LoadPlayerEntry(LeaderboardEntryResponse entryResponse)
    {
        if (entryResponse == null)
            return;

        bestScore = entryResponse.score;
        playerEntry = new LeaderboardEntry(entryResponse);
        PlayerEntryLoaded?.Invoke(playerEntry);
    }

    private static void LoadEntries(LeaderboardGetEntriesResponse leaderboardEntryResponses)
    {
        entries.Clear();

        foreach (LeaderboardEntryResponse entryResponse in leaderboardEntryResponses.entries)
            entries.Add(new LeaderboardEntry(entryResponse));

        EntriesLoaded?.Invoke(entries);
    }
}
