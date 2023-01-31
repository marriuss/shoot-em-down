using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MenuGroup _menuGroup;
    [SerializeField] private MainMenuView _view;
    [SerializeField] private AuthorizationButtonView _authorizationButtonView;

    private void Start()
    {
        _menuGroup.Open(_view);
    }

    private void Update()
    {
        bool isEnabled = false;

#if UNITY_WEBGL && !UNITY_EDITOR
    isEnabled = YandexGamesSdk.IsInitialized ? !PlayerAccount.IsAuthorized : false;
#endif

        if (isEnabled)
            _authorizationButtonView.Appear();
        else
            _authorizationButtonView.Disappear();
    }
}
