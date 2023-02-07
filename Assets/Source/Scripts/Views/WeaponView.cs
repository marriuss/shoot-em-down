using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] TMP_Text _costContainer;
    [SerializeField] ShopWeaponButton _button;
    [SerializeField] Image _image;

    private PlayerData _playerData;
    private Weapon _weapon;

    private void Update()
    {
        if (_playerData == null || _weapon == null)
            return;

        _costContainer.enabled = !_playerData.HasWeapon(_weapon);
    }

    public void Initialize(Weapon weapon, PlayerData playerData)
    {
        _weapon = weapon;
        _playerData = playerData;
        _button.Initialize(weapon, playerData);
        _costContainer.text = weapon.Info.Cost.ToString();
        _image.sprite = weapon.Info.Sprite;
    }
}
