using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private SettingsMenuView _view;
    [SerializeField] private Settings _settings;

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
        _view.SetSettings(_settings.MusicOn, _settings.SoundsOn);
    }
}
