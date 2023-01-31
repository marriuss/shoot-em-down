using UnityEngine;
using Agava.YandexGames;

public class AuthorizationButton : WorkButton
{
    protected override void OnButtonClick()
    {
        PlayerAccount.Authorize();
    }
}
