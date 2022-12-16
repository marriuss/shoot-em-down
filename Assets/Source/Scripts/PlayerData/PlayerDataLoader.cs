using UnityEngine;
using Agava.YandexGames;

public static class PlayerDataLoader
{
    private const string JsonDataKey = "jsonData";

    public static PlayerData PlayerData { get; private set; }

    public static void LoadData()
    {
        PlayerData = null;

#if UNITY_WEBGL && !UNITY_EDITOR
    if (PlayerAccount.IsAuthorized)
    {
        PlayerAccount.GetPlayerData(onSuccessCallback: LoadJsonData);
    }
    else
    {
        LoadJsonData(PlayerPrefs.GetString(JsonDataKey));
    }
#endif
    }

    private static void LoadJsonData(string jsonPlayerData)
    {
        PlayerData = GetPlayerDataFromJson(jsonPlayerData);   
    }

    private static PlayerData GetPlayerDataFromJson(string jsonData)
    {
        return JsonUtility.FromJson<PlayerData>(jsonData);
    }
}