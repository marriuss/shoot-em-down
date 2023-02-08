using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenuController : MonoBehaviour
{
    [SerializeField] private LeaderboardMenuView _view;

    private void Start()
    {
        LeaderboardData.LoadData();
    }

    private void Update()
    {
        _view.SetPlayerEntry(LeaderboardData.PlayerEntry);
        _view.SetEntries(LeaderboardData.Entries);
    }
}
