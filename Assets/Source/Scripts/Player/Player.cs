using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : ResetableMonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;

    private int _money;
    private List<Weapon> _weapons;

    public event UnityAction<int> MoneyChanged;
    public event UnityAction<Collider> ShotCollider;

    public Weapon CurrentWeapon { get; private set; }
    public bool HasWeapon(Weapon weapon) => _weapons.Contains(weapon);
    public bool CanBuy(Weapon weapon) => _money >= weapon.Cost;

    private void Awake()
    {
        _weapons = new List<Weapon>();
        AddWeapon(_startWeapon);
        EquipWeapon(_startWeapon);
        ChangeMoneyValue(0);
    }

    public void SpawnWeapon(Vector3 position)
    {
        CurrentWeapon.Translate(position);
    }

    public override void SetStartState()
    {
        CurrentWeapon.SetStartState();
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.ShotCollider -= OnShotCollider;
            CurrentWeapon.HitCollider -= OnWeaponHitCollider;
        }

        CurrentWeapon = weapon;
        weapon.ShotCollider += OnShotCollider;
        weapon.HitCollider += OnWeaponHitCollider;
    }

    public void BuyWeapon(Weapon weapon)
    {
        ChangeMoneyValue(_money - weapon.Cost);
        AddWeapon(weapon);
    }

    private void OnShotCollider(Collider collider)
    {
        if (collider.TryGetComponent(out Money money))
        {
            AddMoney(money);
        }
        else
        {
            ShotCollider?.Invoke(collider);
        }
    }

    private void OnWeaponHitCollider(Collider collider)
    {
        if (collider.TryGetComponent(out Money money))
            AddMoney(money);
    }

    private void AddMoney(Money money)
    {
        ChangeMoneyValue(_money + money.Value);
    }

    private void ChangeMoneyValue(int newValue)
    {
        _money = newValue;
        MoneyChanged?.Invoke(_money);
    }

    private void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
    }
}
