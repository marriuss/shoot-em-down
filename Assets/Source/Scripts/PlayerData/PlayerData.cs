using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string CurrentWeaponName;
    public HashSet<string> Weapons;
    public int Money;

    public PlayerData(string currentWeaponName, HashSet<string> weapons, int money)
    {
        CurrentWeaponName = currentWeaponName;
        Weapons = weapons;
        Money = money;
    }
}