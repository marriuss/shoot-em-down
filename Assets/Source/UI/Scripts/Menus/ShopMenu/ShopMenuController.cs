using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField] private ShopMenuView _view;
    [SerializeField] private GameObject _content;
    [SerializeField] private WeaponView _weaponViewPrefab;
    [SerializeField] private WeaponsPool _weaponsPool;
    [SerializeField] private Player _player;
    
    private void OnEnable()
    {
        List<Weapon> weapons = _weaponsPool.GetWeaponsList();

        foreach (Weapon weapon in weapons)
        {
            WeaponView weaponView = Instantiate(_weaponViewPrefab, _content.transform);
            weaponView.Initialize(weapon, _player);
            weaponView.enabled = true;
        }

        _player.TotalMoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.TotalMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int newMoneyValue)
    {
        _view.SetMoney(newMoneyValue);
    }
}
