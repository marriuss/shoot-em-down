using UnityEngine;
using Agava.YandexGames;

public class PlayerDataSaver : MonoBehaviour
{
    private string _lastSavedData;

    public void SaveData(PlayerData playerData)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string jsonData = JsonUtility.ToJson(playerData);

        if (jsonData != _lastSavedData)
        {
            PlayerAccount.SetPlayerData(jsonData);
            _lastSavedData = jsonData;
        }
#endif
    }
}
