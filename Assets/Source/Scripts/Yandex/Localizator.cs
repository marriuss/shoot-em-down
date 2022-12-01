using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
using System.Linq;

[RequireComponent(typeof(LeanLocalization))]
public class Localizator : MonoBehaviour
{
    [SerializeField] Settings _settings;

    private List<LeanLanguage> _languages;

    public List<string> LanguagesNames => _languages.Select(language => language.name).ToList();

    private void Awake()
    {
        _languages = GetComponentsInChildren<LeanLanguage>().ToList();
    }

    private void OnEnable()
    {
        _settings.SettingsChanged += OnSettingChanged;
    }

    private void OnDisable()
    {
        _settings.SettingsChanged -= OnSettingChanged;
    }

    public void LocalizeByCode(string languageCode)
    {
        LeanLanguage leanLanguage = _languages.Find(language => language.TranslationCode == languageCode);

        if (leanLanguage != null)
            _settings.ChangeLanguage(leanLanguage.name);
    }

    private void OnSettingChanged()
    {
        LeanLocalization.SetCurrentLanguageAll(_settings.Language);
    }
}
