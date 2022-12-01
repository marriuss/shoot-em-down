using System.Collections;
using UnityEngine;
using Agava.YandexGames;

public class YandexGamesInitializer : MonoBehaviour
{
    [SerializeField] private PlayerDataLoader _playerDataLoader;
    [SerializeField] private Localizator _localizator;
    [SerializeField] private LeaderboardData _leaderboardData;
    [SerializeField] private Settings _settings;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize();
        Initialize();
#endif

        yield return null;
    }

    private void Initialize()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        _localizator.LocalizeByCode(languageCode);

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();

        PlayerAccount.GetPlayerData(_playerDataLoader.LoadData);

        _leaderboardData.Initialize();
    }
}
