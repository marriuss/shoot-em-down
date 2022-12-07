using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Player : ResetableMonoBehaviour
{
    [SerializeField] private WeaponsPool _weaponsPool;
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private bool _isCloudDataUsed;

    private Vector3 _weaponSpawnPosition;
    private int _levelMoney;
    private int _totalMoney;
    private HashSet<Weapon> _weapons;
    private Weapon _nextWeapon;

    public event UnityAction<int> TotalMoneyChanged;
    public event UnityAction<Collider> ShotCollider;
    public event UnityAction<int> CollectedMoney;

    public Weapon CurrentWeapon { get; private set; }
    public bool HasWeapon(Weapon weapon) => _weapons.Contains(weapon);
    public bool CanBuy(Weapon weapon) => _totalMoney >= weapon.Info.Cost;

    private void Awake()
    {
        _weaponSpawnPosition = transform.position;
        _weapons = new HashSet<Weapon>();
        _nextWeapon = null;
        ChangeTotalMoney(0);
        AddWeapon(_startWeapon);
        EquipWeapon(_startWeapon);
        LoadData();
    }

    public override void SetStartState()
    {
        if (_nextWeapon != null)
            EquipWeapon(_nextWeapon);

        CurrentWeapon.SetStartState();
        _levelMoney = 0;
    }

    public void PickShopWeapon(Weapon weapon)
    {
        if (HasWeapon(weapon))
        {
            EquipWeapon(weapon);
        }
        else if (CanBuy(weapon))
        {
            BuyWeapon(weapon);
        }

        SaveData();
    }

    public void SaveLevelProgress()
    {
        AddMoney(_levelMoney);
        _levelMoney = 0;
        SaveData();
    }

    public void AddMoney(int amount)
    {
        if (amount < 0)
            throw new ArgumentException();

        ChangeTotalMoney(_totalMoney + amount);
    }

    private void OnShotCollider(Collider collider)
    {
        if (collider.TryGetComponent(out Money money))
        {
            CollectMoney(money);
        }
        else
        {
            ShotCollider?.Invoke(collider);
        }
    }

    private void OnWeaponHitCollider(Collider collider)
    {
        if (collider.TryGetComponent(out Money money))
            CollectMoney(money);
    }

    private void CollectMoney(Money money)
    {
        int moneyAmount = money.Value;
        _levelMoney += moneyAmount;
        CollectedMoney?.Invoke(moneyAmount);
    }

    private void ChangeTotalMoney(int newAmount)
    {
        _totalMoney = newAmount;
        TotalMoneyChanged?.Invoke(_totalMoney);
    }

    private void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
    }

    private void BuyWeapon(Weapon weapon)
    {
        ChangeTotalMoney(_totalMoney - weapon.Info.Cost);
        AddWeapon(weapon);
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.ShotCollider -= OnShotCollider;
            CurrentWeapon.HitCollider -= OnWeaponHitCollider;
            CurrentWeapon.Despawn();
        }

        CurrentWeapon = weapon;
        CurrentWeapon.SetSpawnPoint(_weaponSpawnPosition);
        weapon.ShotCollider += OnShotCollider;
        weapon.HitCollider += OnWeaponHitCollider;
    }

    private void LoadData()
    {
        if (_isCloudDataUsed)
        {
            PlayerData data = PlayerDataLoader.LoadData();

            if (data == null)
                return;

            ChangeTotalMoney(data.Money);

            string currentWeaponName = data.CurrentWeaponName;
            string[] weaponNames = data.Weapons;

            Weapon weapon;

            foreach (string weaponName in weaponNames)
            {
                weapon = _weaponsPool.FindByName(weaponName);

                if (weapon != null)
                    AddWeapon(weapon);

                if (currentWeaponName == weaponName)
                    EquipWeapon(weapon);
            }
        }
    }

    private void SaveData()
    {
        if (_isCloudDataUsed)
        {
            string[] weaponNames = _weapons.Select(weapon => weapon.Info.Name).ToArray();
            PlayerData playerData = new PlayerData(CurrentWeapon.Info.Name, weaponNames, _totalMoney);
            PlayerDataSaver.SaveData(playerData);
        }
    }
}
