using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System.Linq;

public class PlayerDataLoader : MonoBehaviour
{
    [SerializeField] private WeaponsPool _weaponsPool;

    private HashSet<Weapon> _weapons = new HashSet<Weapon>();

    public Weapon CurrentPlayerWeapon { get; private set; }
    public int Money { get; private set; }
    public HashSet<Weapon> PlayerWeapons => new HashSet<Weapon>(_weapons);

    public void LoadData(string jsonPlayerData)
    {
        if (string.IsNullOrEmpty(jsonPlayerData))
            return;

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonPlayerData);
        string currentWeaponName = playerData.CurrentWeaponName;
        HashSet<string> weaponNames = playerData.Weapons;
        Weapon weapon;

        foreach (string weaponName in weaponNames)
        {
            weapon = _weaponsPool.FindByName(weaponName);
            _weapons.Add(weapon);

            if (currentWeaponName == weaponName)
                CurrentPlayerWeapon = weapon;
        }
    }
}