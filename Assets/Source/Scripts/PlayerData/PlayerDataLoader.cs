using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoader : MonoBehaviour
{
    [SerializeField] private WeaponsPool _weaponsPool;
    [SerializeField] private Player _player;

    public void LoadData(string jsonPlayerData)
    {
        if (string.IsNullOrEmpty(jsonPlayerData))
            return;

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonPlayerData);
        HashSet<Weapon> weapons = new HashSet<Weapon>();
        string currentWeaponName = playerData.CurrentWeaponName;
        string[] weaponNames = playerData.Weapons;
        Weapon currentWeapon = null;
        Weapon weapon;

        foreach (string weaponName in weaponNames)
        {
            weapon = _weaponsPool.FindByName(weaponName);
            weapons.Add(weapon);

            if (currentWeaponName == weaponName)
                currentWeapon = weapon;
        }

        _player.LoadData(playerData.Money, weapons, currentWeapon);
    }
}