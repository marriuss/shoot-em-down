using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MenuView
{
    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);
        SetAdditionalObjectsActive(active);
    }

    protected virtual void SetAdditionalObjectsActive(bool active) { }
}
