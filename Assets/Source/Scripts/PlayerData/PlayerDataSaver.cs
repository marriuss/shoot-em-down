using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class PlayerDataSaver : MonoBehaviour
{
    public void SaveData(PlayerData playerData)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string jsonData = JsonUtility.ToJson(playerData);
        PlayerAccount.SetPlayerData(jsonData);
#endif
    }
}
