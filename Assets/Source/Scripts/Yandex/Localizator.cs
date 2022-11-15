using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
using System.Linq;

[RequireComponent(typeof(LeanLocalization))]
public class Localizator : MonoBehaviour
{
    private List<LeanLanguage> _languages;

    private void Awake()
    {
        _languages = GetComponentsInChildren<LeanLanguage>().ToList();
    }

    public void Localize(string languageCode)
    {
        string languge = _languages.Find(language => language.TranslationCode == languageCode).name;
        LeanLocalization.SetCurrentLanguageAll(languge);
    }
}
