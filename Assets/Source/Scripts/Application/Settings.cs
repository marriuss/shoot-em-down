using UnityEngine;
using UnityEngine.Events;

public class Settings : MonoBehaviour
{
    private bool _musicOn;
    private bool _soundsOn;
    private string _language;

    public event UnityAction SettingsChanged;

    public bool MusicOn => _musicOn;
    public bool SoundsOn => _soundsOn;
    public string Language => _language;

    private void Start()
    {
        SettingsChanged?.Invoke();
    }

    public void SetSettings(Settings settings)
    {
        _musicOn = settings.MusicOn;
        _soundsOn = settings.SoundsOn;
        _language = settings.Language;
        //SettingsChanged?.Invoke();
    }

    public void SetSettings(bool musicOn, bool soundsOn, string language)
    {
        _musicOn = musicOn;
        _soundsOn = soundsOn;
        _language = language;
        //SettingsChanged?.Invoke();
    }

    public void ChangeMusicSettings(bool isMusicOn)
    {
        ChangeSettings(ref _musicOn, isMusicOn);
    }

    public void ChangeSoundsSettings(bool isSoundsOn)
    {
        ChangeSettings(ref _soundsOn, isSoundsOn);
    }

    public void ChangeLanguage(string languageName)
    {
        ChangeSettings(ref _language, languageName);
    }

    private void ChangeSettings<T>(ref T settings, T value)
    {
        settings = value;
        SettingsChanged?.Invoke();
    }
}
