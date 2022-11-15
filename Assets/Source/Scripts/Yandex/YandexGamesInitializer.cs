using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

public class YandexGamesInitializer : MonoBehaviour
{
    [SerializeField] private string[] leaderboards;
    [SerializeField] private PlayerDataLoader _playerDataLoader;
    [SerializeField] private Localizator _localizator;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        yield return YandexGamesSdk.Initialize();

        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        _localizator.Localize(languageCode);

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();

        PlayerAccount.GetPlayerData(_playerDataLoader.LoadData);
#endif
        yield return null;
    }
}
