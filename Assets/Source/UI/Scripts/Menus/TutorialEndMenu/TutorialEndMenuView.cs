using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEndMenuView : MenuView
{
    [SerializeField] private Image _background;

    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);
        _background.gameObject.SetActive(active);
    }
}
