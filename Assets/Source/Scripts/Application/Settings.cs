using UnityEngine;
using UnityEngine.Events;
using Lean.Localization;

public class Settings : MonoBehaviour
{
    private bool _isMusicOn = true;
    private bool _areSoundsOn = true;
    private string _language;
    
    public event UnityAction SettingsChanged;

    public bool IsMusicOn => _isMusicOn;
    public bool AreSoundsOn => _areSoundsOn;
    public string Language => _language;

    private void Start()
    {
        SettingsChanged?.Invoke();
    }

    public void SetSettings(Settings settings)
    {
        _isMusicOn = settings.IsMusicOn;
        _areSoundsOn = settings.AreSoundsOn;
        _language = settings.Language;
    }

    public void ChangeMusicSettings(bool isMusicOn)
    {
        ChangeSettings(ref _isMusicOn, isMusicOn);
    }

    public void ChangeSoundsSettings(bool isSoundsOn)
    {
        ChangeSettings(ref _areSoundsOn, isSoundsOn);
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
