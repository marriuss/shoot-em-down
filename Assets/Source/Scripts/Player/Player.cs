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
        Weapon weapon = Instantiate(_playerData.CurrentWeapon);
        EquipWeapon(weapon);
    }

    public void ResetState()
    {
        CurrentWeapon.ResetState();
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
}
