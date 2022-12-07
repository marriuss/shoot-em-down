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
    [SerializeField] LeanLocalization _leanLocalization;
    [SerializeField] private Settings _settings;
    [SerializeField] private Menu _authorizationMenu;

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
        LeanLocalization.SetCurrentLanguageAll(_language);

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            _authorizationMenu.Close();
            Game.Load(_settings);
        }
        else
        {
            _authorizationMenu.Open();
        }
#else
        Game.Load(_settings);
#endif

        yield return null;
    }
}
