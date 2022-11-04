using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;

    public int Money { get; private set; }
    public Weapon CurrentWeapon { get; private set; }

    private void Start()
    {
        EquipWeapon(Instantiate(_startWeapon));
        Money = 0;
    }

    public void SpawnWeapon(Vector3 position)
    {
        CurrentWeapon.Translate(position);
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (CurrentWeapon != null)
            CurrentWeapon.CollectedMoney -= OnCollectedMoney;

        CurrentWeapon = weapon;
        weapon.CollectedMoney += OnCollectedMoney;
    }

    private void OnCollectedMoney(int value)
    {
        if (value < 0)
            throw new ArgumentException();

        Money += value;
    }
}
