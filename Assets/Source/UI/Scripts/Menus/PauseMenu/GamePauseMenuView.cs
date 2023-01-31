using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseMenuView : PauseMenuView
{
    [SerializeField] private Button _exitButton;

    protected override void SetAdditionalObjectsActive(bool active)
    {
        _exitButton.gameObject.SetActive(active);
    }
}
