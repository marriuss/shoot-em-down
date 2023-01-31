using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPauseMenuView : PauseMenuView
{
    [SerializeField] private Image _background;

    protected override void SetAdditionalObjectsActive(bool active)
    {
        _background.gameObject.SetActive(active);
    }
}
