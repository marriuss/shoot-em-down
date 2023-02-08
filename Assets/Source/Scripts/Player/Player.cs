using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private Vector3 _weaponSpawnPosition;
    
    public int LevelMoney { get; private set; }

    public event UnityAction<Collider> ShotCollider;

    public Weapon CurrentWeapon { get; private set; }

    private void Start()
    {
        _weaponSpawnPosition = transform.position;
        Weapon weapon = Instantiate(_playerData.Weapon);
        CurrentWeapon = weapon;
        CurrentWeapon.SetSpawnPoint(_weaponSpawnPosition);
        weapon.ShotCollider += OnShotCollider;
        weapon.HitCollider += OnWeaponHitCollider;
    }

    public void EndLevel()
    {
        _playerData.SetMoney(_playerData.Money + LevelMoney);
        CurrentWeapon.enabled = false;
    }

    public void ResetState()
    {
        CurrentWeapon.ResetState();
        CurrentWeapon.enabled = true;
        LevelMoney = 0;
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
        LevelMoney += money.Value;
    }
}
