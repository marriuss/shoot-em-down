using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] TMP_Text _costContainer;
    [SerializeField] ShopWeaponButton _button;
    [SerializeField] Image _image;

    private Player _player;
    private Weapon _weapon;

    private void Update()
    {
        if (_player == null || _weapon == null)
            return;

        _costContainer.enabled = !_player.HasWeapon(_weapon);
    }

    public void Initialize(Weapon weapon, Player player)
    {
        _weapon = weapon;
        _player = player;
        _button.Initialize(weapon, player);
        _costContainer.text = weapon.Info.Cost.ToString();
        _image.sprite = weapon.Info.Sprite;
    }
}
