using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private AudioMixerController _musicController;
    [SerializeField] private AudioMixerController _soundsController;

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
        if (focus && _settings.MusicOn)
        {
            SetMusicActive(true);
        }
        else
        {
            SetMusicActive(false);
        }
    }

    public void MuteMusic()
    {
        SetMusicActive(false);
    }

    public void UnmuteMusic()
    {
        SetMusicActive(_settings.MusicOn);
    }

    private void SetMusicActive(bool isOn)
    {
        _musicController.SetActive(isOn);
    }

    private void SetSoundsActive(bool isOn)
    {
        _soundsController.SetActive(isOn);
    }

    private void OnSettingsChanged()
    {
        SetMusicActive(_settings.MusicOn);
        SetSoundsActive(_settings.SoundsOn);
    }
}
