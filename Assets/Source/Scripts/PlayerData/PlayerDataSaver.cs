using UnityEngine;
using Agava.YandexGames;

public static class PlayerDataSaver
{
    private static string lastSavedData;

    public static void SaveData(PlayerData playerData)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    if (PlayerAccount.IsAuthorized)
    {
        string jsonData = JsonUtility.ToJson(playerData);

        if (jsonData != lastSavedData)
        {
            PlayerAccount.SetPlayerData(jsonData);
            lastSavedData = jsonData;
        }
    }
#endif
    }

}
