using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class AuthorizationButtonView : MenuView
{
    protected override void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
