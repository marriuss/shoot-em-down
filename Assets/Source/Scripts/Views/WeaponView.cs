using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class WeaponView : MonoBehaviour
{
    [SerializeField] TMP_Text _costContainer;
    [SerializeField] Button _button;
    [SerializeField] TMP_Text _buttonTextContainer;
    [SerializeField] Image _image;

    private WeaponStats _weaponStats;
    private Player _player;
    private Weapon _weapon;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void Update()
    {
        if (_player == null || _weapon == null)
            return;

        if (_player.HasWeapon(_weapon))
        {
            _button.enabled = _player.CurrentWeapon != _weapon;
            _buttonTextContainer.text = "Equip";
            _costContainer.enabled = false;
        }
        else
        {
            _button.enabled = _player.CanBuy(_weapon);
            _buttonTextContainer.text = "Buy";
        }
    }

    public void Initialize(Weapon weapon, Player player)
    {
        _player = player;
        _weapon = weapon;
        _weaponStats = _weapon.WeaponStats;
        _costContainer.text = _weaponStats.Cost.ToString();
        _image.sprite = _weaponStats.Sprite;
    }

    private void OnButtonClick()
    {
        if (_player.HasWeapon(_weapon))
        {
            _player.EquipWeapon(_weapon);
        }
        else if (_player.CanBuy(_weapon))
        {
            _player.BuyWeapon(_weapon);
        }
    }
}
