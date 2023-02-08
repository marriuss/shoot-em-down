using UnityEngine;

public class AuthorizationButton : WorkButton
{
    [SerializeField] private Authorizer _authorizer;

    protected override void OnButtonClick()
    {
        _authorizer.Authorize();
    }
}
