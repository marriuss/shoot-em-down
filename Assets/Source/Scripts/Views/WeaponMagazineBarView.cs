using UnityEngine;

public class WeaponMagazineBarView : BarView
{
    [SerializeField] private Player _player;

    private Weapon _weapon;

    private void Update()
    {
        if (_player.CurrentWeapon != _weapon)  
            ChangeWeapon(_player.CurrentWeapon);

        if (_weapon != null)
        {
            if (_weapon.Magazine.IsReloading)
                Enable();
            else
                Disable();
        }
    }

    private void ChangeWeapon(Weapon newWeapon)
    {
        if (_weapon != null)
            _weapon.Magazine.AmountChanged -= OnBulletsAmountChanged;

        _weapon = newWeapon;
        SetSlider(_weapon.Magazine.MinAmount, _weapon.Magazine.MaxAmount);
        _weapon.Magazine.AmountChanged += OnBulletsAmountChanged;
    }

    private void OnBulletsAmountChanged(float newValue)
    {
        ChangeValue(newValue);
    }
}
