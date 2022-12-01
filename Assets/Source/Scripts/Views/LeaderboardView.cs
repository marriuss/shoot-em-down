using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private int _topPlayers;
    [SerializeField] private GameObject _container;
    [SerializeField] private LeaderboardEntryView _leaderboardEntryViewPrefab;
    [SerializeField] private LeaderboardEntryView _playerLeaderboardEntry;

    private List<LeaderboardEntryView> _leaderboardEntryViews;

    private void Awake()
    {
        _leaderboardEntryViews = new List<LeaderboardEntryView>();

        for (int i = 0; i < _topPlayers; i++)
            AddEntryView();
    }

    public void SetEntries(List<LeaderboardEntry> entries)
    {
        for (int i = 0; i < entries.Count; i++)
            _leaderboardEntryViews[i].SetData(entries[i]);
    }

    public void SetPlayerEntry(LeaderboardEntry playerEntry)
    {
        _playerLeaderboardEntry.SetData(playerEntry);
    }

    private void AddEntryView()
    {
        LeaderboardEntryView entryView = Instantiate(_leaderboardEntryViewPrefab, _container.transform);
        _leaderboardEntryViews.Add(entryView);
    }
}
