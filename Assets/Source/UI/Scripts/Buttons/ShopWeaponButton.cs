using UnityEngine;
using TMPro;

public class ShopWeaponButton : WorkButton
{
    [SerializeField] TMP_Text _buyTextContainer;
    [SerializeField] TMP_Text _equipTextContainer;

    private Weapon _weapon;
    private Player _player;

    private bool _playerHasWeapon => _player.HasWeapon(_weapon);
    private bool _playerCanBuyWeapon => _player.CanBuy(_weapon);

    private void Update()
    {
        if (_player == null || _weapon == null)
            return;

        SetInteractable(_playerHasWeapon ? _player.CurrentWeapon != _weapon : _playerCanBuyWeapon);
        _equipTextContainer.enabled = _playerHasWeapon;
        _buyTextContainer.enabled = !_playerHasWeapon;
    }

    public void Initialize(Weapon weapon, Player player)
    {
        _weapon = weapon;
        _player = player;
    }

    protected override void OnButtonClick()
    {
        _player.PickShopWeapon(_weapon);
    }
}
