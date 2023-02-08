using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;
using IJunior.TypedScenes;

public class Initializer : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;
    [SerializeField] private PlayerDataStorage _playerSavedData;
    [SerializeField] private Settings _settings;

    private List<LeanLanguage> _languages;
    private const bool MusicOn = true;
    private const bool SoundsOn = true;
    private const string DefaultLanguage = "English";

    private string _language;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _languages = _leanLocalization.GetComponentsInChildren<LeanLanguage>().ToList();
    }

    private IEnumerator Start()
    {
        LeanLanguage leanLanguage = null;

#if UNITY_WEBGL && !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize();

        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        leanLanguage = _languages.Find(language => language.TranslationCode == languageCode);
#endif

        _language = leanLanguage == null ? DefaultLanguage : leanLanguage.name;
        _settings.SetSettings(musicOn: MusicOn, soundsOn: SoundsOn, language: _language);
        _playerSavedData.LoadData();
        LeanLocalization.SetCurrentLanguageAll(_language);
        MainMenu.Load(_settings);
        yield return null;
    }
}
