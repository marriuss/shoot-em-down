using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System.Linq;

public class PlayerDataStorage : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private WeaponsPool _weaponsPool;

    private const string JsonDataKey = "jsonData";

    private static string lastSavedData;

    private void OnEnable()
    {
        _playerData.DataChanged += OnDataChanged;
    }

    private void OnDisable()
    {
        _playerData.DataChanged -= OnDataChanged;
    }

    public void LoadData()
    {
        bool usePrefs = true;

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            usePrefs = false;
            PlayerAccount.GetPlayerData(onSuccessCallback: LoadJsonData);
        }
#endif

        if (usePrefs && PlayerPrefs.HasKey(JsonDataKey))
            LoadJsonData(PlayerPrefs.GetString(JsonDataKey));
    }

    private PlayerDataObject GetPlayerDataFromJson(string jsonData)
    {
        return JsonUtility.FromJson<PlayerDataObject>(jsonData);
    }

    private void LoadJsonData(string jsonPlayerData)
    {
        PlayerDataObject playerDataObject = GetPlayerDataFromJson(jsonPlayerData);

        string currentWeaponName = playerDataObject.CurrentWeaponName;
        string[] weaponNames = playerDataObject.Weapons;

        Weapon startWeapon = null;
        List<Weapon> startWeapons = new List<Weapon>();
        Weapon weapon;

        foreach (string weaponName in weaponNames)
        {
            weapon = _weaponsPool.FindByName(weaponName);

            if (weapon != null)
            {
                startWeapons.Add(weapon);

                if (currentWeaponName == weaponName)
                    startWeapon = weapon;
            }
        }

        _playerData.LoadData(playerDataObject.Money, startWeapon, startWeapons);
    }

    private void OnDataChanged()
    {
        SaveData();
    }

    private void SaveData()
    {
        PlayerDataObject playerDataObject = GetPlayerDataObject();
        string jsonData = JsonUtility.ToJson(playerDataObject);

        if (jsonData != lastSavedData)
            SaveJsonData(jsonData);
    }

    private PlayerDataObject GetPlayerDataObject()
    {
        string[] weaponNames = _playerData.Weapons.Select(weapon => weapon.Info.Name).ToHashSet().ToArray();
        return new PlayerDataObject(_playerData.Weapon.Info.Name, weaponNames, _playerData.Money);
    }

    private void SaveJsonData(string jsonData)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(jsonData);
#endif

        PlayerPrefs.SetString(JsonDataKey, jsonData);
        PlayerPrefs.Save();
        lastSavedData = jsonData;
    }
}
