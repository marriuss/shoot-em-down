using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenuController : MonoBehaviour
{
    [SerializeField] private LeaderboardMenuView _view;

    private void OnEnable()
    {
        LeaderboardData.PlayerEntryLoaded += OnPlayerEntryLoaded;
        LeaderboardData.EntriesLoaded += OnEntriesLoaded;
    }

    private void OnDisable()
    {
        LeaderboardData.PlayerEntryLoaded -= OnPlayerEntryLoaded;
        LeaderboardData.EntriesLoaded -= OnEntriesLoaded;
    }

    private void Start()
    {
        LeaderboardData.LoadData();
    }

    private void OnPlayerEntryLoaded(LeaderboardEntry playerEntry)
    {
        _view.SetPlayerEntry(playerEntry);
    }

    private void OnEntriesLoaded(List<LeaderboardEntry> entries)
    {
        _view.SetEntries(entries);
    }
    
}
