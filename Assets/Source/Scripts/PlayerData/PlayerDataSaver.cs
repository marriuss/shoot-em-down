using UnityEngine;
using Agava.YandexGames;

public static class PlayerDataSaver
{
    private const string JsonDataKey = "jsonData";
    private static string lastSavedData;

    public static void SaveData(PlayerData playerData)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    string jsonData = JsonUtility.ToJson(playerData);
    
    if (jsonData != lastSavedData)
        SaveJsonData(jsonData);
#endif
    }

    private static void SaveJsonData(string jsonData)
    {
        lastSavedData = jsonData;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(jsonData);

        PlayerPrefs.SetString(JsonDataKey, jsonData);
        PlayerPrefs.Save();
    }

}
