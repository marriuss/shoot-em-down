using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private int _topPlayers;
    [SerializeField] private GameObject _container;
    [SerializeField] private LeaderboardEntryView _leaderboardEntryViewPrefab;
    [SerializeField] private LeaderboardEntryView _playerLeaderboardEntry;
    [SerializeField] private Gradient _gradient;

    private List<LeaderboardEntryView> _leaderboardEntryViews;

    private void Awake()
    {
        _leaderboardEntryViews = new List<LeaderboardEntryView>();
        Color color;

        for (int i = 0; i < _topPlayers; i++)
        {
            color = _gradient.Evaluate(i * 1.0f / _topPlayers);
            LeaderboardEntryView entryView = Instantiate(_leaderboardEntryViewPrefab, _container.transform);
            entryView.SetColor(color);
            _leaderboardEntryViews.Add(entryView);
        }
    }

    public void SetEntries(IReadOnlyList<LeaderboardEntry> entries)
    {
        for (int i = 0; i < entries.Count; i++)
            _leaderboardEntryViews[i].SetData(entries[i]);
    }

    public void SetPlayerEntry(LeaderboardEntry playerEntry)
    {
        _playerLeaderboardEntry.SetData(playerEntry);
    }
}
