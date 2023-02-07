using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField] private ShopMenuView _view;
    [SerializeField] private GameObject _content;
    [SerializeField] private WeaponView _weaponViewPrefab;
    [SerializeField] private WeaponsPool _weapons;
    [SerializeField] private PlayerData _playerData;
    
    private void Start()
    {
        List<Weapon> weapons = _weapons.Weapons.OrderBy(weapon => weapon.Info.Cost).ToList();

        foreach (Weapon weapon in weapons)
        {
            WeaponView weaponView = Instantiate(_weaponViewPrefab, _content.transform);
            weaponView.Initialize(weapon, _playerData);
        }
    }

    private void Update()
    {
        _view.SetMoney(_playerData.Money);
    }
}
