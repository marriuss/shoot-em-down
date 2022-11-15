using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

public class YandexGamesInitializer : MonoBehaviour
{
    [SerializeField] private string[] leaderboards;
    [SerializeField] private PlayerDataLoader _playerDataLoader;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        yield return YandexGamesSdk.Initialize();

        LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();

        PlayerAccount.GetPlayerData(_progressLoader.LoadData);

        //Leaderboard.GetPlayerEntry(LeaderboardConstants.Name, TryCreatePlayerLeaderboardEntity);
        //Leaderboard.GetEntries(LeaderboardConstants.Name, _leaderboard.Init);

#endif

        yield return null;
    }
}
