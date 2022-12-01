using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private List<Audio> _audios;
    private List<Sound> _sounds;
    private List<Music> _music;

    private void Awake()
    {
        _audios = GetComponentsInChildren<Audio>().ToList();
        _sounds = SelectAudioTypes<Sound>(_audios);
        _music = SelectAudioTypes<Music>(_audios);
    }

    private void OnEnable()
    {
        _settings.SettingsChanged += OnSettingsChanged;
    }

    private void OnDisable()
    {
        _settings.SettingsChanged -= OnSettingsChanged;
    }

    public void TurnOnMusic()
    {
        SetAudioType(_music, _settings.IsMusicOn);
    }

    public void TurnOffMusic()
    {
        SetAudioType(_music, false);
    }

    private void SetAudioType<T>(List<T> audios, bool isActive) where T : Audio
    {
        foreach (T audio in audios)
            audio.SetMode(isActive);
    }
    
    private List<T> SelectAudioTypes<T>(List<Audio> audios) where T : Audio
    {
        return audios.Where(audio => audio as T != null).Select(audio => (T)audio).ToList();
    }

    private void OnSettingsChanged()
    {
        SetAudioType(_sounds, _settings.AreSoundsOn);
        SetAudioType(_music, _settings.IsMusicOn);
    }
}
