using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;


public class LeaderboardMenuView : MenuView
{
    [SerializeField] private Image _background;
    [SerializeField] private LeaderboardView _view;

    private void OnEnable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    RequestData();
#endif
    }

    public void SetEntries(IReadOnlyList<LeaderboardEntry> entries)
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
    }

    private void RequestData()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            if (PlayerAccount.HasPersonalProfileDataPermission == false)
                PlayerAccount.RequestPersonalProfileDataPermission();
        }
    }
}
