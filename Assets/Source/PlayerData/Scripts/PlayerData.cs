using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "SO/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _startMoney;
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private List<Weapon> _startWeapons;

    public int Money => _startMoney;
    public Weapon Weapon => _startWeapon;
    public HashSet<Weapon> Weapons => new HashSet<Weapon>(_startWeapons);

    public bool HasWeapon(Weapon weapon) => _startWeapons.Contains(weapon);
    public bool CanBuy(Weapon weapon) => Money >= weapon.Info.Cost;

    public event UnityAction DataChanged;

    public void LoadData(int money, Weapon startWeapon, List<Weapon> weapons)
    {
        SetData(money, startWeapon, weapons);
    }

    public void SetMoney(int money)
    {
        if (money < 0)
            throw new ArgumentException("Negative argument.");

        _startMoney = money;
        InvokeDataChange();
    }

    public void SetCurrentWeapon(Weapon weapon)
    {
        if (weapon != null && HasWeapon(weapon))
        {
            _startWeapon = weapon;
            InvokeDataChange();
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            _startWeapons.Add(weapon);
            InvokeDataChange();
        }
    }

    private void InvokeDataChange()
    {
        DataChanged?.Invoke();
    }

    private void SetData(int money, Weapon startWeapon, List<Weapon> weapons)
    {
        _startMoney = money;
        _startWeapon = startWeapon;
        _startWeapons = weapons;
    }
}
