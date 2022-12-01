using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [SerializeField] private Settings _settings;
    [SerializeField] private Toggle _isMusicOnToggle;
    [SerializeField] private Toggle _areSoundsOnToggle;

    private void OnEnable()
    {
        _settings.SettingsChanged += OnSettingsChanged;
    }

    private void OnDisable()
    {
        _settings.SettingsChanged -= OnSettingsChanged;
    }

    private void OnSettingsChanged()
    {
        _isMusicOnToggle.isOn = _settings.IsMusicOn;
        _areSoundsOnToggle.isOn = _settings.AreSoundsOn;
    }
}
