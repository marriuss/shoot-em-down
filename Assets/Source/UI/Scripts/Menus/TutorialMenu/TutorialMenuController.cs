using System;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;

public class TutorialMenuController : MonoBehaviour
{
    [SerializeField] private LeanPhrase _firstMenuPhrase;
    [SerializeField] private List<MenuTrigger> _triggers;
    [SerializeField] private MenuGroup _menuGroup;
    [SerializeField] private TutorialMenuView _view;

    private void Start()
    {
        OpenTutorialMenu(_firstMenuPhrase);
    }

    private void OnEnable()
    {
        foreach(MenuTrigger trigger in _triggers)
            trigger.Triggered += OnMenuTriggered;
    }

    private void OnDisable()
    {
        foreach (MenuTrigger trigger in _triggers)
            trigger.Triggered -= OnMenuTriggered;
    }

    private void OnMenuTriggered(LeanPhrase phrase)
    {
        OpenTutorialMenu(phrase);
    }

    private void OpenTutorialMenu(LeanPhrase phrase)
    {
        _view.SetPhrase(phrase);
        _menuGroup.Open(_view);
    }
}