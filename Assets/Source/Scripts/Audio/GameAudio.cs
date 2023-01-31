using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private AudioMixerController _musicController;
    [SerializeField] private AudioMixerController _soundsController;

    private bool _isMusicOn;

    private void Awake()
    {
        _isMusicOn = true;
    }

    private void OnEnable()
    {
        _settings.SettingsChanged += OnSettingsChanged;
    }

    private void OnDisable()
    {
        _settings.SettingsChanged -= OnSettingsChanged;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (_isMusicOn)
                SetMusicMode(_settings.MusicOn);
        }
        else
        {
            SetMusicMode(false);
        }
    }

    public void TurnOnMusic()
    {
        _isMusicOn = true;
        SetMusicMode(_settings.MusicOn);
    }

    public void TurnOffMusic()
    {
        _isMusicOn = false;
        SetMusicMode(false);
    }

    private void SetMusicMode(bool isOn)
    {
        _musicController.SetMode(isOn);
    }

    private void OnSettingsChanged()
    {
        _musicController.SetMode(_settings.MusicOn);
        _soundsController.SetMode(_settings.SoundsOn);
    }
}
