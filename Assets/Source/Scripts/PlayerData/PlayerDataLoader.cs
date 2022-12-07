using UnityEngine;
using Agava.YandexGames;

public static class PlayerDataLoader
{
    private static PlayerData loadedData = null;

    public static PlayerData LoadData()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.GetPlayerData(onSuccessCallback:LoadData);
#endif

        return loadedData;
    }

    private static void LoadData(string jsonPlayerData)
    {
        loadedData = JsonUtility.FromJson<PlayerData>(jsonPlayerData);
    }
}