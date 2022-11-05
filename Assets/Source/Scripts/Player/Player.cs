using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;

    public int Money { get; private set; }
    public Weapon CurrentWeapon { get; private set; }

    public UnityAction Crashed;
    public UnityAction<Collider> ShotCollider;

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
        {
            CurrentWeapon.ShotCollider -= OnShotCollider;
            CurrentWeapon.HitCollider -= OnWeaponHitCollider;
        }

        CurrentWeapon = weapon;
        weapon.ShotCollider += OnShotCollider;
        weapon.HitCollider += OnWeaponHitCollider;
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
        {
            AddMoney(money);
        }
        else if (collider.TryGetComponent(out Platform platform))
        {
            Crashed?.Invoke();
        }
    }

    private void AddMoney(Money money)
    {
        Money += money.Value;
    }
}
