using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenuView : MenuView
{
    [SerializeField] private Image _background;
    [SerializeField] private Button _exitButton;
    [SerializeField] private LeaderboardView _view;

    public void SetEntries(List<LeaderboardEntry> entries)
    {
        _view.SetEntries(entries);
    }

    public void SetPlayerEntry(LeaderboardEntry playerEntry)
    {
        _view.SetPlayerEntry(playerEntry);
    }

    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);
        _background.gameObject.SetActive(active);
        _exitButton.gameObject.SetActive(active);
    }
}
