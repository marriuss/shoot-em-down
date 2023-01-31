using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Localization;

[RequireComponent (typeof(TMP_Dropdown))]
public class LanguagesDropdown : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private Localizator _localizator;

    private TMP_Dropdown _dropdown;
    private List<string> _languages;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _languages = new List<string>();
        string localization;

        foreach (string language in _localizator.LanguagesNames)
        {
            _languages.Add(language);
            localization = LeanLocalization.GetTranslationText(language);
            _dropdown.options.Add(new TMP_Dropdown.OptionData(localization));
        }
    }

    private void OnEnable()
    {
        _settings.SettingsChanged += OnSettingsChanged;
        _dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _settings.SettingsChanged -= OnSettingsChanged;
        _dropdown.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void OnValueChanged(int index)
    {
        _settings.ChangeLanguage(_languages[index]);
    }

    private void OnSettingsChanged()
    {
        _dropdown.value = _languages.FindIndex(language => language == _settings.Language);
    }
}