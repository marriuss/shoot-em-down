using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
using UnityEngine.UI;

public class TutorialMenuView : MenuView
{
    [SerializeField] private LocalizedText _text;

    private LeanPhrase _phrase;

    public void SetPhrase(LeanPhrase phrase)
    {
        _phrase = phrase;
    }

    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);

        if (active)
            _text.SetPhrase(_phrase);
    }
}
