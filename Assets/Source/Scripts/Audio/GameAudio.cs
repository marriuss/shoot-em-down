using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private List<Sound> _sounds;
    private List<Music> _music;

    private bool _isMusicOn;

    private void Awake()
    {
        _sounds = GetComponentsInChildren<Sound>().ToList();
        _music = GetComponentsInChildren<Music>().ToList();
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
                SetMusicMode(_settings.IsMusicOn);
        }
        else
        {
            SetMusicMode(false);
        }
    }

    public void TurnOnMusic()
    {
        _isMusicOn = true;
        SetMusicMode(_settings.IsMusicOn);
    }

    public void TurnOffMusic()
    {
        _isMusicOn = false;
        SetMusicMode(false);
    }

    private void SetMusicMode(bool isOn)
    {
        SetMode(_music, isOn);
    }

    private void SetMode<T>(List<T> audios, bool isActive) where T : Audio
    {
        foreach (T audio in audios)
            audio.SetMode(isActive);
    }

    private void OnSettingsChanged()
    {
        SetMode(_sounds, _settings.AreSoundsOn);
        SetMode(_music, _settings.IsMusicOn);
    }
}
