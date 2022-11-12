using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMagazineBarView : BarView
{
    [SerializeField] private Weapon _weapon;

    private void Start()
    {
        SetSlider(_weapon.Magazine.MinAmount, _weapon.Magazine.MaxAmount);
    }

    private void OnEnable()
    {
        _weapon.Magazine.AmountChanged += OnBulletsAmountChanged;
    }

    private void OnDisable()
    {
        _weapon.Magazine.AmountChanged += OnBulletsAmountChanged;
    }

    private void Update()
    {
        if (_weapon.Magazine.IsReloading)
            Enable();
        else
            Disable();
    }

    private void OnBulletsAmountChanged(float newValue)
    {
        ChangeValue(newValue);
    }
}
