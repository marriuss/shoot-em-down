using UnityEngine;
using TMPro;

public class ShopWeaponButton : WorkButton
{
    [SerializeField] TMP_Text _buyTextContainer;
    [SerializeField] TMP_Text _equipTextContainer;

    private Weapon _weapon;
    private PlayerData _playerData;

    private void Update()
    {
        if (_playerData == null || _weapon == null)
            return;

        bool playerHasWeapon = _playerData.HasWeapon(_weapon);

        SetInteractable(playerHasWeapon ? _playerData.CurrentWeapon != _weapon : _playerData.CanBuy(_weapon));
        _equipTextContainer.enabled = playerHasWeapon;
        _buyTextContainer.enabled = !playerHasWeapon;
    }

    public void Initialize(Weapon weapon, PlayerData playerData)
    {
        _weapon = weapon;
        _playerData = playerData;
    }

    protected override void OnButtonClick()
    {
        if (_playerData.HasWeapon(_weapon))
        {
            _playerData.SetCurrentWeapon(_weapon);
        }
        else if (_playerData.CanBuy(_weapon))
        {
            _playerData.SetMoney(_playerData.Money - _weapon.Info.Cost);
            _playerData.AddWeapon(_weapon);
        }
    }
}
