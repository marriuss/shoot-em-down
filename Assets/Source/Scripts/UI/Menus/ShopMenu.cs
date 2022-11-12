using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : Menu
{
    [SerializeField] private TMP_Text _textContainer;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private GameObject _content;
    [SerializeField] private WeaponView _weaponViewPrefab;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        foreach (Weapon weapon in _weapons)
        {
            WeaponView weaponView = Instantiate(_weaponViewPrefab, _content.transform);
            weaponView.Initialize(weapon, _player);
        }

        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int newMoneyValue)
    {
        _textContainer.text = newMoneyValue.ToString();
    }
}
