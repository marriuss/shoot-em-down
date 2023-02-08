using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : MenuView
{
    [SerializeField] private Image _background;
    [SerializeField] private Toggle _musicOnToggle;
    [SerializeField] private Toggle _soundsOnToggle;

    public void SetSettings(bool musicOn, bool soundsOn)
    {
        _musicOnToggle.isOn = musicOn;
        _soundsOnToggle.isOn = soundsOn;
    }

    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);
        _background.gameObject.SetActive(active);
    }
}
