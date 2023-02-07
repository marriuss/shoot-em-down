using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PlayerData", menuName ="SO/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _money;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private List<Weapon> _weapons;

    public int Money => _money;
    public Weapon CurrentWeapon => _currentWeapon;
    public List<Weapon> Weapons => new List<Weapon>(_weapons);

    public bool HasWeapon(Weapon weapon) => _weapons.Contains(weapon);
    public bool CanBuy(Weapon weapon) => _money >= weapon.Info.Cost;

    public void SetMoney(int money)
    {
        _money = money; 
    }

    public void SetCurrentWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    public void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
    }

}
