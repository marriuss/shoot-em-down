using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MenuView
{
    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
